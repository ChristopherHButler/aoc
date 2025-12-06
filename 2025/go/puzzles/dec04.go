package puzzles

import (
	"aoc/utils"
	"fmt"
)

var Dec04 = Puzzle{
	SolvePart1: func(date string, useTestData bool) {
		fmt.Println("Day 04, Part 1 - ")

		matrix, err := utils.DataFileToMatrix(date, useTestData, "1", false)
		if err != nil {
			fmt.Println("Error reading file:", err)
			return
		}
		var total int = 0

		// perimeter positions
		var directions = [][]int{
			{-1, -1}, {-1, 0}, {-1, 1},
			{0, -1}, {0, 1},
			{1, -1}, {1, 0}, {1, 1},
		}

		for rowIdx, row := range matrix {
			for colIdx, val := range row {
				// fmt.Printf("Element at [%d][%d]: %s\n", rowIdx, colIdx, val)
				if val == "@" {
					count := 0
					for _, dir := range directions {
						currRowPos := rowIdx + dir[0]
						currColPos := colIdx + dir[1]

						// Check bounds and target value
						if currRowPos >= 0 && currRowPos < len(matrix) && currColPos >= 0 && currColPos < len(matrix[0]) &&
							matrix[currRowPos][currColPos] == "@" {
							count++
						}
					}
					if count < 4 {
						total++
					}
				}
			}
		}

		var outputString = "Total [using puzzle data]: "

		if useTestData {
			outputString = "Total [using test data]: "
		}

		fmt.Println(outputString, total, "\n\n")
	}, // SolvePart1
	SolvePart2: func(date string, useTestData bool) {
		fmt.Println("Day 04, Part 2 - ")

		matrix, err := utils.DataFileToMatrix(date, useTestData, "1", false)
		if err != nil {
			fmt.Println("Error reading file:", err)
			return
		}
		var total int = 0

		// perimeter positions
		var directions = [][]int{
			{-1, -1}, {-1, 0}, {-1, 1},
			{0, -1}, {0, 1},
			{1, -1}, {1, 0}, {1, 1},
		}

	outerloop:
		for rowIdx := 0; rowIdx < len(matrix); rowIdx++ {
			utils.PrintMatrix(matrix)
			row := matrix[rowIdx]
			for colIdx := 0; colIdx < len(row); colIdx++ {
				val := row[colIdx]
				if val == "@" {
					count := 0
					for _, dir := range directions {
						currRowPos := rowIdx + dir[0]
						currColPos := colIdx + dir[1]

						// Check bounds and target value
						if currRowPos >= 0 && currRowPos < len(matrix) && currColPos >= 0 && currColPos < len(matrix[0]) &&
							matrix[currRowPos][currColPos] == "@" {
							count++
						}
					}
					if count < 4 {
						matrix[rowIdx][colIdx] = "x"
						total++
						goto outerloop
					}
				}
			}
		}

		var outputString = "Total [using puzzle data]: "

		if useTestData {
			outputString = "Total [using test data]: "
		}

		fmt.Println(outputString, total, "\n\n")
	}, // SolvePart2
}
