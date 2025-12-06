package puzzles

import (
	"aoc/utils"
	"fmt"
	"strconv"
	"strings"
)

var Dec06 = Puzzle{
	SolvePart1: func(date string, useTestData bool) {
		fmt.Println("Day 06, Part 1 - ")

		matrix, err := utils.DataFileToStringMatrix(date, useTestData, "1", false)
		if err != nil {
			fmt.Println("Error reading file:", err)
			return
		}
		var total int64 = 0

		for col := 0; col < len(matrix[0]); col++ {
			var sum int64 = 0
			op := matrix[len(matrix)-1][col]
			//fmt.Printf("Operation: [%s]\n", op)
			for row := 0; row < len(matrix); row++ {
				if row == len(matrix)-1 {
					continue
				}
				val, _ := strconv.ParseInt(matrix[row][col], 10, 64)
				// fmt.Printf("%s ", matrix[row][col])
				switch op {
				case "+":
					sum += val
				case "*":
					if sum == 0 {
						sum = 1
					}
					sum *= val
				}
			}
			//fmt.Println("total: ", total)
			total += sum
			//fmt.Println("total: ", total)
			//fmt.Println()
		}

		var outputString = "Total [using puzzle data]: "

		if useTestData {
			outputString = "Total [using test data]: "
		}

		fmt.Println(outputString, total, "\n\n")
	}, // SolvePart1
	SolvePart2: func(date string, useTestData bool) {
		fmt.Println("Day 06, Part 2 - ")

		matrix, err := utils.DataFileToCharMatrixNoFilter(date, useTestData, "1", false)
		if err != nil {
			fmt.Println("Error reading file:", err)
			return
		}

		var total int64 = 0
		var sum int64 = 0
		var op string = ""

		for col := 0; col < len(matrix[0]); col++ {
			// fmt.Println("col: ", col)
			var num string = ""
			// set the operation
			if strings.TrimSpace(matrix[len(matrix)-1][col]) != "" {
				op = matrix[len(matrix)-1][col]
			}
			// fmt.Println("op: ", op)

			for row := 0; row < len(matrix); row++ {
				// add the number until we get to the operation
				if row != len(matrix)-1 {
					num += strings.TrimSpace(matrix[row][col])
				} else {
					// when we get to the operation row, complete the operation
					// fmt.Println("num: [", num, "]")
					val, _ := strconv.ParseInt(num, 10, 64)
					//fmt.Println("val: ", val)
					// this is the empty row...go to next one
					if val == 0 {
						continue
					}
					switch op {
					case "+":
						sum += val
						//fmt.Println("+ sum: ", sum)
					case "*":
						if sum == 0 {
							sum = 1
						}
						sum *= val
						//fmt.Println("* sum: ", sum)
					}
					// fmt.Println("len(matrix): ", len(matrix))
					// fmt.Println("len(matrix[0])-1: ", len(matrix[0])-1)
					// fmt.Println("col+2 > len(matrix): ", col+2 > len(matrix))
					// fmt.Println("matrix[len(matrix)-1][col+2]: ", matrix[len(matrix)-1][col+2])
					if col+2 > len(matrix[0])-1 || strings.TrimSpace(matrix[len(matrix)-1][col+2]) != "" {
						total += sum
						//fmt.Println("total: ", total)
						sum = 0
						continue
					}
				}
				// fmt.Println("row: [", row, "]: ", num)
			}

			// fmt.Println()
		}

		var outputString = "Total [using puzzle data]: "

		if useTestData {
			outputString = "Total [using test data]: "
		}

		fmt.Println(outputString, total, "\n\n")
	}, // SolvePart2
}
