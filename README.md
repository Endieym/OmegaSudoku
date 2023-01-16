Sudoku Solver
This project is a Sudoku solver that uses a combination of constraint propagation (human strategy), normal backtracking, and DLX methods to solve the puzzle. The solver can read and write puzzles to files.

Usage
The solver can be run in Visual Studio using .NET 6.0 and C#.

Input
Puzzles can be input in the form of a .txt file with the following format:

9 0 0 0 0 0 0 0 0
0 0 3 6 0 0 0 0 0
0 7 0 0 9 0 2 0 0
0 5 0 0 0 7 0 0 0
0 0 0 0 4 5 7 0 0
0 0 0 1 0 0 0 3 0
0 0 1 0 0 0 0 6 8
0 0 8 5 0 0 0 1 0
0 9 0 0 0 0 4 0 0

Where the numbers represent the given cells in the puzzle and the zeroes represent the blank cells to be filled in.

Output
The solution, if one exists, will be output to the console in the same format as the input.

Methods
The solver uses a combination of the following methods to solve the puzzle:

Constraint Propagation (human strategy)
Normal Backtracking
DLX (Dancing Links X)
Additional Features
This solver can read and write to files, it can solve the sudoku puzzle using 3 different techniques, and it is written in C# using Visual Studio .NET 6.0


Conclusion
The Sudoku solver can be used to solve puzzles of varying difficulty and is a great tool for anyone looking to improve their solving skills or automate the process.
