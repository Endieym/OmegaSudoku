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
        return new Cell(Value, Row, Col)
        {
            PossibleValue = this.PossibleValue
        };
       
    }
    public override bool Equals(object obj)
    {
        var cell = obj as Cell;
        return cell != null &&
               Value == cell.Value &&
               Row == cell.Row &&
               Col == cell.Col &&
               PossibleValue == cell.PossibleValue;
    }

    public override int GetHashCode()
    {
        var hashCode = -822805915;
        hashCode = hashCode * -1521134295 + Value.GetHashCode();
        hashCode = hashCode * -1521134295 + Row.GetHashCode();
        hashCode = hashCode * -1521134295 + Col.GetHashCode();
        hashCode = hashCode * -1521134295 + PossibleValue.GetHashCode();
        return hashCode;
    }
}
