using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPattern
{
    int col;

    int row;

    public ChessPattern(int row, int col)
    {
        this.row = row;
        this.col = col;
    }

    public int getCol()
    {
        return col;

    }

    public int getRow()
    {
        return row;
    }

}
