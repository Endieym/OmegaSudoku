using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using OmegaSudoku.Exceptions;

namespace OmegaSudoku.SudokuGame;

internal class BoardValidation
{
    public static bool BoardValidate(Board board)
    {
        for (int i = 0; i < board.BoardSize; i++)
        {

            if (board.CheckRowDups(i) != -1) throw new RowException(i);
            if (board.CheckColDups(i) != -1) throw new ColException(i);
        }

        for (int i = 0; i < board.BoardSize; i += board.BoxSize)
        {
            for (int j = 0; j < board.BoardSize; j += board.BoxSize)
            {
                if (board.CheckBoxDups(i, j) != -1) throw new BoxException(i / board.BoxSize, j / board.BoxSize);
            }
        }
        return true;
    }

}
