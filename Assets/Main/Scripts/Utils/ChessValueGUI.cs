using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PieceManager))]
public class ChessValueGUI : MonoBehaviour
{
    float squareSize = 50f; 
    float offsetX = 10f;  
    float offsetY = 10f;


    int[,] temp; 


    private void OnGUI()
    {
#if UNITY_EDITOR
        temp = PieceManager.Instance.GetChessPieces();

        GUIStyle textStyle = new GUIStyle();
        textStyle.fontSize = 20;
        textStyle.normal.textColor = Color.black;

        for (int i = 0; i < 8; i++)
        {
            for (int j = 0; j < 8; j++)
            {
                float x = offsetX + j * squareSize;
                float y = offsetY + i * squareSize;

                Rect squareRect = new Rect(x, y, squareSize, squareSize);
                GUI.Box(squareRect, "");

                GUI.Label(new Rect(x, y, squareSize, squareSize), temp[i, j].ToString(), textStyle);
            }
        }
#endif
    }
}
