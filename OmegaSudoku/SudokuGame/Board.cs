using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.SudokuGame;

public class Board 
{
    private Cell[,] board;
    public int BoardSize;
    public int BoxSize;

    public Board(int boardSize)
    {
        BoardSize = boardSize;
        BoxSize = (int)Math.Sqrt(boardSize);
        board = new Cell[BoardSize, BoardSize];
    }
    public Board(string boardString, int boardSize)
    {
        BoardSize = boardSize;
        BoxSize = (int)Math.Sqrt(boardSize);
        board = new Cell[BoardSize, BoardSize];
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                board[i, j] = new Cell(boardString[j + BoardSize * i], i, j);
            }
        }
    
    }
    
    public string ToStringLine() // returns the board in a string line
    {
        string result = "";
        for(int i =0; i < BoardSize; i++)
        {
            for(int j = 0; j < BoardSize; j++)
            {
                result += this.board[i, j].Value;
            }
        }
        return result;
    }
    
    public void UpdateRow(int row, int num) // Marks the entire row with the possible value num
    {
        var currRow = GetRow(row);
        foreach (var cell in currRow)
        {
            cell.PossibleValue |= 1 << num;
        }
    }
    public void UpdateCol(int col, int num) // Marks the entire row with the possible value num

    { 
        var currCol = GetCol(col);
        foreach (var cell in currCol)
        {
            cell.PossibleValue |= 1 << num;
        }
    }
    public void UpdateBox(int row, int col, int num) // Marks the entire box with the possible value num
    {
        var currBox = GetBox(row, col);
        foreach (var cell in currBox)
        {
            cell.PossibleValue |= 1 << num;
        }
    }

    // Indexer for specific board value (in int)
    public int this[int row, int col]
    {
        get { return board[row,col].Value - '0'; }
        set { board[row,col].Value = (char)(value +'0'); }
    }
    // Indexer for specific board cell
    public Cell this[int index]
    {
        get { return board[index/this.BoardSize, index % this.BoardSize]; }
        set { board[index / this.BoardSize, index % this.BoardSize].Value = value.Value; }
    }

    
    public void PrintBoard() // Prints the board
    {
        Console.WriteLine(ToString());
    }

    // ToString method
    public override string ToString()
    {
        string str = "";
        for (int i = 0; i < BoardSize; i++)
        {
            str += "| ";
            for (int j = 0; j < BoardSize; j++)
            {
                str += board[i, j].Value;
                str += ' ';
                if ((j + 1) % Math.Sqrt(BoardSize) == 0) str += ' ';

            }
            str += "|\n";
            if ((i + 1) % Math.Sqrt(BoardSize) == 0) str += " \n";
        }
        return str;
    }
    
    //Checks whether there are duplicates in the row, returns the row where there was a duplicate
    public int CheckRowDups(int row)
    {
        var currRow = GetRow(row);
        var set = new HashSet<int>();
        int i = 0;
        foreach (var cell in currRow)
        {
            if (!set.Add(cell.Value) && cell.Value != '0')
                return i;
            i++;
        }
        return -1;
    }

    /// <summary>
    /// Creates an IEnumerable list that contains every cell in the row 
    /// </summary>
    /// <param name="row"></param>
    /// <returns>The IEnumerable</returns>
    public IEnumerable<Cell> GetRow(int row)
    {
        for (int i = 0; i < BoardSize; i++)
        {

            yield return board[row, i];

        }
    }

    /// <summary>
    /// Creates an IEnumerable list that contains every cell in the column 
    /// </summary>
    /// <param name="col"></param>
    /// <returns>The IEnumerable</returns>
    public IEnumerable<Cell> GetCol(int col)
    {
        for (int i = 0; i < BoardSize; i++)
        {

            yield return board[i, col];

        }
    }


    //Checks whether there are duplicates in the column, returns the column
    //index where there was a duplicate

    public int CheckColDups(int col)
    {
        var currCol = GetCol(col);
        var set = new HashSet<int>();
        int i = 0;
        foreach (var cell in currCol)
        {
            if (!set.Add(cell.Value) && cell.Value != '0')
                return i;
            i++;
        }
        return -1;
    }
    //Checks whether there are duplicates in the box of specific index, returns the box index
    //where there was a duplicate
    public int CheckBoxDups(int row, int col)
    {
        var currBox = GetBox(row, col);
        var set = new HashSet<int>();
        int i = 0;
        foreach (var cell in currBox)
        {
            if (!set.Add(cell.Value) && cell.Value != '0')
                return i;
            i++;
        }
        return -1;
    }

    public int GetBoxIndex(int row, int col)
    {
        return (row / BoxSize) * BoxSize + col / BoxSize;
    }

    /// <summary>
    /// Creates an IEnumerable list that contains every cell in the box 
    /// that board[row,col] belongs to.
    /// </summary>
    /// <param name="row"></param>
    /// <param name="col"></param>
    /// <returns>The IEnumerable</returns>
    public IEnumerable<Cell> GetBox(int row, int col)
    {
        var iBox = row / BoxSize;
        var jBox = col / BoxSize;
        for (int i = iBox * BoxSize; i < (iBox +1) *BoxSize; i++)
        {
            for (int j = jBox * BoxSize; j < (jBox +1)* BoxSize; j++)
            {
                yield return board[i, j];
            }

        }
    }

    /// <summary>
    /// Clones the board object, cloning its cells aswell
    /// </summary>
    /// <returns>the new cloned object</returns>
    public object Clone() 
    {
        Board temp = new Board(this.BoardSize);
        for (int i = 0; i < this.BoardSize; i++)
        {
            for (int j = 0; j < this.BoardSize; j++)
            {
                temp.board[i, j] = (Cell)this.board[i, j].Clone();
            }
        }
        return temp;
    }

}
