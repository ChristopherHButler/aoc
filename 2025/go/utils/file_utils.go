package utils

import (
	"bufio"
	"fmt"
	"os"
	"path/filepath"
	"strings"
)

func DataFileReader(date string, useTestData bool, part string, verbose bool) ([]string, error) {

	currentDir, err := os.Getwd()

	if err != nil {
		fmt.Println("Error getting current directory:", err)
		return nil, err
	}
	if verbose {
		fmt.Println("Current directory:", currentDir)
	}

	parentDir := filepath.Dir(currentDir)

	if verbose {
		fmt.Println("Parent directory:", parentDir)
	}

	var fullpath string
	if useTestData {
		fullpath = parentDir + "/common/data/" + date + "-part" + part + "-test-data.txt"
	} else {
		fullpath = parentDir + "/common/data/" + date + "-data.txt"
	}

	if verbose {
		fmt.Println("Full path:", fullpath)
	}

	file, err := os.Open(fullpath)

	if err != nil {
		return nil, err
	}
	defer file.Close()

	var lines []string
	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		lines = append(lines, scanner.Text())
	}

	if err := scanner.Err(); err != nil {
		return nil, err
	}

	return lines, nil
}

func DataFileToMatrix(date string, useTestData bool, part string, verbose bool) ([][]string, error) {

	currentDir, err := os.Getwd()

	if err != nil {
		fmt.Println("Error getting current directory:", err)
		return nil, err
	}
	if verbose {
		fmt.Println("Current directory:", currentDir)
	}

	parentDir := filepath.Dir(currentDir)

	if verbose {
		fmt.Println("Parent directory:", parentDir)
	}

	var fullpath string
	if useTestData {
		fullpath = parentDir + "/common/data/" + date + "-part" + part + "-test-data.txt"
	} else {
		fullpath = parentDir + "/common/data/" + date + "-data.txt"
	}

	if verbose {
		fmt.Println("Full path:", fullpath)
	}

	file, err := os.Open(fullpath)

	if err != nil {
		return nil, err
	}
	defer file.Close()

	var matrix [][]string
	scanner := bufio.NewScanner(file)

	for scanner.Scan() {
		line := scanner.Text()
		parts := strings.Split(line, "")
		row := append([]string(nil), parts...)
		matrix = append(matrix, row)
	}

	if err := scanner.Err(); err != nil {
		return nil, err
	}

	return matrix, nil
}
