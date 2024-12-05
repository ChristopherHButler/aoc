#![allow(dead_code)]
#![allow(unused_variables)]
#![allow(unused_imports)]
#![allow(non_snake_case)]
mod puzzles {
	pub mod boilerplate;
	pub mod dec01;
	pub mod dec04;
}

mod utils {
	pub mod data_file_reader;
	pub mod output_utils;
}

use chrono::Local;

use puzzles::boilerplate;
use puzzles::dec01;
use puzzles::dec04;

fn main() {

	let now = Local::now();
	let current_date = now.format("%d-%m-%Y").to_string();

	// test boilerplate
	// let test_date = "01-12-2000";
	// boilerplate::solvePart1(test_date, true);
	// boilerplate::solvePart2(test_date, true);

	// run puzzle solvers
	// use_test_data
	// dec01::solvePart1(&current_date, false); // ans: 3508942
	// dec01::solvePart2(&current_date, false); // ans: 26593248

	// dec04::solvePart1("04-12-2024", false); // ans: 2464
	dec04::solvePart2("04-12-2024", false); // ans: 1982
}