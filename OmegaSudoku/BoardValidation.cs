using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace OmegaSudoku;

internal class BoardValidation
{
    public static bool BoardValidate(Board board)
    {
        for (int i = 0; i < Constants.BoardScale; i++)
        {
            if (board.CheckRowDups(i) != -1) throw new RowException(i);
            if (board.CheckColDups(i) != -1) throw new ColException(i);
        }

        for (int i = 0; i < Constants.BoardScale; i += 3)
        {
            for(int j = 0; j < Constants.BoardScale; j += 3)
            {
                if (board.CheckBoxDups(i, j) != -1) throw new BoxException(i/3 + 1, j/3 + 1);
            }
        }
        return true;
    }

}
