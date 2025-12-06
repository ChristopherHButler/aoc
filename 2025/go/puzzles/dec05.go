package puzzles

import (
	"aoc/utils"
	"fmt"
	"sort"
	"strconv"
	"strings"
)

var Dec05 = Puzzle{
	SolvePart1: func(date string, useTestData bool) {
		fmt.Println("Day 05, Part 1 - ")

		lines, err := utils.DataFileReader(date, useTestData, "1", false)
		if err != nil {
			fmt.Println("Error reading file:", err)
			return
		}
		var total int = 0

		type Range struct {
			start int64
			end   int64
		}

		var ranges []Range

		// ingredients := make(map[int64]bool)
		var processingIngredients = false

		for _, line := range lines {
			if line == "" {
				processingIngredients = true
				continue
			}
			// fmt.Println(line)
			if !processingIngredients {
				//  parse range
				parts := strings.Split(line, "-")
				start, _ := strconv.ParseInt(parts[0], 10, 64)
				end, _ := strconv.ParseInt(parts[1], 10, 64)
				ranges = append(ranges, Range{start: start, end: end})
				// // mark ingredients in range as true
				// for i := start; i <= end; i++ {
				// 	if !ingredients[i] {
				// 		ingredients[i] = true
				// 	}
				// }
			} else {
				// process ingredients
				ingId, _ := strconv.ParseInt(line, 10, 64)
				for _, r := range ranges {
					if ingId >= r.start && ingId <= r.end {
						total++
						break
					}
				}
				// if ingredients[ingId] {
				// 	total++
				// }
			}
		}

		var outputString = "Total [using puzzle data]: "

		if useTestData {
			outputString = "Total [using test data]: "
		}

		fmt.Println(outputString, total, "\n\n")
	}, // SolvePart1
	SolvePart2: func(date string, useTestData bool) {
		fmt.Println("Day 05, Part 2 - ")

		lines, err := utils.DataFileReader(date, useTestData, "1", false)
		if err != nil {
			fmt.Println("Error reading file:", err)
			return
		}
		var total int = 0

		type Range struct {
			start int64
			end   int64
		}

		var ranges []Range

		for _, line := range lines {
			if line == "" {
				break
			}
			parts := strings.Split(line, "-")
			start, _ := strconv.ParseInt(parts[0], 10, 64)
			end, _ := strconv.ParseInt(parts[1], 10, 64)
			ranges = append(ranges, Range{start: start, end: end})
		}

		// sort ranges
		sort.Slice(ranges, func(i, j int) bool {
			return ranges[i].start < ranges[j].start
		})

		// merge overlapping ranges
		var merged []Range
		current := ranges[0]

		// current is the first element, i starts from 1, not 0
		for i := 1; i < len(ranges); i++ {
			// if the end of the current range is equal to or
			// larger than the start of the next range, they overlap
			if current.end >= ranges[i].start {
				current.end = max(current.end, ranges[i].end)
				// dont append yet, keep checking
			} else {
				merged = append(merged, current)
				current = ranges[i]
			}
		}
		// merge the last range
		merged = append(merged, current)

		// calc total num of Ids from each merged range
		for _, r := range merged {
			total += int(r.end - r.start + 1)
		}

		var outputString = "Total [using puzzle data]: "

		if useTestData {
			outputString = "Total [using test data]: "
		}

		fmt.Println(outputString, total, "\n\n")
	}, // SolvePart2
}
