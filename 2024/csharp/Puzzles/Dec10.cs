using System.Formats.Asn1;
using System.Runtime.InteropServices;
using csharp.utils;

namespace csharp.Puzzles;

public static class Dec10
{
    public static int[,]? Map;

    public static int solvePart1(string? date, bool useTestData = false)
    {
        // log current puzzle
        Console.WriteLine("Day 10, Part 1: \n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

        Map = CreateMatrix(dfr.Lines);

		var total = ComputeScores(Map);
        
        // log the solution
        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {total}\n");

		return total;
    }

    public static int solvePart2(string date, bool useTestData = false)
    {
        // log current puzzle
        Console.WriteLine("Day 10, Part 2: \n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

        Map = CreateMatrix(dfr.Lines);

        var total = ComputeTrailHeadRatings(Map);
        
        // log the solution
        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {total}\n");

		return total;
    }


    private static int[,] CreateMatrix(List<string> lines)
    {   
        var numRows = lines.Count;
        var numCols = lines[0].ToCharArray().Length;

        int[,] matrix = new int[numRows, numCols];

        for (int i = 0; i < numRows; i++)
        {
            char[] col = lines[i].ToCharArray();

            for (int j = 0; j < numCols; j++)
            {
                matrix[i, j] = Convert.ToInt32(col[j].ToString());
            }
        }
        return matrix;
    }


    private static List<(int, int)> FindTrailHeads(int[,] map)
    {
        //  everything working....
        var numRows = map.GetLength(0);
        var numCols = map.GetLength(1);

        var trailHeads = new List<(int, int)>();

        for (int i = 0; i < numRows; i++)
        {
            for (int j = 0; j < numCols; j++)
            {
                if (map[i,j] == 0)
                {
                    trailHeads.Add((i, j));
                }
            }
        }
        return trailHeads;
    }


    private static int BFS(int[,] map, (int, int) trailHead)
    {
        var rows = map.GetLength(0);
        var cols = map.GetLength(1);


        Queue<(int, int)> positions = new Queue<(int, int)>();
        HashSet<(int, int)> visited = new HashSet<(int, int)>();
        HashSet<(int, int)> reachableNines = new HashSet<(int, int)>();

        // add the start position
        positions.Enqueue(trailHead);

        while (positions.Count > 0)
        {
            var (x, y) = positions.Dequeue();
            
            if (visited.Contains((x, y))) continue;
            
            visited.Add((x, y));

            if (map[x, y] == 9) reachableNines.Add((x, y));
            
            // explore neighbors
            foreach (var (nx, ny) in GetNeighbors(x, y, rows, cols))
            {
                if (!visited.Contains((nx, ny)) && map[nx, ny] == map[x, y] + 1)
                {
                    positions.Enqueue((nx, ny));
                }
            }
        }
        Console.WriteLine($"reachableNines: {reachableNines.Count}");
        return reachableNines.Count;
    }

    private static IEnumerable<(int, int)> GetNeighbors(int x, int y, int rows, int cols)
    {
        int[] dx = {-1, 1, 0, 0};
        int[] dy = {0, 0, -1, 1};

        for (int i = 0; i < 4; i++)
        {
            int nx = x + dx[i];
            int ny = y + dy[i];

            if (nx >= 0 && nx < cols && ny >= 0 && ny < rows)
            {
                yield return (nx, ny);
            }
        }
    }


    private static int ComputeScores(int[,] map)
    {
        List<(int, int)> trailHeads = FindTrailHeads(map);

        Console.WriteLine($"trailHeads: {trailHeads.Count}");

        var total = 0;

        foreach (var trailHead in trailHeads)
        {
            total += BFS(map, trailHead);
        }

        return total;

    }


    private static int DFS(int[,] map, int x, int y, HashSet<(int, int)> visited)
    {
        // skip current position
        if (visited.Contains((x, y))) return 0;

        // Add current position to visited set for this path
        visited.Add((x, y));

        if (map[x, y] == 9)
        {
            visited.Remove((x, y));
            return 1;
        }

        int rows = map.GetLength(0);
        int cols = map.GetLength(1);
        int trailCount = 0;



        // Check all neighbors
        foreach (var (nx, ny) in GetNeighbors(x, y, rows, cols))
        {
            // check that the neighbor a) hasn't been visisted and b) satisfies the hiking rules
            if (map[nx, ny] == map[x, y] + 1)
            {
                // Recursively explore this neighbor
                trailCount += DFS(map, nx, ny, visited);
            }
        }
        visited.Remove((x, y));

        return trailCount;
    }

    private static int ComputeTrailHeadRatings(int[,] map)
    {
        List<(int, int)> trailHeads = FindTrailHeads(map);

        Console.WriteLine($"trailHeads: {trailHeads.Count}");

        var total = 0;

        foreach (var trailHead in trailHeads)
        {
            var (x, y) = trailHead;
            int rating = DFS(map, x, y, new HashSet<(int, int)>());
            Console.WriteLine($"Trailhead {trailHead} rating: {rating}");
            total += rating;
        }

        return total;
    }


}
