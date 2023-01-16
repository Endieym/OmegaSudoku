using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku.Exceptions;

//Exception classes for illegal boards
[Serializable]

public class BoardException : Exception
{
    public BoardException() { }

    public BoardException(string message) : base("Illegal board input: \n"
        + message)
    { }
    public BoardException(string message, Exception inner) : base("Illegal board input: \n"
        + message, inner)
    { }


}

[Serializable]

public class RowException : BoardException
{
    public RowException(int pos) : base(string.Format("Row {0} has two cells of the same value!", pos)) { }

}
public class ColException : BoardException
{
    public ColException(int pos) : base(string.Format("Col {0} has two cells of the same value!", pos)) { }

}
public class BoxException : BoardException
{
    public BoxException(int i, int j) : base(string.Format("Box [{0},{1}] has two cells of the same value!", i, j)) { }

}

public class UnsolvableBoardException : BoardException
{
    public UnsolvableBoardException() :base("Board is unsolvable!") { }
}


