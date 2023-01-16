# Sudoku Solver
This project is a Sudoku solver that uses a combination of constraint propagation (human strategy), normal backtracking, and DLX methods to solve the puzzle. The solver can read and write puzzles to files.

## Usage
The solver can be run in Visual Studio using .NET 6.0 and C#.

## Input
Puzzles can be input in the form of a .txt file with the following format:

_800000070006010053040600000000080400003000700020005038000000800004050061900002000
100000027000304015500170683430962001900007256006810000040600030012043500058001000_


Where the numbers represent the given cells in the puzzle and the zeroes represent the blank cells to be filled in.

## Output
The solution, if one exists, will be output to the console.
```
9 0 0 0 0 0 0 0 0
0 0 3 6 0 0 0 0 0
0 7 0 0 9 0 2 0 0
0 5 0 0 0 7 0 0 0
0 0 0 0 4 5 7 0 0
0 0 0 1 0 0 0 3 0
0 0 1 0 0 0 0 6 8
0 0 8 5 0 0 0 1 0
0 9 0 0 0 0 4 0 0
```

## Methods
The solver uses a combination of the following methods to solve the puzzle:

- **Constraint Propagation (human strategy):**  This method involves making logical deductions about the possible values of empty cells based on the given numbers in the puzzle. It is a technique that humans often use when solving Sudoku puzzles by hand.

-  **Normal Backtracking:** This method involves trying different numbers in empty cells and backtracking when a contradiction is found. It is a simple, but potentially time-consuming, method for solving Sudoku puzzles.

-  **DLX (Dancing Links X):** This method is a specific implementation of Algorithm X, a more efficient version of the backtracking algorithm. It uses a technique called "dancing links" to keep track of the possible values for each cell and make deductions about which numbers can be placed in which cells.

The solver uses the DLX and normal backtracking methods simultaneously and will use whichever method solves the puzzle faster.

## Additional Features
This solver can read and write to files, it can solve the sudoku puzzle using 3 different techniques, and it is written in C# using Visual Studio .NET 6.0

## Conclusion
The Sudoku solver can be used to solve puzzles of varying difficulty and is a great tool for anyone looking to improve their solving skills or automate the process.
