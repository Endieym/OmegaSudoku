using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku;

internal class Board
{
    private Cell[,] board;

    public Board()
    {
        this.board = new Cell[Constants.BoardScale, Constants.BoardScale];
    }
    public Board(string boardString)
    {
        this.board = new Cell[Constants.BoardScale, Constants.BoardScale];
        for (int i = 0; i < Constants.BoardScale; i++)
        {
            for (int j = 0; j < Constants.BoardScale; j++)
            {
                this.board[i,j] = new Cell(boardString[j+(Constants.BoardScale*i)]-'0',i,j);
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
        for (int i = 0; i < Constants.BoardScale; i++)
        {
            str+=("| ");
            for (int j = 0; j < Constants.BoardScale; j++)
            {
                str += (board[i, j].Value);
                str += (' ');
                if ((j + 1) % 3 == 0) str += (' ');

            }
            str += ("|\n");
            if ((i + 1) % 3 == 0) str += (" \n");
        }
        return str;
    }
    public bool BoardValid()
    {   
        for (int i = 0; i < Constants.BoardScale; i++)
        {
            if (CheckRowDups(i)!= -1) return false;
           
        }
        return true;
    }


    public int CheckRowDups(int row)
    {
        var set = new HashSet<int>(); 
        for (int i = 0; i < Constants.BoardScale; i++)
        {
           
            if (!set.Add(this.board[row,i].Value)&& this.board[row, i].Value != 0)
                return i;
        }
        return -1;
    }
    public int CheckColDups(int col)
    {
        var set = new HashSet<int>();
        for (int i = 0; i < Constants.BoardScale; i++)
        {
            if (!set.Add(this.board[i, col].Value) && this.board[i, col].Value != 0)
                return i;
        }
        return -1;
    }

    public int CheckBoxDups(int iBox, int jBox)
    {
        var set = new HashSet<int>();
        for (int i = iBox; i < iBox+Constants.BoxSize; i++)
        {
            for(int j = jBox; j < jBox + Constants.BoxSize; j++)
            {
                if(!set.Add(this.board[i, j].Value) && this.board[i, j].Value != 0)
                return i;
            }
            
        }
        return -1;
    }

    

}
