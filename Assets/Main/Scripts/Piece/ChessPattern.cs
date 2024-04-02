using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChessPattern
{
    public int up;
    public int down;
    public int left;
    public int right;
    public int upleft;
    public int upright;
    public int downleft;
    public int downright;

    public ChessPattern(int up, int down, int left, int right, int upleft, int upright, int downleft, int downright)
    {
        this.up = up;
        this.down = down;
        this.left = left;
        this.right = right;
        this.upleft = upleft;
        this.upright = upright;
        this.downleft = downleft;
        this.downright = downright;
    }

}
