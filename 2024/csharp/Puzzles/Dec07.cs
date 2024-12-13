using csharp.utils;

namespace csharp.Puzzles;

// Whats important for this puzzle: bitwise shifting
// bitwise shifting can help us find the number of combinations of something
// here I'm using it for the number of operators in an equation


public class Equations
{
	public decimal TestValue { get; set; }
	public List<decimal> Numbers { get; set; } = new List<decimal>();
	public bool CanBeTrue { get; set; } = false;
}

public static class Dec07
{
    public static decimal solvePart1(string? date, bool useTestData = false)
    {
        // log current puzzle
        Console.WriteLine("Day 07, Part 1: Operators\n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

        var total = ParseAndComputeEquations(dfr.Lines);
        
        // log the solution
        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {total}\n");

				return total;
    }

    public static decimal solvePart2(string date, bool useTestData = false)
    {
        // log current puzzle
        Console.WriteLine("Day 07, Part 2: || Operators\n");

        // create a data file reader and read the file.
        var dfr = new DataFileReader(date: date, useTestData: useTestData, part: 1);
        dfr.ReadFile();

				var total = ParseAndComputeEquations2(dfr.Lines);
        
        // log the solution
        var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
        Console.WriteLine($"{outputString}: {total}\n");

				return total;
    }


	private static decimal ParseAndComputeEquations(List<string> lines)
	{
		var equations = new List<Equations>();
		decimal count = 0;

		foreach (var line in lines)
		{
			var parts = line.Split(":");

			// Console.WriteLine(parts[0]);

			var testVal = decimal.Parse(parts[0].Trim());
			var numbers = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

			var eq = new Equations
			{
				TestValue = testVal,
				Numbers = numbers.Select(x => decimal.Parse(x)).ToList()
			};
			eq.CanBeTrue = ComputeEquation(eq);
			if (eq.CanBeTrue) count += testVal;
			equations.Add(eq);
		}
		return count;
	}

	private static bool ComputeEquation(Equations eq)
	{
			// each line is an equation. The first number is the result of combining the rest of the numbers using + or *
			// example data:
			// 190: 10 19
      // 3267: 81 40 27

			// we need to test every combination of operators for each equation.
			// we can use bitwise shifting to test every combination of operators

			// we need 1 less operator than numbers between all numbers. 
			// 2 numbers means 1 operator between 2 numbers
			// 3 numbers means 2 operators between 3 numbers
			

			// in the first example, there are two numbers (10 and 19) so we need 1 operator
			// numOperators = 2-1 = 1
			// in the second example, we need 2 operators between 3 numbers so:
			// numOperators = 2. 
			var numOperators = eq.Numbers.Count - 1;

			// the loop will run for all possible combinations of operators
			// for the first example, 1 << 1 = 2, so the loop will run twice
			// this is correct because the operator can either be + or *
			// for the next equation, 1 << 2 = 4, so the loop will run four times
			// this is correct because the operator can be ++, **, +*, or *+

			// the double for loop is used to test every possible combination of operators
			// i represents the current combination of operators
			// j represents the current operator's position. 
			// For the first example there is only 1 position, between 10 and 19
			// For the second example there are 2 positions, between 81 and 40, and between 40 and 27

			for (int i = 0; i < (1 << numOperators); i++)
			{
					var result = eq.Numbers[0];

					for (int j = 0; j < numOperators; j++)
					{ 
							// check if the operator is a multiply or add
							// the loops will evaluate as follows for 3 numbers
							//   i = 0 (binary: 00)
							//   j=0: 00 & 01 = 0 → add
							//   j=1: 00 & 10 = 0 → add
							//   Result: 81 + 40 + 27 = 148

							// i = 1 (binary: 01)
							//   j=0: 01 & 01 = 1 → multiply
							//   j=1: 01 & 10 = 0 → add
							//   Result: 81 * 40 + 27 = 3267 (matches the test value)

							// i = 2 (binary: 10)
							//   j=0: 10 & 01 = 0 → add
							//   j=1: 10 & 10 = 1 → multiply
							//   Result: 81 + 40 * 27 = 3267 (matches the test value by the puzzle rules)

							// i = 3 (binary: 11)
							//   j=0: 11 & 01 = 1 → multiply
							//   j=1: 11 & 10 = 1 → multiply
							//   Result: 81 * 40 * 27 = 87120
							bool isMultiply = (i & (1 << j)) != 0;

							// get the next number in the equation
							decimal nextNum = eq.Numbers[j + 1];

							if (isMultiply)
							{
									result *= nextNum;
							}
							else
							{
									result += nextNum;
							}
					}
					if (result == eq.TestValue)
					{
							return true;
					}
			}
			return false;	
	}

	private static decimal ParseAndComputeEquations2(List<string> lines)
	{
		var equations = new List<Equations>();
		decimal count = 0;

		for (int i = 0; i < lines.Count; i++)
		{
			Console.WriteLine($"Processing Equation: {i} of {lines.Count}");

			var parts = lines[i].Split(":");

			var testVal = decimal.Parse(parts[0].Trim());
			var numbers = parts[1].Split(" ", StringSplitOptions.RemoveEmptyEntries);

			var eq = new Equations
			{
				TestValue = testVal,
				Numbers = numbers.Select(x => decimal.Parse(x)).ToList()
			};
			eq.CanBeTrue = ComputeEquation2(eq);
			if (eq.CanBeTrue) count += testVal;
			equations.Add(eq);
		}
		return count;
	}


	private static bool ComputeEquation2(Equations eq)
	{
			var numbers = eq.Numbers;
			var target = eq.TestValue;

			var numOperators = numbers.Count - 1;

			if (numOperators < 1)
					return numbers.Count == 1 && numbers[0] == target;
			
			// there are three operstors: + (0), * (1), || (2)
			// total combinations = 3^numOperators
			// need to iterate from 0 - 3^(numOperators)-1 in base 3
			var max = (int)Math.Pow(3, numOperators);

			// where i is the current combination of operators
			for (int i = 0; i < max; i++)
			{
					var ops = GetOperatorCombination(i, numOperators);
					decimal result = EvaluateCombination(numbers, ops);
					if (result == target)
							return true;
			}

			return false;
	}


	private static int[] GetOperatorCombination(int combo, int length)
	{
			// Convert "combo" to base-3 representation
			// length = number of operator positions
			var ops = new int[length];
			int value = combo;
			for (int i = length - 1; i >= 0; i--)
			{
					ops[i] = value % 3;
					value /= 3;
			}
			return ops;
	}


	private static decimal EvaluateCombination(List<decimal> numbers, int[] ops)
	{
			// Evaluate left-to-right:
			// ops[i] applies between current result and numbers[i+1]
			// ops codes: 0 = '+', 1 = '*', 2 = '||'
			decimal current = numbers[0];

			for (int i = 0; i < ops.Length; i++)
			{
					int op = ops[i];
					decimal next = numbers[i + 1];

					if (op == 2)
					{
							// Concatenation
							// Convert to string, concatenate, then parse back
							current = decimal.Parse(current.ToString() + next.ToString());
					}
					else if (op == 0)
					{
							// Addition
							current = current + next;
					}
					else
					{
							// Multiplication
							current = current * next;
					}
			}

			return current;
	}	


}
