package puzzles

import (
	"aoc/utils"
	"fmt"
	"strconv"
	"strings"
)

var Dec03 = Puzzle{
	SolvePart1: func(date string, useTestData bool) {
		fmt.Println("Day 03, Part 1 - ")

		lines, err := utils.DataFileReader(date, useTestData, "1", false)
		if err != nil {
			fmt.Println("Error reading file:", err)
			return
		}
		var total int = 0

		for _, line := range lines {
			//fmt.Println(line)
			// for each line, iterate each char
			nums := strings.Split(line, "")
			first := "0"
			idx := 0
			second := "0"
			for i := 0; i < len(nums)-1; i++ {
				// fmt.Println(nums[i])
				firstNum, _ := strconv.Atoi(first)
				iNum, _ := strconv.Atoi(nums[i])
				if iNum > firstNum && i < len(nums) {
					first = nums[i]
					idx = i + 1
				}
			}
			for j := idx; j < len(nums); j++ {
				secondNum, _ := strconv.Atoi(second)
				jNum, _ := strconv.Atoi(nums[j])
				if jNum >= secondNum {
					second = nums[j]
				}
			}
			// fmt.Println("First:", first, " Second:", second)
			result := first + second
			if val, err := strconv.Atoi(result); err == nil {
				total += val
			}
		}

		var outputString = "Total [using puzzle data]: "

		if useTestData {
			outputString = "Total [using test data]: "
		}

		fmt.Println(outputString, total, "\n\n")
	}, // SolvePart1
	SolvePart2: func(date string, useTestData bool) {
		fmt.Println("Day 03, Part 2 - ")

		lines, err := utils.DataFileReader(date, useTestData, "1", false)
		if err != nil {
			fmt.Println("Error reading file:", err)
			return
		}
		var total int = 0

		for _, line := range lines {
			result := ""
			remaining := strings.Split(line, "")
			for pos := 0; pos < 12; pos++ {
				highest := "0"
				hIdx := 0
				maxIdx := len(remaining) - (11 - pos)

				for i := 0; i < maxIdx; i++ {
					if remaining[i] > string(highest[0]) {
						highest = string(remaining[i])
						hIdx = i
					}
				}

				result += highest
				remaining = remaining[hIdx+1:]

			}
			if val, err := strconv.ParseInt(result, 10, 64); err == nil {
				total += int(val)
			}
		}

		var outputString = "Total [using puzzle data]: "

		if useTestData {
			outputString = "Total [using test data]: "
		}

		fmt.Println(outputString, total, "\n\n")
	}, // SolvePart2
}
