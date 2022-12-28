using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku;

[Serializable]

internal class BoardException : Exception
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

internal class RowException : BoardException
{
    public RowException(int pos) : base(String.Format("Row {0} has two cells of the same value!", pos)) { }

}
internal class ColException : BoardException
{
    public ColException(int pos) : base(String.Format("Col {0} has two cells of the same value!", pos)) { }

}
internal class BoxException : BoardException
{
    public BoxException(int i, int j) : base(String.Format("Box [{0},{1}] has two cells of the same value!", i,j )) { }

}


