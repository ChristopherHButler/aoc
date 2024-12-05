use crate::utils::data_file_reader::data_file_reader;
use crate::utils::output_utils::print_output;


pub fn solvePart1(date: &str, use_test_data: bool) {
	println!("day 04, Part 1: XMAS");
	
	match data_file_reader(date, use_test_data, 1, false) {
		Ok(lines) => {
			let matrix = create_matrix(&lines);

			print_matrix(&matrix);
			
			let total = scan_matrix(&matrix);

			print_output(&total.to_string(), use_test_data);
		}
		Err(_) => {
			println!("Error reading file");
		}
	}

}

pub fn solvePart2(date: &str, use_test_data: bool) {
	println!("day 04, Part 2: X-MAS");

	match data_file_reader(date, use_test_data, 1, false) {
		Ok(lines) => {
			let matrix = create_matrix(&lines);

			print_matrix(&matrix);
			
			let total = turbo_scan(&matrix);

			print_output(&total.to_string(), use_test_data);
		}
		Err(_) => {
			println!("Error reading file");
		}
	}
}


fn create_matrix(lines: &Vec<String>) -> Vec<Vec<char>> {
    let num_rows = lines.len();
    let num_cols = lines[0].len();
    let mut matrix = vec![vec![' '; num_cols]; num_rows];

    for (i, line) in lines.iter().enumerate() {
        for (j, ch) in line.chars().enumerate() {
            matrix[i][j] = ch;
        }
    }
    matrix
}

fn scan_matrix(matrix: &Vec<Vec<char>>) -> i32 {
		let num_rows = matrix.len();
		let num_cols = matrix[0].len();

		let mut count = 0;
		for i in 0..num_rows {
				for j in 0..num_cols {
						if matrix[i][j] == 'X' {
							// check up left
							if i >= 3 && j >= 3 {
								if matrix[i-1][j-1] == 'M' && 
									 matrix[i-2][j-2] == 'A' && 
									 matrix[i-3][j-3] == 'S' {
										count += 1;
								}
							}
							// check up
							if i >= 3 {
								if matrix[i-1][j] == 'M' && 
									matrix[i-2][j] == 'A' && 
									matrix[i-3][j] == 'S' {
										count += 1;
								}
							}
							// check up right
							if i >= 3 && j + 3 < num_cols {
								if matrix[i-1][j+1] == 'M' && 
										matrix[i-2][j+2] == 'A' && 
										matrix[i-3][j+3] == 'S' {
										count += 1;
								}
							}
							// check right
							if j + 3 < num_cols {
								if matrix[i][j+1] == 'M' && 
										matrix[i][j+2] == 'A' && 
										matrix[i][j+3] == 'S' {
										count += 1;
								}
							}
							// check down right
							if i + 3 < num_rows && j + 3 < num_cols {
								if matrix[i+1][j+1] == 'M' && 
										matrix[i+2][j+2] == 'A' && 
										matrix[i+3][j+3] == 'S' {
										count += 1;
								}
							}
							// check down
							if i + 3 < num_rows {
								if matrix[i+1][j] == 'M' && 
										matrix[i+2][j] == 'A' && 
										matrix[i+3][j] == 'S' {
										count += 1;
								}
							}
							// check down left
							if i + 3 < num_rows && j >= 3 {
								if matrix[i+1][j-1] == 'M' && 
										matrix[i+2][j-2] == 'A' && 
										matrix[i+3][j-3] == 'S' {
										count += 1;
								}
							}
							// check left
							if j >= 3 {
								if matrix[i][j-1] == 'M' && 
										matrix[i][j-2] == 'A' && 
										matrix[i][j-3] == 'S' {
										count += 1;
								}
							}
						}
				}
		}
		count
}

fn turbo_scan(matrix: &Vec<Vec<char>>) -> i32 {
	let num_rows = matrix.len();
	let num_cols = matrix[0].len();

	let mut count = 0;

	for i in 0..num_rows {
		for j in 0..num_cols {
			if matrix[i][j] == 'M' || matrix[i][j] == 'S' {
				// only actually check the possible values
				if bf_check(&matrix, i, j) {
					count += 1;
				}
			}
		}
	}
	count
}

fn bf_check(m: &Vec<Vec<char>>, i: usize, j: usize) -> bool {
	let num_rows = m.len();
	let num_cols = m[0].len();

	// check if the shape can be made
	if i + 2 >= num_rows || j + 2 >= num_cols {
			return false;
	}

	// 1a. the letter is M
	// M.M
	// .A.
	// S.S
	let mm = m[i][j] == 'M' && 
					 m[i+1][j+1] == 'A' && 
					 m[i+2][j+2] == 'S' && 
					 m[i+2][j] == 'M' && 
					 m[i][j+2] == 'S';
	
	// 1b. the letter is M
	// M.S
	// .A.
	// M.S
	let ms = m[i][j] == 'M' && 
					 m[i+1][j+1] == 'A' && 
					 m[i+2][j+2] == 'S' && 
					 m[i+2][j] == 'S' && 
					 m[i][j+2] == 'M';

	// 2a. the letter is S
	// S.S
	// .A.
	// M.M
	let ss = m[i][j] == 'S' && 
					 m[i+1][j+1] == 'A' && 
					 m[i+2][j+2] == 'M' && 
					 m[i+2][j] == 'S' && 
					 m[i][j+2] == 'M';

	// 2b. the letter is S
	// S.M
	// .A.
	// S.M
	let sm = m[i][j] == 'S' && 
					 m[i+1][j+1] == 'A' && 
					 m[i+2][j+2] == 'M' && 
					 m[i+2][j] == 'M' && 
					 m[i][j+2] == 'S';

	mm || ms || ss || sm
}



fn print_matrix(matrix: &Vec<Vec<char>>) {
		for row in matrix {
				for ch in row {
						print!(" [{}] ", ch);
				}
				println!();
		}
}
