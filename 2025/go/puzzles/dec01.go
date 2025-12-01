package puzzles

import (
	"aoc/utils"
	"fmt"
)

var Dec01 = Puzzle{
	SolvePart1: func(date string, useTestData bool) {
		fmt.Println("Day 01, Part 1")

		const N = 100
		pos := 50
		zeros := 0

		lines, err := utils.DataFileReader(date, useTestData, "1", false)
		if err != nil {
			fmt.Println("Error reading file:", err)
			return
		}

		for _, line := range lines {
			dir := line[0]
			var dist int
			fmt.Sscanf(line[1:], "%d", &dist)

			if dir == 'L' {
				pos = (pos - dist) % N
			} else {
				pos = (pos + dist) % N
			}

			if pos < 0 {
				pos += N
			}

			if pos == 0 {
				zeros++
			}
		}

		fmt.Println("zeros:", zeros)

	}, // SolvePart1
	SolvePart2: func(date string, useTestData bool) {
		fmt.Println("Day 01, Part 2")

		const N = 100
		pos := 50
		zeros := 0

		lines, err := utils.DataFileReader(date, useTestData, "1", false)
		if err != nil {
			fmt.Println("Error reading file:", err)
			return
		}

		for _, line := range lines {
			dir := line[0]
			var dist int
			fmt.Sscanf(line[1:], "%d", &dist)

			for i := 0; i < dist; i++ {
				if dir == 'L' {
					pos = (pos - 1 + N) % N
				} else {
					pos = (pos + 1) % N
				}
				if pos == 0 {
					zeros++
				}
			}
		}

		fmt.Println("zeros:", zeros)
	}, // SolvePart2
}
