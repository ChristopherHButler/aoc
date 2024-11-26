using System;
using csharp.Puzzles;

var currentDate = "01-12-2000"; // DateTime.Now.ToString("dd-MM-yyyy");

Console.WriteLine($"AoC: {currentDate}");
Console.WriteLine("-----------------\n");


DecXX.solvePart1(currentDate, useTestData: true);
DecXX.solvePart2(currentDate);