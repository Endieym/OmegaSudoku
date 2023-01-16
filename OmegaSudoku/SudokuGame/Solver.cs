using OmegaSudoku.Exceptions;
using OmegaSudoku.SudokuGame.Algorithms.Backtracking;
using OmegaSudoku.SudokuGame.Algorithms.ConstraintPropagation;
using OmegaSudoku.SudokuGame.Algorithms.DLX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.SudokuGame;

public class Solver
{
    public static bool solutionFound = false;
    private Board gameBoard;

    public Solver(Board gameBoard)
    {
        this.gameBoard = gameBoard;
    }
  


    /// <summary>
    /// Function which tries to solve the sudoku puzzle
    /// Does it in two seperate threads
    /// Tries DLX and Normal backtracking at the same time
    /// Whichever is faster will be used.
    /// </summary>
    /// <returns>returns the string of the solution</returns>
    /// <exception cref="UnsolvableBoardException"></exception>
    public string Solve()
    {
        ConstraintSolve();

        var cts = new CancellationTokenSource();

        Task task1 = new Task(() => DlxBacktrack(cts.Token),cts.Token);
        Task task2 = new Task(() => BacktrackNormal(cts.Token), cts.Token);

        // starts the threads
        task1.Start();
        task2.Start();

        while (!solutionFound)
        {
            if (task1.Wait(1))
            {
                cts.Cancel();
                break;
            }
            if (task2.Wait(1))
            {
                cts.Cancel();
                break;
            }
        }
        if (!solutionFound)
            throw new UnsolvableBoardException();
        return this.gameBoard.ToStringLine();

    }

    public Board GetBoard() // getter for the board
    {
        return this.gameBoard;
    }
    private bool DlxBacktrack(CancellationToken ct) // Calls for dlx algorithm
    {
        Board puzzleTemp = (Board)this.gameBoard.Clone();
        bool flag = SudokuDLX.Solve(puzzleTemp);
        if (!solutionFound && flag)
        {
            Console.WriteLine("DLX SOLVED");
            solutionFound = true;
            this.gameBoard = puzzleTemp;
            ct.ThrowIfCancellationRequested();
            return true;
        }
        return false;
       

    }
    public bool BacktrackNormal(CancellationToken ct) // calls for backtracking algorithm
    {
        Board puzzleTemp = (Board)this.gameBoard.Clone();
        bool flag = BacktrackSolve(puzzleTemp);
        if (!solutionFound && flag)
        {
            Console.WriteLine("BACKTRACK SOLVED");
            solutionFound = true;
            this.gameBoard = puzzleTemp;
            
            ct.ThrowIfCancellationRequested();
            return true;
        }
        
        return false;
        
        
        

    }


    // A function to solve the puzzle using constraint propagation
    public void ConstraintSolve()
    {
        for(int row = 0; row < gameBoard.BoardSize; row++)
        {
            for(int col =0; col < gameBoard.BoardSize; col++)
            {
                if (gameBoard[row, col] != 0)
                {
                    SudokuStrategies.MarkPossibilities(row, col, gameBoard[row, col], gameBoard);

                }

            }
        }
        ConstraintPropagation.ConstraintSolve(gameBoard);
    }



    public bool BacktrackSolve(Board puzzle) // calls for backtracking
    {
        if (!Backtracking.BacktrackSolve(puzzle))
            return false;
        return true;

    }    

}
