﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku;

internal class Cell
{
    public Cell(int value, int row, int col)
    {
        this.Value = value;
        this.Row = row;
        this.Col = col;
        
    }
    public int Value { get; set; }
    
    public int Row { get; set; }

    public int Col { get; set; }
}