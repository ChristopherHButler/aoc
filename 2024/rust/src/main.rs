
mod puzzles {
	pub mod boilerplate;
}

mod utils {
	pub mod data_file_reader;
}

use chrono::Local;

use puzzles::boilerplate::day_01;


fn main() {

	let now = Local::now();
	let formatted_date = "01-12-2000"; //now.format("%d-%m-%Y").to_string();

	// use_test_data
	day_01(formatted_date, true);
}