using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.SudokuGame;

internal class Board : ICloneable, IDisposable
{
    private Cell[,] board;
    public int BoardSize;
    public int BoxSize;
    private bool _disposed;

    public Board(int boardSize)
    {
        this.BoardSize = boardSize;
        this.BoxSize = (int)Math.Sqrt(boardSize);
        this.board = new Cell[BoardSize, BoardSize];
    }
    public Board(string boardString, int boardSize)
    {
        this.BoardSize = boardSize;
        this.BoxSize = (int)Math.Sqrt(boardSize);
        this.board = new Cell[BoardSize, BoardSize];
        
        for (int i = 0; i < BoardSize; i++)
        {
            for (int j = 0; j < BoardSize; j++)
            {
                board[i, j] = new Cell(boardString[j + BoardSize * i], i, j);
                
            }
        }

        
    }
    public Dictionary<int,int>[,] PossibleProtection { get; set; }


    public void UpdateCell(int row, int col, int num)
    {
        UpdateRow(row, num);
        UpdateCol(col, num);
        UpdateBox(row, col, num);


        //int possibleMask = 0;


        //var currRow = GetRow(row);
        //var currCol = GetCol(col);
        //var currBox = GetBox(row, col);
        //int i = 0;
        //while (i < BoardSize)
        //{
        //    possibleMask |= (1 << currRow.ElementAt(i).Value);
        //    possibleMask |= (1 << currCol.ElementAt(i).Value);
        //    possibleMask |= (1 << currBox.ElementAt(i++).Value);

        //}


    }

    public void UpdateRow(int row, int num)
    {
        var currRow = GetRow(row);
        if (num >= 0)
        {
            foreach (var cell in currRow)
            {
                if (cell.Value == '0' )
                {
                    cell.PossibleValue |= (1 << num);
                }

            }
        }
        else
        {
            foreach (var cell in currRow)
            {
                if (cell.Value == '0')
                {
                  
                    
                     cell.PossibleValue &= ~(1 << (-num));


                }

            }
        }
        
    }
    public void UpdateCol(int col, int num)
    {
        var currCol = GetCol(col);
        if(num >= 0)
        {
            foreach (var cell in currCol)
            {
                if (cell.Value == '0')
                {
                    
                    cell.PossibleValue |= (1 << num);

                }
            }
        }
        else
        {
            foreach (var cell in currCol)
            {
                if (cell.Value == '0')
                {

                   
                     cell.PossibleValue &= ~(1 << (-num));
                }
                    

            }
        }
        
    }
    public void UpdateBox(int row, int col, int num)
    {
        var currBox = GetBox(row, col);
        if (num >= 0)
        {
            foreach (var cell in currBox)
            {
                if(cell.Value == '0')
                {
                    if (cell.Row != row && cell.Col != col)
                    {
                        cell.PossibleValue |= (1 << num);

                    }
                }
                
            }
        }
        else
        {
            foreach (var cell in currBox)
            {
                if(cell.Value == '0')
                {
                    if(cell.Row != row && cell.Col != col)
                    {
                      
                            cell.PossibleValue &= ~(1 << (-num));

                    }
                }
                
            }
        }
        
    }

    public int this[int row, int col]
    {
        get { return board[row,col].Value - '0'; }
        set { board[row,col].Value = (char)(value +'0'); }
    }
    public Cell this[int index]
    {
        get { return board[index/this.BoardSize, index % this.BoardSize]; }
        set { board[index / this.BoardSize, index % this.BoardSize].Value = value.Value; }
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
    public IEnumerable<long> GetRowPossibilites(int row)
    {
        for (int i = 0; i < BoardSize; i++)
        {

            yield return board[row, i].PossibleValue;

        }
    }

    public IEnumerable<Cell> GetCol(int col)
    {
        for (int i = 0; i < BoardSize; i++)
        {

            yield return board[i, col];

        }
    }
    public IEnumerable<int> GetColPossibilites(int col)
    {
        for (int i = 0; i < BoardSize; i++)
        {

            yield return board[i, col].PossibleValue;

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

    public IEnumerable<int> GetBoxPossibilites(int row, int col)
    {
        var iBox = row / BoxSize;
        var jBox = col / BoxSize;
        for (int i = iBox * BoxSize; i < (iBox + 1) * BoxSize; i++)
        {
            for (int j = jBox * BoxSize; j < (jBox + 1) * BoxSize; j++)
            {
                yield return board[i, j].PossibleValue;
            }

        }
    }

    public object Clone()
    {
        Board temp = new Board(this.BoardSize);
        for(int i = 0; i < this.BoardSize; i++)
        {
            for(int j = 0; j < this.BoardSize; j++)
            {    
                temp.board[i, j] = (Cell)this.board[i, j].Clone();
            }
        }
        return temp;
    }

    public void Dispose()
    {
        for (int i = 0; i < this.BoardSize; i++)
        {
            for (int j = 0; j < this.BoardSize; j++)
            {
                this.board[i, j] = null;
            }
        }
        // Dispose of unmanaged resources.
        Dispose(true);
        // Suppress finalization.
        GC.SuppressFinalize(this);
    }
    protected virtual void Dispose(bool disposing)
    {
        if (_disposed)
        {
            return;
        }

        if (disposing)
        {
            // TODO: dispose managed state (managed objects).
        }

        // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
        // TODO: set large fields to null.

        _disposed = true;
    }

    public override bool Equals(object? obj)
    {
        var other = obj as Board;
        if (other == null || this.BoardSize != other.BoardSize)
            return false;

        for (int i = 0; i < this.BoardSize; i++)
        {
            for (int j = 0; j < this.BoardSize; j++)
            {
                if (!this.board[i, j].Equals(other.board[i, j]))
                    return false;
            }
        }
        return true;
    }
    public override int GetHashCode()
    {
        var hashCode = -1749122894;
        hashCode = hashCode * -1521134295 + this.BoardSize.GetHashCode();
        hashCode = hashCode * -1521134295 + this.BoxSize.GetHashCode();
        for (int i = 0; i < this.BoardSize; i++)
        {
            for (int j = 0; j < this.BoardSize; j++)
            {
                hashCode = hashCode * -1521134295 + this.board[i, j].GetHashCode();
            }
        }
        return hashCode;
    }
}

