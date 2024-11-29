"use strict";
Object.defineProperty(exports, "__esModule", { value: true });
const index_1 = require("../utils/index");
const solvePart1 = (date, useTestData = true) => {
    console.log('Day 01, Part 1');
    // to do: implementation
    const lines = (0, index_1.dataFileReader)({ date, useTestData, part: 1 });
    lines.map(line => console.log(line));
    console.log('The answer is...');
};
const solvePart2 = (date, useTestData = true) => {
    console.log('Day 01, Part 2');
    // to do: implementation
    console.log('The answer is...');
};
exports.default = {
    solvePart1,
    solvePart2
};
