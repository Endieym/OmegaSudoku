using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OmegaSudoku.SudokuGame.Algorithms.SparseMatrix;

namespace OmegaSudoku.SudokuGame.Algorithms.DLX;

internal class DancingLinks
{
    private SparseMatrix matrix;
    private Stack<DancingNode> solution;


    public DancingLinks(SparseMatrix matrix)
    {
        this.matrix = matrix;
        this.solution = new Stack<DancingNode>();
    }

    public Stack<DancingNode> DancingSolve()
    {
        this.Search();
        return this.solution;
    }

    public bool Search()
    {
        if(matrix.head.Right == matrix.head)
            return true;

        ColumnHeader ColumnNode = ChooseColumn();
        ColumnNode.Cover();

        DancingNode row = ColumnNode.Down;

        while(row != ColumnNode)
        {
            solution.Push(row);

            DancingNode j = row.Right;
            while(j != row)
            {
                j.Column.Cover();
                j = j.Right;
            }

            if (Search())
                return true;

            row = solution.Pop();

            ColumnNode = row.Column;
            j = row.Left;

            // Uncover the columns that were covered
            while (j != row)
            {
                j.Column.Uncover();
                j = j.Left;
            }

            
            row = row.Down;
        }
        // Uncover the selected column
        ColumnNode.Uncover();
        return false;

    }

    public ColumnHeader ChooseColumn() // Chooses the best column object to work on
    {

        ColumnHeader minColNode = (ColumnHeader)matrix.head.Right;
        // finds the column with the least amount of 1's
        ColumnHeader node = (ColumnHeader)matrix.head.Right;

        while(node != matrix.head){
            if (node.Size < minColNode.Size)
                minColNode = node;

            node = (ColumnHeader)node.Right;
        }
        return minColNode;
        
        //for (ColumnHeader node = (ColumnHeader)matrix.head.Right; node != matrix.head; node = (ColumnHeader)node.Right)
        //{
        //    if (node.Size < minColNode.Size)
        //        minColNode = node;
        //}
        //return minColNode;
    }

    //public void Cover(ColumnHeader node)
    //{
    //    // Covers the column in the matrix

    //    node.Right.Left = node.Left; // L[R[c]] <- L[c]
    //    node.Left.Right = node.Right; // R[L[c]] <- R[c]

    //    DancingNode row = node.Down;
    //    while(row != node)
    //    {
    //        DancingNode right =  row.Right;
    //        while( right != row)
    //        {
    //            right.Down.Up = right.Up;
    //            right.Up.Down = right.Down;

    //            right.Column.Size--;
    //            right = right.Right;
    //        }
    //        row = row.Down;
    //    }
    //}
    //public void Uncover(ColumnHeader node)
    //{
    //    DancingNode row = node.Up;
    //    while (row != node)
    //    {
    //        DancingNode left = row.Left;
    //        while(left != row)
    //        {
    //            left.Column.Size++;
    //            // This works because the links of the current Node still exist after
    //            // removing it from the matrix
    //            left.Down.Up = left;   // U[D[j]] <- j
    //            left.Up.Down = left;   // D[U[j]] <- j
    //            left = left.Left;
    //        }
    //        row = row.Up;
    //    }
    //    node.Right.Left = node;  // L[R[c]] = c
    //    node.Left.Right = node;  // R[L[c]] = c   
    //}

}
