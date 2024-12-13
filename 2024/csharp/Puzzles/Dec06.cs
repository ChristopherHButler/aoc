using csharp.utils;

namespace csharp.Puzzles;

// whats important for this puzzle: 
// matrix traversal, pathfinding, 
// algorithm to find repeating patterns because brute forcing is shit

public class Pos
{
    public int X { get; set; }
    public int Y { get; set; }
}

interface IGuard
{
		public Pos Position { get; set; }
		public char Direction { get; set; }
		public bool HasLeft { get; set; }
		public ICollection<Tuple<int, int>> Visited { get; set; }
		public void InitGuard(Pos pos, char dir);
}

public class Guard : IGuard
{
	 public Pos Position { get; set; }
	 public char Direction { get; set; }
	 public bool HasLeft { get; set; }
	 // guards cannot visit the same position multiple times
	public ICollection<Tuple<int, int>> Visited { get; set; } = new HashSet<Tuple<int, int>>();

	 public Guard()
	 {
		 Position = new Pos();
		 HasLeft = false;
	 }

	 public void InitGuard(Pos pos, char dir)
	 {
		 Position = pos;
		 Direction = dir;
		 Visited.Add(new Tuple<int, int>(pos.X, pos.Y));
	 }
}

public class OldGuard : IGuard
{
	 public Pos Position { get; set; }
	 public char Direction { get; set; }
	 public bool HasLeft { get; set; }
	 // old guards can visit the same position multiple times
	public ICollection<Tuple<int, int>> Visited { get; set; } = new List<Tuple<int, int>>();

	 public OldGuard()
	 {
		 Position = new Pos();
		 HasLeft = false;
	 }
	 
	 public void InitGuard(Pos pos, char dir)
	 {
		 Position = pos;
		 Direction = dir;
		 Visited.Add(new Tuple<int, int>(pos.X, pos.Y));
	 }

}

// This is the boilerplate code for each day's puzzle.
public static class Dec06
{
		
