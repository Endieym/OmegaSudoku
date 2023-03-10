using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static OmegaSudoku.SudokuGame.Algorithms.SparseMatrix;

namespace OmegaSudoku.SudokuGame.Algorithms.DLX
{
    /// <summary>
    /// This class represents a single node in the dancing links matrix
    /// </summary>
    internal class DancingNode
    {
        public DancingNode Left { get; set; }
        public DancingNode Right { get; set; }
        public DancingNode Up { get; set; }
        public DancingNode Down { get; set; }
        public ColumnHeader Column { get; set; }

        public DancingNode(ColumnHeader head)
        {
            this.Left = this;
            this.Right = this;
            this.Up = this;
            this.Down = this;
            this.Column = head;
        }
        /// <summary>
        /// Links the node given down from the current node
        /// </summary>
        /// <param name="node">The node to connect</param>
        public void LinkDown(DancingNode node)
        {
            node.Down = this.Down;
            node.Up = this;
            this.Down.Up = node;
            this.Down = node;
        }
        /// <summary>
        /// Links the node given to the right of the current node
        /// </summary>
        /// <param name="node">The node to connect</param>
        public void LinkRight(DancingNode node)
        {
            node.Right = this.Right; 
            node.Left = this;
            this.Right.Left = node;
            this.Right = node;
        }
        /// <summary>
        /// Unlinks the node from its row
        /// </summary>
        public void UnlinkRow()
        {
            this.Right.Left = this.Left;
            this.Left.Right = this.Right;

        }
        /// <summary>
        /// Unlinks the node from its column
        /// </summary>
        public void UnlinkCol()
        {
            this.Down.Up = this.Up;
            this.Up.Down = this.Down;

        }
        /// <summary>
        /// Relinks the node to its previous row
        /// </summary>
        public void RelinkRow()
        {
            this.Left.Right = this.Right.Left = this;

        }
        /// <summary>
        /// Relinks the node to its previous column
        /// </summary>
        public void RelinkColumn()
        {
            this.Up.Down = this.Down.Up = this;

        }


    }
}
