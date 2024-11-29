# AoC 2024 - TypeScript edition

This is a TypeScript implementation of Advent of Code 2024.

## About 

 - This project uses:
   - `TypeScript` as the programming language
	 - `esbuild` as the typescript compiler
	 - `node` and `npm` to make using npm packages easier
	 - `concurrently` to run multiple scripts simultaneously
	 - `nodemon` to automatically re-run the project when file changes are detected

 - `./src/index.ts` is the entry point to the program which allows you to run all puzzles.

 - Puzzles are implemented as functions

 - Just about every day's puzzle relies on the `dataFileReader` util.



## Getting Started

1. If you do not have node installed follow [these steps](https://nodejs.org/en/download/package-manager) to install.

2. To install the typescript compiler:

```
npm install -g typescript ts-node
```

verify with:

```
tsc --help
```

3. To run the project:

```
npm run start
```


## Running the puzzle solvers

