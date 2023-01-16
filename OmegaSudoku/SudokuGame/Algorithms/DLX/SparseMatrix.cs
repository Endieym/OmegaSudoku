using OmegaSudoku.SudokuGame.Algorithms.DLX;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.SudokuGame.Algorithms;

internal class SparseMatrix
{
    public class ColumnHeader : DancingNode
    {
        public int Index { get; set; }
        public int Size { get; set; }
        public ColumnHeader() : base(null)
        {
            this.Size = 0;
            this.Column = this;
        }
        public void Cover()
        {
            // Covers the column in the matrix

            this.UnlinkRow(); // L[R[c]] <- L[c]
                              // R[L[c]] <- R[c]

            DancingNode row = this.Down;
            while (row != this)
            {

                // Traverse the nodes connected to the current node (row)
                DancingNode right = row.Right;
                while (right != row)
                {
                    right.UnlinkCol();  // U[D[j]] <- U[j]
                                        // D[U[j]] <- D[j]
                    

                    right.Column.Size--;
                    right = right.Right;
                }
                row = row.Down;
            }
        }

        public void Uncover()
        {
            DancingNode row = this.Up;
            while (row != this)
            {
                DancingNode left = row.Left;
                while (left != row)
                {
                    left.Column.Size++;

                    // This works because the links of the current Node still exist after
                    // removing it from the matrix
                    left.RelinkColumn();  // U[D[j]] <- j
                                          // D[U[j]] <- j

                    left = left.Left;
                }
                row = row.Up;
            }

            this.RelinkRow();  // L[R[c]] = c
                               // R[L[c]] = c   
        }


    }

    public ColumnHeader head;
    public ColumnHeader[] cols;
    public SparseMatrix(int col)
    {
       
        this.head = new ColumnHeader();
        this.cols = new ColumnHeader[col];
        ColumnHeader prev = head;        
        for(int i = 0; i < col; i++)
        {
            cols[i] = new ColumnHeader();
            cols[i].Index = i;

            prev.LinkRight(cols[i]);
            prev = cols[i];

        }


    }

    public void ConvertToDLX(byte[,] matrix)
    {
        for(int i = 0; i < matrix.GetLength(0); i++)
        {
            DancingNode? prev = null;
            for (int j = 0; j < matrix.GetLength(1); j++)
            {
                if (matrix[i, j] == 1) // check if the cell is not empty
                {
                    ColumnHeader currentColumn = cols[j];

                    DancingNode node = new DancingNode(currentColumn);
                    // Insert the new node below the column
                    currentColumn.Up.LinkDown(node);

                    if (prev == null) prev = node;
                    else
                    {
                        prev.LinkRight(node);
                        prev = node;
                    }
                    currentColumn.Size++;
                }
                
            }
        }
    }

  

    
}
