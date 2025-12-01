from typing import List
from utils.file_utils import data_file_reader



def solve_part1(date: str, use_test_data: bool = False):
	print("Day 01, Part 1")

	lines = data_file_reader(date = date, use_test_data = use_test_data, part = 1)

	N = 100
	pos = 50
	zeros = 0

	for line in lines:
		dir = line[0]
		dist = int(line[1:])
		if dir == 'L':
			pos = (pos - dist) % N
		elif dir == 'R':
			pos = (pos + dist) % N

		if pos < 0:
			pos += N

		if pos == 0:
			zeros += 1

	print(f"Zeros: {zeros}")



def solve_part2(date, use_test_data = False):
	print("Day 01, Part 2")

	lines = data_file_reader(date = date, use_test_data = use_test_data, part = 1)

	N = 100
	pos = 50
	zeros = 0

	for line in lines:
		dir = line[0]
		dist = int(line[1:])
		for i in range (dist):
			if dir == 'L':
				pos = (pos - 1 + N) % N
			elif dir == 'R':
				pos = (pos + 1) % N
			if pos == 0:
				zeros += 1

	print(f"Zeros: {zeros}")
