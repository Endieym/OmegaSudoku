using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.SudokuGame;

internal class Cell : ICloneable
{
    public Cell(char value, int row, int col)
    {
        Value = value;
        Row = row;
        Col = col;   
        PossibleValue = 0;

    }

    public int PossibleValue { get; set; }

    public char Value { get; set; }

    public int Row { get; set; }

    public int Col { get; set; }

    public object Clone()
    {
        return new Cell(Value, Row, Col);
    }
}