		public static char[,]? Matrix;
    public static int solvePart1(string? date, bool useTestData = false)
    {
        // log current puzzle
        Console.WriteLine("Day 06, Part 1: Guard Route\n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

        // create a matrix from the data
        Matrix = CreateMatrix(dfr.Lines);

				// print the matrix
        PrintMatrix(Matrix);

				var total = ScanMatrix(Matrix);

        // log the solution
        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {total}\n");

				return total;
    }

		public static int solvePart2(string date, bool useTestData = false)
		{
				// log current puzzle
				Console.WriteLine("Day 06, Part 2: \n");

				// create a data file reader and read the file.
				var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
				dfr.ReadFile();

				// create a matrix from the data
        Matrix = CreateMatrix(dfr.Lines);

				var total = ScanScanScan(Matrix);
				
				// log the solution
				var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
				Console.WriteLine($"{outputString}: {total}\n");

				return total;
		}


		private static char[,] CreateMatrix(List<string> lines)
		{   
				var numRows = lines.Count;
				var numCols = lines[0].ToCharArray().Length;

				char[,] matrix = new char[numRows, numCols];

				for (int i = 0; i < numRows; i++)
				{
						char[] col = lines[i].ToCharArray();

						for (int j = 0; j < numCols; j++)
						{
								matrix[i, j] = col[j];
						}
				}
				return matrix;
		}


		private static int ScanMatrix(char[,] matrix)
		{						
				// create a guard object and set the starting position and direction
				var guard = new Guard();
				var pos = GetStartPosition(matrix);
				guard.InitGuard(pos, matrix[pos.X, pos.Y]);
				
				// start moving the guard
				while (GuardIsPresent(matrix, guard.Position))
				{
					// Console.WriteLine($"Guard is at position [{guard.Position.X}, {guard.Position.Y}]");
					if (guard.HasLeft)
					{
						// Console.WriteLine("Guard has left the matrix.");
						break;
					}
					VisualizeMatrix(matrix, guard);
					MoveGuard(matrix, guard);
				}
				// compute moves
				return guard.Visited.Count;

		}

		private static Pos GetStartPosition(char[,] matrix)
		{
				var numRows = matrix.GetLength(0);
				var numCols = matrix.GetLength(1);

				var guardDirs = new char[] { '>', '<', '^', 'v' };

				for (int i = 0; i < numRows; i++)
				{
					for (int j = 0; j < numCols; j++)
					{
						// Console.WriteLine($"matrix[{i},{j}] = {matrix[i,j]}");
						if (matrix[i,j] == guardDirs[0] || matrix[i,j] == guardDirs[1] || matrix[i,j] == guardDirs[2] || matrix[i,j] == guardDirs[3])
						{
							return new Pos { X = i, Y = j };
						}
					}
				}
				throw new ArgumentException("No guard found in matrix.");
		}

		private static bool GuardIsPresent(char[,] matrix, Pos position)
		{
				var numRows = matrix.GetLength(0);
        var numCols = matrix.GetLength(1);
				return position.X >= 0 && position.X < numRows && position.Y >= 0 && position.Y < numCols; 
		}

		private static void MoveGuard(char[,] matrix, IGuard guard)
		{
				var numRows = matrix.GetLength(0);
        var numCols = matrix.GetLength(1);

				// based on direction. check if the next move is a valid move (obstacle or not)
				// if valid, move in that direction to the next position
				// record the position in a list of tuples
				// if not valid, change direction by 90 deg
				// be aware of the edge of the matrix.



				if (guard.Direction == '>')
				{
					// guard has left
					if (guard.Position.Y + 1 >= numCols)
					{
						//Console.WriteLine("Guard has left the matrix.");
						// guard has left and need to signal that
						guard.HasLeft = true;
						return;
					}
					// guard is changing direction
					else if (matrix[guard.Position.X, guard.Position.Y + 1] == '#')
					{
						//Console.WriteLine("Guard is changing direction. [v]");
						guard.Direction = 'v';
						return;
					}
					// guard is moving
					else
					{
						//Console.WriteLine("Guard is moving: right");
						guard.Position.Y += 1;
						guard.Visited.Add(new Tuple<int, int>(guard.Position.X, guard.Position.Y));
						return;
					}
				}
				else if (guard.Direction == 'v')
				{
					// guard has left
					if (guard.Position.X + 1 >= numRows)
					{
						//Console.WriteLine("Guard has left the matrix.");
						// guard has left and need to signal that
						guard.HasLeft = true;
						return;
					}
					// guard is changing direction
					else if (matrix[guard.Position.X + 1, guard.Position.Y] == '#')
					{
						//Console.WriteLine("Guard is changing direction. [<]");
						guard.Direction = '<';
						return;
					}
					// guard is moving
					else
					{
						//Console.WriteLine("Guard is moving: down");
						guard.Position.X += 1;
						guard.Visited.Add(new Tuple<int, int>(guard.Position.X, guard.Position.Y));
						return;
					}
				}
				else if (guard.Direction == '<')
				{
					
					// guard has left
					if (guard.Position.Y - 1 < 0)
					{
						//Console.WriteLine("Guard has left the matrix.");
						// guard has left and need to signal that
						guard.HasLeft = true;
						return;
					}
					// guard is changing direction
					else if (matrix[guard.Position.X, guard.Position.Y - 1] == '#')
					{
						//Console.WriteLine("Guard is changing direction. [^]");
						guard.Direction = '^';
						return;
					}
					// guard is moving
					else
					{
						//Console.WriteLine("Guard is moving: left");
						guard.Position.Y -= 1;
						guard.Visited.Add(new Tuple<int, int>(guard.Position.X, guard.Position.Y));
						return;
					}
				}
				else if (guard.Direction == '^')
				{
					// guard has left
					if (guard.Position.X - 1 < 0)
					{
						//Console.WriteLine("Guard has left the matrix.");
						// guard has left and need to signal that
						guard.HasLeft = true;
						return;
					}
					// guard is changing direction
					else if (matrix[guard.Position.X - 1, guard.Position.Y] == '#')
					{
						//Console.WriteLine("Guard is changing direction. [>]");
						guard.Direction = '>';
						return;
					}
					// guard is moving
					else
					{
						//Console.WriteLine("Guard is moving: up");
						guard.Position.X -= 1;
						guard.Visited.Add(new Tuple<int, int>(guard.Position.X, guard.Position.Y));
						return;
					}
				}
	
		}


		private static int ScanScanScan(char[,] matrix)
		{
			var placements = new HashSet<Tuple<int, int>>();
			var numRows = matrix.GetLength(0);
			var numCols = matrix.GetLength(1);
			var startPos = GetStartPosition(matrix);

			// loop over each position on the matrix
			for (int i = 0; i < numRows; i++)
			{
				for (int j = 0; j < numCols; j++)
				{
					Console.WriteLine($"Checking position [{i},{j}]");
					// do not check if there is an obstacle in the position or if it's the guard's start position
					if (ObstacleAtPosition(matrix, new Pos { X = i, Y = j }))
					{
						// Console.WriteLine("Obstacle found. Moving to next position");
						continue;
					}
					else if (i == startPos.X && j == startPos.Y)
					{
						// Console.WriteLine("guard start position. Moving to next position");
						continue;
					}

					// if no obstacle, place an obstacle in each position
					var map = SetObstacle(matrix, startPos, new Pos { X = i, Y = j });
					// Console.WriteLine("Placing obstacle at position...");
					// PrintMatrix(map);

					// check if the guard gets stuck
					if (TestObstaclePlacement(map, new Pos { X = i, Y = j }))
					{
						// Console.WriteLine("Found a place to add obstacle.");
						// add the palcement to the list of obstacle positions
						placements.Add(new Tuple<int, int>(i, j));
						// Console.WriteLine("Guard is stuck. Moving to next position.");
						// continue;
					}
				}
			}

			// return possible placements
			return placements.Count;
		}


		private static bool TestObstaclePlacement(char[,] matrix, Pos obs)
		{
				// create a guard object
				var guard = new OldGuard();
				var pos = GetStartPosition(matrix);
				guard.InitGuard(pos, matrix[pos.X, pos.Y]);

				// Console.WriteLine("Testing obstacle placement...");

				while(GuardIsPresent(matrix, guard.Position))
				{
					// the guard is not stuck in a loop
					if (guard.HasLeft)
					{
						// Console.WriteLine("Guard left the map...");
						return false;
					}
					// Console.WriteLine("Moving guard again...");
					// VisualizeMatrix(matrix, guard, obs);
					MoveGuard(matrix, guard);

					// check for a repeating pattern
					if (IsLooping(guard.Visited))
					{
						// Console.WriteLine("Guard is looping!!!");
						return true;
					}
					// Console.WriteLine($"Guard is at position [{guard.Position.X}, {guard.Position.Y}]");
					// Console.WriteLine($"Guard has visited {guard.Visited.Count} positions.");
				}
				// Console.WriteLine("Why would it get here? ");
				// unsure if this makes sense
				return false;				
		}


		private static bool IsLooping(ICollection<Tuple<int, int>> visited)
		{
			// lets just try some really dumb fucking logic
			// if the guard has visited the same position more than five times, it's looping
			var visitedList = visited.ToList();

			var looping = visitedList
												.GroupBy(x => x)
												.Where(g => g.Count() > 5)
												.Any();

			return looping;

			// check if the guard is looping
			// if the guard is looping, return true
			// if the guard is not looping, return false
			// var minLoopSize = 4;

			// 	// guard has not visited enough positions to be looping
			// 	if (visited.Count < minLoopSize) return false;

			// 	var visitedList = visited.ToList();

    	// 	// Try different pattern lengths up to half the list length
			// 	for (int patternLength = 2; patternLength <= visited.Count / 2; patternLength++)
			// 	{
			// 		bool isLooping = true;

			// 		// compare each element with the one 'patternLength' steps ahead
			// 		for (int i = 0; i < visited.Count - patternLength; i++)
			// 		{
			// 			if (!visitedList[i].Equals(visitedList[i + patternLength]))
			// 			{
			// 				isLooping = false;
			// 				break;
			// 			}
			// 		}

			// 		if (isLooping)
			// 		{
			// 			Console.WriteLine($"Found pattern of length {patternLength}");
			// 			return true;
			// 		}
			// 	}
			// 	return false;

		}



		private static bool ObstacleAtPosition(char[,] matrix, Pos position)
		{
			return matrix[position.X, position.Y] == '#';
		}
		private static char[,] SetObstacle(char[,] matrix, Pos startPos, Pos currPos)
		{
				if (ObstacleAtPosition(matrix, currPos) || currPos.Equals(startPos))
				{
						// no need to set an obstacle
					 return matrix;
				}

				// copy matrix first
				var numRows = matrix.GetLength(0);
				var numCols = matrix.GetLength(1);
				char[,] newMatrix = new char[numRows, numCols];

				for (int i = 0; i < numRows; i++)
				{
					for (int j = 0; j < numCols; j++)
					{
						if (i == currPos.X && j == currPos.Y)
						{
							newMatrix[i, j] = '#';
						}
						else
						{
							newMatrix[i, j] = matrix[i, j];
						}
					}
				}
				// Console.WriteLine($"Obstacle placed at [{currPos.X},{currPos.Y}] New Map: ");
				// PrintMatrix(newMatrix);
				return newMatrix;
		}





    private static void PrintMatrix(char[,] matrix)
    {
        var numRows = matrix.GetLength(0);
        var numCols = matrix.GetLength(1);

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                Console.Write($" [{matrix[i,j]}] ");
            }
            Console.WriteLine("");
        }
    }

		private static void VisualizeMatrix(char[,] matrix, IGuard guard, Pos? obs = null)
		{
			Console.Clear(); // Clear previous frame
			var numRows = matrix.GetLength(0);
			var numCols = matrix.GetLength(1);

			for (int i = 0; i < numRows; i++)
			{
				for (int j = 0; j < numCols; j++)
				{
					if (obs != null && i == obs.X && j == obs.Y)
					{
						// highlight the new obstacle
						Console.ForegroundColor = ConsoleColor.Red;
						Console.Write($"[{matrix[i,j]}]");
					}
					else if (guard.Position.X == i && guard.Position.Y == j)
					{
							Console.ForegroundColor = ConsoleColor.Green;
              Console.Write($"[{guard.Direction}]");
					}
					else if (guard.Visited.Contains(new Tuple<int, int>(i, j)))
					{
							Console.ForegroundColor = ConsoleColor.DarkYellow;
              Console.Write($"[{matrix[i,j]}]");
					}
					else
					{
							Console.ForegroundColor = ConsoleColor.Gray;
							Console.Write($"[{matrix[i,j]}]");
					}
				}
				Console.WriteLine("");
			}
			Console.ResetColor();
    	Thread.Sleep(15); // Delay 15ms between frames
		}
		

}
