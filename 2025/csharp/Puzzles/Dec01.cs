using csharp.utils;

namespace csharp.Puzzles;

public static class Dec01
{
    public static int solvePart1(string? date, bool useTestData = false)
    {
        Console.WriteLine("Day 01, Part 1: password\n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

        const int N = 100;
        int pos = 50;
        int zeros = 0;


        for (int i = 0; i < dfr.Lines.Count; i++)
        {
            var line = dfr.Lines[i];
            var dir = line[0];
            var dist = Convert.ToInt32(line.Substring(1));

            if (dir == 'L')
                pos = (pos - dist) % N;
            else if (dir == 'R')
                pos = (pos + dist) % N;

            if (pos < 0)
                pos += N;

            if (pos == 0)
                zeros++;

        }

        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {zeros}\n\n");

        return zeros;
    }

    public static int solvePart2(string date, bool useTestData = false)
    {
        Console.WriteLine("Day 01, Part 2: password\n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

        const int N = 100;
        int pos = 50;
        int zeros = 0;


        for (int i = 0; i < dfr.Lines.Count; i++)
        {
            var line = dfr.Lines[i];
            var dir = line[0];
            var dist = Convert.ToInt32(line.Substring(1));

            for (int j = 0; j < dist; j++)
            {
                if (dir == 'L')
                    pos = (pos - 1 + N) % N;
                else if (dir == 'R')
                    pos = (pos + 1) % N;
                if (pos == 0)
                    zeros++;
            }
        }

        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {zeros}\n\n");

        return zeros;

    }
}
