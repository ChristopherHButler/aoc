using csharp.utils;

namespace csharp.Puzzles;

// This is the boilerplate code for each day's puzzle.
public static class Boilerplate
{
    public static int solvePart1(string? date, bool useTestData = false)
    {
        // log current puzzle
        Console.WriteLine("Day XX, Part 1: \n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

				var total = dfr.Lines.Count;

        for (int i = 0; i < dfr.Lines.Count; i++) 
        {
            Console.WriteLine(dfr.Lines[i]);
        }
        
        // log the solution
        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {total}\n");

				return total;
    }

    public static int solvePart2(string date, bool useTestData = false)
    {
        // log current puzzle
        Console.WriteLine("Day XX, Part 2: \n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

				var total = dfr.Lines.Count;

        for (int i = 0; i < dfr.Lines.Count; i++) 
        {
            Console.WriteLine(dfr.Lines[i]);
        }
        
        // log the solution
        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {total}\n");

		return total;
    }
}
