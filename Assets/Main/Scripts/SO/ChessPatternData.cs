using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Chess Pattern Data", menuName = "Scriptable Object/Chess Patter Data", order = int.MaxValue)]
public class ChessPatternData : ScriptableObject
{
    [SerializeField]
    ChessPieceType type;

}

public class ChessPattern
{
    int col;
    int row;

    public ChessPattern(int col, int row)
    {
        this.col = col;
        this.row = row;
    }
    public int Getcol()
    {
        return col;
    }
    public int GetRow()
    {
        return row;
    }
}
