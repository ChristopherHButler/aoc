import { PuzzleSolver } from './../types/index';
import { dataFileReader } from '../utils/index';


const solvePart1: PuzzleSolver = (date, useTestData = false) => {
	console.log('Day 01, Part 1');

	const lines = dataFileReader({ date, useTestData, part: 1 });

    const N = 100;
    let pos = 50;
    let zeros = 0;

    for (let i = 0; i < lines.length; i++) {
        const line = lines[i];
        const dir = line[0];
        const dist = parseInt(line.substring(1));

        console.log(`Line: ${line}, dir: ${dir}, dist: ${dist}`);


        if (dir === 'L') {
            pos = (pos - dist) % N;
        } else if (dir === 'R') {
            pos = (pos + dist) % N;
        }
        if (pos < 0) {
            pos += N;
        }
        if (pos === 0) {
            zeros++;
        }
    }

	var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
	console.log(`${outputString}: ${zeros}\n\n`);


};

const solvePart2: PuzzleSolver = (date, useTestData = false) => {
	console.log('Day 01, Part 2');

	const lines = dataFileReader({ date, useTestData, part: 1 });

    const N = 100;
    let pos = 50;
    let zeros = 0;

    for (let i = 0; i < lines.length; i++) {
        const line = lines[i];
        const dir = line[0];
        const dist = parseInt(line.substring(1));

        for (let j = 0; j < dist; j++) {
            if (dir === 'L') {
                pos = (pos - 1 + N) % N;
            } else if (dir === 'R') {
                pos = (pos + 1) % N;
            }
            if (pos === 0) {
                zeros++;
            }
        }
    }

	var outputString = useTestData ? "Total [using test data]" : "Total [using puzzle data]";
	console.log(`${outputString}: ${zeros}\n\n`);


};

export default {
	solvePart1,
	solvePart2
}
