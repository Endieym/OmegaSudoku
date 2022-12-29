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
        for (int i = 0; i < this.BoardSize; i++)
        {
            if (CheckRowDups(i)!= -1) return false;
           
        }
        return true;
    }


    public int CheckRowDups(int row)
    {
        var set = new HashSet<int>(); 
        for (int i = 0; i < this.BoardSize; i++)
        {
           
            if (!set.Add(this.board[row,i].Value)&& this.board[row, i].Value != '0')
                return i;
        }
        return -1;
    }
    public int CheckColDups(int col)
    {
        var set = new HashSet<int>();
        for (int i = 0; i < this.BoardSize; i++)
        {
            if (!set.Add(this.board[i, col].Value) && this.board[i, col].Value != '0')
                return i;
        }
        return -1;
    }

    public int CheckBoxDups(int iBox, int jBox)
    {
        var set = new HashSet<int>();
        for (int i = iBox; i < iBox+ this.BoxSize; i++)
        {
            for(int j = jBox; j < jBox + this.BoxSize; j++)
            {
                if(!set.Add(this.board[i, j].Value) && this.board[i, j].Value != '0')
                return i;
            }
            
        }
        return -1;
    }

    

}
