package main

import (
	"fmt"
	"time"
)

func main() {
	currentDate := time.Now().Format("02-01-2006")
	fmt.Printf("AoC: %s\n", currentDate)
	fmt.Println("-----------------")

	// test boilerplate
	// Note - Go does not suport default values or optional parameters.
	// testDate := "01-12-2000"
	// params: date, useTestData
	// puzzles.Boilerplate.SolvePart1(testDate, true)
	// puzzles.Boilerplate.SolvePart2(testDate, true)

	// run puzzle solvers
	// puzzles.Dec01.SolvePart1(currentDate, true) // ans: 1147
	// puzzles.Dec01.SolvePart2(currentDate, true) // ans: 6789

	// puzzles.Dec02.SolvePart1(currentDate, true) // ans: 23039913998
	// puzzles.Dec02.SolvePart2(currentDate, true) // ans: 35950619148
}
