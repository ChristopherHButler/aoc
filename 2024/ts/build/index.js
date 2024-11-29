"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const puzzles_1 = require("./puzzles");
const currentDate = "01-12-2000"; // (new Date()).toLocaleDateString('en-GB').split('/').join('-');
// console.log(formattedDate);
console.log(`Aoc: ${currentDate}`);
console.log('-----------------\n');
// run puzzle solvers
puzzles_1.day01.solvePart1(currentDate, true);
puzzles_1.day01.solvePart2(currentDate);
