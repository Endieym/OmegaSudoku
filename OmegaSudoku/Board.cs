using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku;

internal class Board
{
    private Cell[,] board;
    public int BoardSize;
    public int BoxSize;

    public Board()
    {
        BoardSize = 0;
        BoxSize = 0;
        this.board = new Cell[this.BoardSize, this.BoardSize];
    }
    public Board(string boardString, int boardSize)
    {
        this.BoardSize = boardSize;
        this.BoxSize = (int)Math.Sqrt(boardSize);
        this.board = new Cell[this.BoardSize, this.BoardSize];
        for (int i = 0; i < this.BoardSize; i++)
        {
            for (int j = 0; j < this.BoardSize; j++)
            {
                this.board[i,j] = new Cell(boardString[j+(this.BoardSize * i)],i,j);
            }
        }
    }
    
    public void PrintBoard()
    {
        Console.WriteLine(ToString());
    }
    

    public override string ToString()
    {
        string str = "";
        for (int i = 0; i < this.BoardSize; i++)
        {
            str+=("| ");
            for (int j = 0; j < this.BoardSize; j++)
            {
                str += (board[i, j].Value);
                str += (' ');
                if ((j + 1) % Math.Sqrt(this.BoardSize) == 0) str += (' ');

            }
            str += ("|\n");
            if ((i + 1) % Math.Sqrt(this.BoardSize) == 0) str += (" \n");
        }
        return str;
    }
    public bool BoardValid()
    {
        var setRow = new HashSet<int>();
        var setCol = new HashSet<int>();
        var setBox = new HashSet<int>();

        for (int i = 0; i < this.BoardSize; i++)
        {
            if (CheckRowDups(i)!= -1) return false;
           
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
        for (int i = 0; i < this.BoardSize; i++)
        {

            yield return this.board[row, i];
                
        }
    }
    public IEnumerable<Cell> GetCol(int col)
    {
        for (int i = 0; i < this.BoardSize; i++)
        {

            yield return this.board[i, col];

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
        var iBox = row / this.BoxSize;
        var jBox = col / this.BoxSize;
        for (int i = iBox * this.BoxSize; i < iBox + this.BoxSize; i++)
        {
            for (int j = jBox * this.BoxSize; j < jBox + this.BoxSize; j++)
            {
                yield return this.board[i, j];
            }

        }
    }



}
