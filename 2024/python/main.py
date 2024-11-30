from puzzles import (
	dec01
)


def main():
	current_date = "01-12-2000"
	print(f"AoC: {current_date}")
	print('-----------------\n')

	# Run puzzle solvers
	dec01.solve_part1(current_date)
	dec01.solve_part2(current_date, use_test_data=False)

# Run main function
if __name__ == "__main__":
	main()
