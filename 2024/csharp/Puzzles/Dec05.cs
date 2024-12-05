using csharp.utils;

namespace csharp.Puzzles;


public static class Dec05
{
    public static int solvePart1(string? date, bool useTestData = false)
    {
        // log current puzzle
        Console.WriteLine("Day 05, Part 1: sleigh launch safety manual - correctly-ordered updates\n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

        // psuedo code
        // first get the rules and the updates
        var rules = GetRules(dfr.Lines);
        var updates = GetUpdates(dfr.Lines);

        var total = ComputeCorrectUpdates(rules, updates);

        // log the solution
        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {total}\n");

        return total;
    }

    public static int solvePart2(string date, bool useTestData = false)
    {
        // log current puzzle
        Console.WriteLine("Day 05, Part 2: sleigh launch safety manual - re-order pages\n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

        // psuedo code
        // first get the rules and the updates
        var rules = GetRules(dfr.Lines);
        var updates = GetUpdates(dfr.Lines);

        var total = ComputeReorderedPages(rules, updates);

        // log the solution
        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {total}\n");

        return total;
    }

    private static List<Tuple<int, int>> GetRules(List<string> data)
    {
        var rules = new List<Tuple<int, int>>();

        for (int i = 0; i < data.Count; i++)
        {
            // stop when you get to the break
            if (string.IsNullOrEmpty(data[i]))
            {
                return rules;
            }
            var parts = data[i].Split("|");
            rules.Add(new Tuple<int, int>(Convert.ToInt32(parts[0]), Convert.ToInt32(parts[1])));
        }
        return rules;

    }

    private static List<List<int>> GetUpdates(List<string> data)
    {
        var start = false;
        var updates = new List<List<int>>();
        for (int i = 0; i < data.Count; i++)
        {
            if (string.IsNullOrEmpty(data[i]))
            {
                start = true;
                continue;
            }
            if (start)
            {
                List<int> update = data[i].Split(",").Select(x => Convert.ToInt32(x)).ToList();
                updates.Add(update);
            }
        }
        return updates;
    }

    private static int ComputeCorrectUpdates(List<Tuple<int, int>> rules, List<List<int>> updates)
    {
        var count = 0;

        // then go through each update        
        for (int i = 0; i < updates.Count; i++)
        {
            if (CheckUpdateOrder(updates[i], rules))
            {
                // if its in the right order, find the middle page and add that number to the count 
                count += GetMiddlePage(updates[i]);             
            }
        }
        return count;
    }
    private static bool CheckUpdateOrder(List<int> update, List<Tuple<int, int>> rules)
    {

        for (int i = 0; i < rules.Count; i++)
        {
            var rule = rules[i];
            var val1 = rule.Item1;
            var val2 = rule.Item2;

            //Console.Write("Update pages: ");
            for (int j = 0; j < update.Count; j++)
            {
                // Console.Write($"{update[j]}, ");
            }
            
            //Console.WriteLine($"Rule => val1: {val1}, val2: {val2}");

            if (update.Contains(val1) && update.Contains(val2))
            {
                if (update.IndexOf(val1) < update.IndexOf(val2))
                {
                    //Console.WriteLine("Rule is good: \u2705");
                    continue;
                }
                else
                {
                    //Console.WriteLine("Breaks Rule: \u274c");
                    return false;
                }
            }
            else
            {
                // ignore rule
                continue;
            }
        }
        return true;
    }

    private static int GetMiddlePage(List<int> update)
    {
        var index = update.Count / 2;
        return update[index];
    }

    private static int ComputeReorderedPages(List<Tuple<int, int>> rules, List<List<int>> updates)
    {
        var count = 0;

        for (int i = 0; i < updates.Count; i++)
        {
            if (!CheckUpdateOrder(updates[i], rules))
            {
                var update = updates[i];
                Console.Write($"Unordered: ");
                for (int j = 0; j < update.Count; j++)
                {
                    Console.Write($"{update[j]}, ");
                }
                Console.WriteLine($" \u274c");

                // update is in the wrong order. re order it and find the middle page
                // reupdate until its right
                // count += ReOrderUpdate(updates[i], rules);
                var newUpdate = ReOrderUpdate(updates[i], rules);
                while (!CheckUpdateOrder(newUpdate, rules))
                {
                    newUpdate = ReOrderUpdate(newUpdate, rules);
                }
                count += GetMiddlePage(newUpdate);
            }
        }
        return count;
    }

    private static List<int> ReOrderUpdate(List<int> update, List<Tuple<int, int>> rules)
    {
        // var orderedUpdate = new List<int>();

        // go through all rules
        for (int i = 0; i < rules.Count; i++)
        {
            var rule = rules[i];
            var val1 = rule.Item1;
            var val2 = rule.Item2;

            // Console.WriteLine("Current State of Update: ");
            // for (int j = 0; j < update.Count; j++)
            // {
            //     Console.Write($"{update[j]}, ");
            // }
            //Console.WriteLine($"Current Rule => val1: {val1}, val2: {val2}");

            // check if rule applies to update
            if (update.Contains(val1) && update.Contains(val2))
            {
                // Console.WriteLine($"index of {val1} is {update.IndexOf(val1)}");
                // Console.WriteLine($"index of {val2} is {update.IndexOf(val2)}");
                
                // perform the reorder update
                if (update.IndexOf(val1) > update.IndexOf(val2))
                {
                    // Console.WriteLine($"Need to switch values for Rule => val1: {val1}, val2: {val2}");
                    // swap the values
                    // int temp = update[update.IndexOf(val1)];
                    // update[update.IndexOf(val1)] = update[update.IndexOf(val2)];
                    // update[update.IndexOf(val2)] = temp;
                    // don't swap, move
                    int index1 = update.IndexOf(val1);
                    int index2 = update.IndexOf(val2);
                    update.RemoveAt(index2);
                    update.Insert(index1, val2);

                }
            }
            // else
            // {
            //     Console.WriteLine($"This rule is ok. Rule => val1: {val1}, val2: {val2}");
            //     // ignore rule
            //     continue;
            // }
        }

        Console.Write("Reordered: ");
        for (int j = 0; j < update.Count; j++)
        {
            Console.Write($"{update[j]}, ");
        }
        Console.WriteLine(" \u2705");

        return update;

        // get the middle page
        // return GetMiddlePage(update);
    }

}
