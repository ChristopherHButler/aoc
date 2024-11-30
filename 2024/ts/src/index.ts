import {
	day01,
} from './puzzles';


const currentDate = "01-12-2000"; // (new Date()).toLocaleDateString('en-GB').split('/').join('-');
// console.log(formattedDate);

console.log(`AoC: ${currentDate}`);
console.log('-----------------\n');

// run puzzle solvers

day01.solvePart1(currentDate, true);
day01.solvePart2(currentDate);