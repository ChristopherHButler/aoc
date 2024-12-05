from utils.file_utils import data_file_reader
from utils.output_utils import print_output
from utils.performance_utils import timing_decorator

@timing_decorator
def solve_part1(date, use_test_data = False):
	print("Day 02, Part 1: Safe reports")

	lines = data_file_reader(date = date, use_test_data = use_test_data, part = 1)
	
	total_safe_reports: int = 0

	# for each report
	for line in lines:
		report: list[str] = line.split(" ")

		if report_is_safe(report):
			total_safe_reports += 1

	print_output(str(total_safe_reports), use_test_data)


@timing_decorator
def solve_part2(date, use_test_data = False):
	print("Day 02, Part 2: Problem Dampener")

	lines = data_file_reader(date = date, use_test_data = use_test_data, part = 1)
	
	total_safe_reports: int = 0

	# for each report
	for line in lines:
		report: list[str] = line.split(" ")

		if report_is_safe_pt2(report):
			total_safe_reports += 1

	print_output(str(total_safe_reports), use_test_data)


def report_is_safe(report: list[str]) -> bool:
	if is_gradually_increasing(report) or is_gradually_decreasing(report):
		return True
	else:
		return False

def report_is_safe_pt2(report: list[str]) -> bool:
	if is_gradually_increasing(report) or is_gradually_decreasing(report):
		return True
	elif safe_with_bad_value(report):
		return True
	else:
		return False

def safe_with_bad_value(report: list[str]) -> bool:
	for i in range(len(report)):
		report_list = list(report)
		report_list.pop(i)
		if is_gradually_increasing(report_list) or is_gradually_decreasing(report_list):
			return True
	return False

def is_gradually_increasing(report: list[str]) -> bool:
	for i in range(len(report) - 1):
		current_level = int(report[i])
		next_level = int(report[i + 1])
		if current_level < next_level and is_within_tolerance(current_level, next_level):
			continue
		else:
			return False
	return True


def is_gradually_decreasing(report: list[str]) -> bool:
	for i in range(len(report) - 1):
		current_level = int(report[i])
		next_level = int(report[i + 1])
		if current_level > next_level and is_within_tolerance(current_level, next_level):
			continue
		else:
			return False
	return True


def is_within_tolerance(current_level: int, next_level: int) -> bool:
	abs_delta = abs(current_level - next_level)
	if abs_delta >= 1 and abs_delta <= 3:
		return True
	else:
		return False

