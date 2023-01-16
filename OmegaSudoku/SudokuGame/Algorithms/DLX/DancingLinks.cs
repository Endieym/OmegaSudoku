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

    /// <summary>
    /// The search algorithm of DLX
    /// </summary>
    /// <returns>Whether a solution was found or not</returns>
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

        for (ColumnHeader node = (ColumnHeader)matrix.head.Right; node != matrix.head; node = (ColumnHeader)node.Right)
        {
            if (node.Size < minColNode.Size)
                minColNode = node;
        }
        return minColNode;
    }

    

}
