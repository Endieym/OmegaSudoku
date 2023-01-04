using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.SudokuGame;

internal class Board
{
    private Cell[,] board;
    public int BoardSize;
    public int BoxSize;

    public Board()
    {
        BoardSize = 0;
        BoxSize = 0;
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

    public int this[int row, int col]
    {
        get { return board[row,col].Value - '0'; }
        set { board[row,col].Value = (char)(value +'0'); }
    }

    public void PrintBoard()
    {
        Console.WriteLine(ToString());
    }


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
    public bool BoardValid()
    {

        for (int i = 0; i < BoardSize; i++)
        {
            if (CheckRowDups(i) != -1) return false;
            if (CheckColDups(i) != -1) return false;

        }
        for (int i = 0; i < BoardSize; i += BoxSize)
        {
            for (int j = 0; j < BoardSize; j += BoxSize)
            {
                if (CheckBoxDups(i, j) != -1) return false;
            }
        }
        return true;
    }


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

    public IEnumerable<Cell> GetRow(int row)
    {
        for (int i = 0; i < BoardSize; i++)
        {

            yield return board[row, i];

        }
    }
    public IEnumerable<Cell> GetCol(int col)
    {
        for (int i = 0; i < BoardSize; i++)
        {

            yield return board[i, col];

        }
    }
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



}
