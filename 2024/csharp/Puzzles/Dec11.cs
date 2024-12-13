using csharp.utils;

namespace csharp.Puzzles;

// This is the boilerplate code for each day's puzzle.
public static class Dec11
{
    static Dictionary<(string, int), long> memo = new Dictionary<(string, int), long>();

    public static int solvePart1(string? date, bool useTestData = false)
    {
        // log current puzzle
        Console.WriteLine("Day 11, Part 1: Plutonian Pebbles - Blink 25\n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

				var total = ComputeStones(dfr.Lines[0]);
        
        // log the solution
        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {total}\n");

		return total;
    }

    public static long solvePart2(string date, bool useTestData = false)
    {
        // log current puzzle
        Console.WriteLine("Day 11, Part 2: \n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

		var total = ComputeStonesRecursive(dfr.Lines[0]);
        
        // log the solution
        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {total}\n");

		return total;
    }


    public static int ComputeStones(string data)
    {
        // create a list of stones
        var stones = data.Split(" ").Select(long.Parse).ToList();

        var numIterations = 25;

        // for each blink, check what to do with each stone.
        for (int i = 0; i < numIterations; i++)
        {
            Console.WriteLine($"Blink: {i}");
            var newStones = new List<long>();

            // for each stone, check what should be done
            for (int j = 0; j < stones.Count; j++)
            {
                // Console.WriteLine($"Stone: {stones[j]}");
                // rule 1: if the stone is 0, replace it with a number 1
                if (stones[j] == 0)
                {
                    newStones.Add(1);
                }
                // rule 2: if stone is even, replace it by two stones.
                // The left half of the digits are engraved on the new left stone, 
                // and the right half of the digits are engraved on the new right stone. 
                // (The new numbers don't keep extra leading zeroes: 1000 would become stones 10 and 0.)
                else if (stones[j].ToString().ToCharArray().Count() % 2 == 0)
                {
                    var stone = stones[j].ToString();
                    var half = stone.Length / 2;
                    var left = stone.Substring(0, half);
                    var right = stone.Substring(half);
                    newStones.Add(long.Parse(left));
                    newStones.Add(long.Parse(right));
                }
                // rule 3: If none of the other rules apply, 
                // the stone is replaced by a new stone; 
                // the old stone's number multiplied by 2024 is engraved on the new stone.
                else 
                {
                    newStones.Add(stones[j] * 2024);
                }
            }
            stones = newStones;
        }

        // foreach (var stone in stones)
        // {
        //     Console.Write($"{stone} ");
        // }

        return stones.Count;
    }


    private static long ComputeStonesRecursive(string data)
    {
        var stones = data.Split(" ", StringSplitOptions.RemoveEmptyEntries);

        var numIterations = 75;
        long total = 0;

        foreach (var stone in stones)
        {            
            total += CountStonesAfter(stone, numIterations);
        }
        return total;
    }

    private static long CountStonesAfter(string stone, int numIterations)
    {
        // no steps left
        if (numIterations == 0) return 1;

        // check the cache to see if you've computed the value before
        if (memo.TryGetValue((stone, numIterations), out var cached))
        {
            return cached;
        }

        long result;

        // rule 1: if the stone is 0, replace it with a number 1 
        if (stone == "0")
        {
            result = CountStonesAfter("1", numIterations - 1);
        }

        // rule 2: if stone is even, replace it by two stones.
        else if (stone.Length % 2 == 0)
        {
            var half = stone.Length / 2;
            var left = stone.Substring(0, half).TrimStart('0');
            var right = stone.Substring(half).TrimStart('0');

            // If trimming leads to empty, that stone is actually "0"
            if (string.IsNullOrEmpty(left)) left = "0";
            if (string.IsNullOrEmpty(right)) right = "0";

            result = CountStonesAfter(left, numIterations - 1) + CountStonesAfter(right, numIterations - 1);
        }
        // rule 3: If none of the other rules apply,
        // the stone is replaced by a new stone;
        // the old stone's number multiplied by 2024 is engraved on the new stone.
        else
        {
            result = CountStonesAfter((long.Parse(stone) * 2024).ToString(), numIterations - 1);
        }

        memo[(stone, numIterations)] = result;
        return result;
    }


}
