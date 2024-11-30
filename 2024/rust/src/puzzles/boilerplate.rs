
use crate::utils::data_file_reader::data_file_reader;

pub fn day_00(date: &str, use_test_data: bool) {

	// part 1 (used in data file name)
	match data_file_reader(date, use_test_data, 1, false) {
		Ok(lines) => {
			for line in lines {
				println!("{}", line);
			}
		}
		Err(_) => {
			println!("Error reading file");
		}
	}
}

