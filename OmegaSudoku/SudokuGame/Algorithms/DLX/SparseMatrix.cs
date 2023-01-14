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
        public int Col { get; set; }
        public Node Left { get; set; }
        public Node Right { get; set; }
        public Node Up { get; set; }
        public Node Down { get; set; }
        public Node Column { get; set; }
        public int Size { get; set; }
    }
    private int rows;
    private int cols;
    public Node head;

    public SparseMatrix(int rows, int cols)
    {
        this.rows = rows;
        this.cols = cols;
        head = new Node();
        Node[] prevs = new Node[cols];

        for(int i = 0; i < cols; i++)
        {
            prevs[i] = new Node();
            prevs[i].Column = prevs[i];
            prevs[i].Size = 0;
            prevs[i].Left = head;
            prevs[i].Right = head.Right;
            head.Right.Left = prevs[i];
            head.Right = prevs[i];
        }
        


    }

    public void AddNode(int row, int col)
    {

    }

    
}
