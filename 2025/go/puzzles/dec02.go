package puzzles

import (
	"aoc/utils"
	"fmt"
	"strconv"
	"strings"
)

var Dec02 = Puzzle{
	SolvePart1: func(date string, useTestData bool) {
		fmt.Println("Day 02, Part 1 - pattern recognition")

		lines, err := utils.DataFileReader(date, useTestData, "1", false)
		if err != nil {
			fmt.Println("Error reading file:", err)
			return
		}

		ranges := strings.Split(lines[0], string(','))

		total := 0

		for _, r := range ranges {
			stnd := strings.Split(r, "-")
			first, _ := strconv.Atoi(stnd[0])
			last, _ := strconv.Atoi(stnd[1])

			// fmt.Println("Processing range:", r)
			// fmt.Println("Range:", first, last)

			for i := first; i <= last; i++ {
				numAsString := strconv.Itoa(i)
				if len(numAsString)%2 == 0 {
					mid := len(numAsString) / 2
					firstHalf := numAsString[:mid]
					secondHalf := numAsString[mid:]
					if firstHalf == secondHalf {
						total += i
					}
				}
			}
		}

		fmt.Println("Total:", total)
	}, // SolvePart1
	SolvePart2: func(date string, useTestData bool) {
		fmt.Println("Day 02, Part 2 - advanced (chunked) pattern recognition")

		lines, err := utils.DataFileReader(date, useTestData, "1", false)
		if err != nil {
			fmt.Println("Error reading file:", err)
			return
		}

		ranges := strings.Split(lines[0], string(','))

		total := 0

		for _, r := range ranges {
			stnd := strings.Split(r, "-")
			first, _ := strconv.Atoi(stnd[0])
			last, _ := strconv.Atoi(stnd[1])

			for i := first; i <= last; i++ {
				numAsString := strconv.Itoa(i)
				length := len(numAsString)
				mid := length / 2
				for j := 1; j <= mid; j++ {
					if length%j == 0 {
						pattern := numAsString[:j]
						isRepeating := true
						for k := j; k < length; k += j {
							if numAsString[k:k+j] != pattern {
								isRepeating = false
								break
							}
						}
						if isRepeating {
							total += i
							break
						}
					}

				}
			}
		}

		fmt.Println("Total:", total)
	}, // SolvePart2
}
