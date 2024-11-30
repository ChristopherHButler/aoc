
mod puzzles {
	pub mod boilerplate;
}

mod utils {
	pub mod data_file_reader;
}

use chrono::Local;

use puzzles::boilerplate::day_00;


fn main() {

	let now = Local::now();
	let formatted_date = now.format("%d-%m-%Y").to_string();

	// testing boilerplate
	let test_date = "01-12-2000";
	day_00(test_date, true);

	// run puzzle solvers
	// use_test_data
	// day_01(formatted_date, true);
}