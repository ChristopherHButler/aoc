package main

import (
	"aoc/puzzles"
	"fmt"
)

func main() {
	currentDate := "01-12-2000"
	fmt.Printf("AoC: %s\n", currentDate)
	fmt.Println("-----------------")

	// test boilerplate
	// Note - Go does not suport default values or optional parameters.
	puzzles.Boilerplate.SolvePart1(currentDate, true)
	puzzles.Boilerplate.SolvePart2(currentDate, true)

	// run puzzle solvers

}
