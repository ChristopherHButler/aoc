import {
	boilerplate,
	day01,
	day02,
} from './puzzles';


const currentDate = (new Date()).toLocaleDateString('en-GB').split('/').join('-');

console.log(`AoC: ${currentDate}`);
console.log('-----------------\n');

// test boilerplate
// const testDate = "01-12-2000";
// boilerplate.solvePart1(testDate, true);
// boilerplate.solvePart2(testDate);

// run puzzle solvers

// day01.solvePart1(currentDate); // ans: 3508942
// day01.solvePart2(currentDate); // ans: 26593248

day02.solvePart1(currentDate, true); // ans: 6730673
day02.solvePart2(currentDate); // ans: 3749