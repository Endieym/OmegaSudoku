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
    private List<int> solution;


    public DancingLinks(SparseMatrix matrix)
    {
        this.matrix = matrix;
        this.solution = new List<int>();
    }

    public List<int> Search()
    {
        if(matrix.head.Right == matrix.head)
            return solution;

        Node ColumnNode = ChooseColumn();
        Cover(ColumnNode);
        Node row = ColumnNode.Down;

        while(row != ColumnNode)
        {
            solution.Add(row.Row);
            Node j = row.Right;
            while(j != row)
            {
                Cover(j.Column);
                j = j.Right;
            }
            Search();
            Node column = row.Column;
            j = row.Left;

            // Uncover the columns that were covered
            while (j != row)
            {
                Uncover(j.Column);
                j = j.Left;
            }

            solution.Remove(row.Row);
            row = row.Down;
        }
        // Uncover the selected column
        Uncover(ColumnNode);
        return null;


    }
    public Node ChooseColumn() // Chooses the best column object to work on
    {
        Node minColNode = matrix.head.Right;
        // finds the column with the least amount of 1's
        for (Node node = matrix.head.Right; node != matrix.head; node = node.Right)
        {
            if (node.Size < minColNode.Size)
                minColNode = node;
        }
        return minColNode;
    }

    public void Cover(Node node)
    {
        // Covers the column in the matrix

        node.Right.Left = node.Left; // L[R[c]] <- L[c]
        node.Left.Right = node.Right; // R[L[c]] <- R[c]

        Node row = node.Down;
        while(row != node)
        {
            Node right =  row.Right;
            while( right != row)
            {
                right.Down.Up = right.Up;
                right.Up.Down = right.Down;
                right.Column.Size--;
                right = right.Right;
            }
            row = row.Down;
        }
    }
    public void Uncover(Node node)
    {
        Node row = node.Up;
        while (row != node)
        {
            Node left = row.Left;
            while(left != row)
            {
                left.Column.Size++;
                // This works because the links of the current Node still exist after
                // removing it from the matrix
                left.Down.Up = left;   // U[D[j]] <- j
                left.Up.Down = left;   // D[U[j]] <- j
                left = left.Left;
            }
            row = row.Up;
        }
        node.Right.Left = node;  // L[R[c]] = c
        node.Left.Right = node;  // R[L[c]] = c   
    }

}
