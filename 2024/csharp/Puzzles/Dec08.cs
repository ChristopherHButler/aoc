using csharp.utils;

namespace csharp.Puzzles;

public static class Dec08
{
    public static char[,]? Matrix;
    public static Dictionary<(int, int), char> AntennaMap = new Dictionary<(int, int), char>();
    public static Dictionary<(int, int), char> AntinodeMap = new Dictionary<(int, int), char>();


    public static int solvePart1(string? date, bool useTestData = false)
    {
        // log current puzzle
        Console.WriteLine("Day 08, Part 1: Antinodes\n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

        // create a matrix from the data
        Matrix = CreateMatrix(dfr.Lines);

        // get all the antennas
        FindAntennas(Matrix, AntennaMap);

        // find the antinodes
        FindAntinodes(Matrix, AntennaMap, AntinodeMap);

        foreach (var antinode in AntinodeMap)
        {
            Console.WriteLine(antinode);
        }
        
		var total = AntinodeMap.Count;

        // log the solution
        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {total}\n");

		return total;
    }

    public static int solvePart2(string date, bool useTestData = false)
    {
        // log current puzzle
        Console.WriteLine("Day 08, Part 2: ALL the antinodes!!\n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

        // create a matrix from the data
        Matrix = CreateMatrix(dfr.Lines);

        // get all the antennas
        AntennaMap.Clear();
        FindAntennas(Matrix, AntennaMap);

        // find the antinodes
        AntinodeMap.Clear();
        FindAllAntinodes(Matrix, AntennaMap, AntinodeMap);

        DebugPrint(Matrix, AntennaMap, AntinodeMap, "After finding antinodes");

        var total = AntinodeMap.Count;
        
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

    private static void FindAntennas(char[,] matrix, Dictionary<(int, int), char> antennaMap)
    {
        var numRows = matrix.GetLength(0);
        var numCols = matrix.GetLength(1);

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {                
                if(char.IsLower(matrix[i,j]) || char.IsUpper(matrix[i,j]) || char.IsDigit(matrix[i,j]))
                {
                    antennaMap.Add((i, j), matrix[i,j]);
                }
            }
        }
    }

    private static void FindAntinodes(char[,] matrix, Dictionary<(int, int), char> antennaMap, Dictionary<(int, int), char> antinodeMap)
    {
        foreach (var antenna1 in antennaMap)
        {
            var (x1, y1) = antenna1.Key;
            var freq1 = antenna1.Value;

            foreach (var antenna2 in antennaMap)
            {
                if (antenna1.Key == antenna2.Key) continue;

                var (x2, y2) = antenna2.Key;
                var freq2 = antenna2.Value;

                if (freq1 == freq2)
                {
                    // compute the antinodes
                    int xAntinode1 = x1 + 2 * (x2 - x1);
                    int yAntinode1 = y1 + 2 * (y2 - y1);

                    // make sure the first antinode is within the map
                    if (xAntinode1 >= 0 && xAntinode1 < matrix.GetLength(1) && yAntinode1 >= 0 && yAntinode1 < matrix.GetLength(0))
                    {
                        var antinode1 = (xAntinode1, yAntinode1);

                        if (!antinodeMap.ContainsKey(antinode1))
                        {
                            antinodeMap[antinode1] = freq1;
                        }
                    }

                    int xAntinode2 = x2 + 2 * (x1 - x2);
                    int yAntinode2 = y2 + 2 * (y1 - y2);
                    
                    // make sure the second antinode is within the map
                    if (xAntinode2 >= 0 && xAntinode2 < matrix.GetLength(1) && yAntinode2 >= 0 && yAntinode2 < matrix.GetLength(0))
                    {
                        var antinode2 = (xAntinode2, yAntinode2);
                        
                        if (!antinodeMap.ContainsKey(antinode2))
                        {
                            antinodeMap[antinode2] = freq2;
                        }
                    }
                }
            }
        }
    }

    private static void FindAllAntinodes(char[,] matrix, Dictionary<(int, int), char> antennaMap, Dictionary<(int, int), char> antinodeMap)
    {
        // Group antennas by their frequency character (a-z, A-Z, 0-9)
        // Only keep groups with 2 or more antennas since we need pairs
        var frequencyGroups = antennaMap
                .GroupBy(a => a.Value)
                .Where(g => g.Count() >= 2);

        // Process each frequency group separately
        foreach (var group in frequencyGroups)
        {
            // Get the frequency character (e.g. 'a', 'B', '0') 
            var frequency = group.Key;
            
            // Extract just the coordinates of antennas with this frequency
            var antennas = group.Select(a => a.Key).ToList();

            // Check every possible pair of antennas in this frequency group
            for (int i = 0; i < antennas.Count; i++)
            {
                for (int j = i + 1; j < antennas.Count; j++)
                {
                    // Get coordinates of both antennas in the pair
                    var (x1, y1) = antennas[i];  // First antenna position
                    var (x2, y2) = antennas[j];  // Second antenna position

                    // Calculate the direction vector between the antennas
                    // A direction vector represents how to move from one point to another in a 2D grid
                    // Point 1: (x1,y1) = (2,3)
                    // Point 2: (x2,y2) = (5,5)

                    // Direction vector = (dx,dy) = (x2-x1, y2-y1) = (3,2)

                    // This means:
                    // - Move 3 units in x direction (right)
                    // - Move 2 units in y direction (down)
                    
                    var dx = x2 - x1;  // Change in x direction
                    var dy = y2 - y1;  // Change in y direction

                    // Find the GCD to reduce the direction vector to smallest steps
                    var gcd = GCD(Math.Abs(dx), Math.Abs(dy));
                    if (gcd != 0)  // Prevent division by zero
                    {
                        dx /= gcd;  // Normalize x step size
                        dy /= gcd;  // Normalize y step size
                    }

                    // Start at first antenna position
                    var x = x1;
                    var y = y1;

                    // Mark all points forward along the line until matrix boundary
                    while (x >= 0 && x < matrix.GetLength(1) && 
                        y >= 0 && y < matrix.GetLength(0))
                    {
                        antinodeMap.TryAdd((x, y), frequency);
                        x += dx;  // Step forward in x direction
                        y += dy;  // Step forward in y direction
                    }

                    // Start at first antenna again but go backwards
                    x = x1 - dx;  // First step backwards in x
                    y = y1 - dy;  // First step backwards in y

                    // Mark all points backward along the line until matrix boundary
                    while (x >= 0 && x < matrix.GetLength(1) && 
                        y >= 0 && y < matrix.GetLength(0))
                    {
                        antinodeMap.TryAdd((x, y), frequency);
                        x -= dx;  // Step backward in x direction
                        y -= dy;  // Step backward in y direction
                    }
                }
            }
        }
    }

    private static int GCD(int a, int b)
    {
        while (b != 0)
        {
            var t = b;
            b = a % b;
            a = t;
        }
        return a;
    }


    private static void DebugPrint(char[,] matrix, Dictionary<(int, int), char> antennaMap, Dictionary<(int, int), char> antinodeMap, string message = "")
    {
        if (!string.IsNullOrEmpty(message))
        {
            Console.WriteLine($"\n=== {message} ===");
        }

        var numRows = matrix.GetLength(0);
        var numCols = matrix.GetLength(1);

        // Print grid with antennas and antinodes
        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                var pos = (j, i);
                if (antennaMap.TryGetValue(pos, out char freq))
                {
                    Console.ForegroundColor = ConsoleColor.Green;
                    Console.Write($"{freq}");
                }
                else if (antinodeMap.TryGetValue(pos, out char afreq))
                {
                    Console.ForegroundColor = ConsoleColor.Yellow;
                    Console.Write("#");
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Gray;
                    Console.Write(".");
                }
            }
            Console.WriteLine();
        }
        Console.ResetColor();

        // Print stats
        Console.WriteLine($"\nAntennas: {antennaMap.Count}");
        Console.WriteLine($"Antinodes: {antinodeMap.Count}");
    }

}
