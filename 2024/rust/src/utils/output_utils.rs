use std::fs;


pub fn print_output(total: &str, use_test_data: bool) {
	let output_string = if use_test_data {
		"Total [using test data]"
	} else {
		"Total [using puzzle data]"
	};

	println!("{}: {}", output_string, total);
}