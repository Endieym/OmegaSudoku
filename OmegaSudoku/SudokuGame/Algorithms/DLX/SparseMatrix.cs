using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.SudokuGame.Algorithms;

internal class SparseMatrix
{
    public class Node      
    {
        public int Row { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Up { get; set; }
        public Node Down { get; set; }
        public Node Column { get; set; }
        public int Size { get; set; }
    }
    private int rowSize;
    private int colSize;
    public Node head;
    public Node[] cols;
    public SparseMatrix(int row, int col)
    {
        this.rowSize = row;
        this.colSize = col;
        this.head = new Node();
        this.cols = new Node[col];

        for(int i = 0; i < col; i++)
        {
            cols[i] = new Node();
            cols[i].Column = cols[i];
            cols[i].Size = 0;
            cols[i].Left = head;
            cols[i].Right = head.Right;
            head.Right.Left = cols[i];
            head.Right = cols[i];
        }
        


    }

    public void AddNode(int row, int col)
    {
        Node node = new Node();
        node.Row = row;
        node.Column = cols[col];
        node.Column.Size++;
        // Link the node to the column
        node.Up = node.Column.Up;
        node.Down = node.Column;
        node.Up.Down = node;
        node.Up = node;
        // Link the node from the left and right
        node.Left = node.Column.Left;
        node.Right = node.Column.Right;
        cols[col].Left.Right = cols[col];
        cols[col].Right.Left = cols[col];
        
    }

    
}
