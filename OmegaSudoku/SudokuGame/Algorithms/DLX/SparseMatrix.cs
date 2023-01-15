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

    Node FindAbove(int row, int col)
    {
        Node temp, tempCol;
        int i;

        temp = head.Right;
        while (--col != 0)
        {
            temp = temp.Right;


        }
        tempCol = temp;
        while (temp.Down.Row < row && temp != tempCol)
        {
            temp = temp.Down;

        }
        return temp;


    }

    Node FindBefore(int row, int col)
    {
        Node temp, tempRow;
        int i;

        temp = head.Down;
        while (--row != 0)
        {
            temp = temp.Down;

        }
        tempRow = temp;
        while (temp.Right.Column != cols[col] && temp != tempRow)
        {
            temp = temp.Right;

        }
        return temp;


    }

    public void AddNode(int row, int col)
    {
        Node node = new Node();
        node.Row = row;
        node.Column = cols[col];
        node.Column.Size++;
        Node nodeBefore, nodeAbove;
        nodeBefore= FindBefore(row, col);
        nodeAbove = FindAbove(row, col);
        // Link the node to the column
        node.Up = nodeAbove;
        node.Down = nodeAbove.Down;
        nodeAbove.Down.Up = node;
        nodeAbove.Down = node;
        // Link the node from the left and right
        node.Left = nodeBefore;
        node.Right = nodeBefore.Right;
        nodeBefore.Right.Left = node;
        nodeBefore.Right = node;

    }

    
}
