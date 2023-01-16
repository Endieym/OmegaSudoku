using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.SudokuGame;

public class Cell
{
    /// <summary>
    /// Class which represents a single cell in the sudoku board
    /// </summary>
    /// <param name="value"></param>
    /// <param name="row"></param>
    /// <param name="col"></param>
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


    public object Clone() // Clone the object, returns the cloned object
    {
        return new Cell(Value, Row, Col)
        {
            PossibleValue = this.PossibleValue
        };

    }
}
