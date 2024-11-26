use std::fs;
use std::io::{self, Read};


pub fn data_file_reader(date: &str, use_test_data: bool, part: u8) -> Result<Vec<String>, io::Error> {

	let filepath = if use_test_data {
		format!("../../common/data/{}-part{}-test-data.txt", date, part)
	} else {
		format!("../../common/data/{}-data.txt", date)
	};

	match fs::metadata(&filepath) {
		Ok(metadata) => {
			if metadata.is_file() {
				let mut lines = Vec::new();
				let file_contents = fs::read_to_string(&filepath)?;

				for line in file_contents.lines() {
					lines.push(line.to_string());
				}

				Ok(lines)
			
			} else {
				println!("Path exists, but it's not a file");
				Err(io::Error::new(io::ErrorKind::Other, "Path exists, but it's not a file"))
			}
		}
		Err(_) => {
			println!("File does not exist");
			Err(io::Error::new(io::ErrorKind::NotFound, "File does not exist"))
		}
	}
}
