using csharp.Puzzles;
using csharp.utils;


var currentDate = DateTime.Now.ToString("dd-MM-yyyy");

Console.WriteLine($"AoC: {currentDate}");
Console.WriteLine("-----------------\n");

// test boilerplate
// var testDate = "01-12-2000";
// Boilerplate.solvePart1(testDate, useTestData: false);
// Boilerplate.solvePart2(testDate);

// run puzzle solvers
var dec01Part1 = Dec01.solvePart1(currentDate, useTestData: true); // ans: 1147
var dec01Part2 = Dec01.solvePart2(currentDate, useTestData: true); // ans: 6789

// var dec02Part1 = Dec02.solvePart1("02-12-2024", useTestData: true); // ans: 282
// var dec02Part2 = Dec02.solvePart2("02-12-2024", useTestData: true); // ans: 349
