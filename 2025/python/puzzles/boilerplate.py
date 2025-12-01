from utils.file_utils import data_file_reader
from utils.output_utils import print_output
from utils.performance_utils import timing_decorator



def solve_part1(date, use_test_data = False):
	print("Day 00, Part 1: ")

	lines = data_file_reader(date = date, use_test_data = use_test_data, part = 1)
	for line in lines:
		print(line)
	
	total = 0

	print_output(str(total), use_test_data)



def solve_part2(date, use_test_data = False):
	print("Day 00, Part 2: ")

	lines = data_file_reader(date = date, use_test_data = use_test_data, part = 1)
	for line in lines:
		print(line)
	
	total = 0

	print_output(str(total), use_test_data)