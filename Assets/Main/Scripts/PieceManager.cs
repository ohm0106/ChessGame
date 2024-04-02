using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public enum SelectType
{
    None,
    ChessPiece_Default_White = 100, // 흰색
    ChessPiece_Default_Black = 200, // 블랙
    ChessPiece_White_Selected = 101, // 흰색 체스 말 선택
    ChessPiece_Black_Selected = 201, // 블랙 체스 말 선택
    Board_Default_White = 310,
    Board_Default_Black = 300,
    Board_Selected = 301, // 선택된 보드 판
    Board_Selectable  = 302// 선택 가능한 보드 판 


}

public class PieceManager : Singleton<PieceManager>
{

    [SerializeField]
    ChessMaterialData chessMaterialData;

    Dictionary<SelectType, Material> selectMaterialDics;

    List<ChessPiece> blackChessPieces;
    List<ChessPiece> whiteChessPieces;
    ChessPiece[,] existChessPieces;
    ChessPiece curChessPiece;

    int row = 8;
    int col = 8;
    BoardPiece[,] boardPieces;
    BoardPiece curBoardPieces;

    protected override void InternalAwake()
    {
        base.InternalAwake();
        Init();
    }

    void Start()
    {
        selectMaterialDics = new Dictionary<SelectType, Material>();

        for (int i = 0; i < chessMaterialData.ChessMaterial.Length; i++)
        {
            ChessMaterial chessMaterial = chessMaterialData.ChessMaterial[i];
            selectMaterialDics.Add(chessMaterial.type, chessMaterial.material);
        }
    }

    void Init()
    {
        var boardPiecesTemp = FindObjectsOfType<BoardPiece>();
        boardPieces = new BoardPiece[row, col];
        existChessPieces = new ChessPiece[row, col];

        int r;
        int c;
        foreach (var piece in boardPiecesTemp)
        {
            ConvertNameToIndices(piece.gameObject.name, out r, out c);
            boardPieces[r, c] = piece;
            piece.SetColRow(r, c);
        }

        var chessPiecesTemp = FindObjectsOfType<ChessPiece>();

        blackChessPieces = new List<ChessPiece>();
        whiteChessPieces = new List<ChessPiece>();

        foreach (var arr in chessPiecesTemp)
        {
            if (arr.GetSelectType() == SelectType.ChessPiece_Default_White)
                blackChessPieces.Add(arr);
            else
                whiteChessPieces.Add(arr);

            int[] temp = arr.GetColRow();
            existChessPieces[temp[0], temp[1]] = arr;
        }
    }

    public bool CheckExistChessPieces(int row, int col)
    {
        if (object.ReferenceEquals(existChessPieces[row, col], null))
        {
            return false;
        }
        return true;
    }

    public void SetExistChessPieces(int preRow, int preCol, int curRow, int curCol)
    {
        if (CheckExistChessPieces(preRow, preCol))
        {
            existChessPieces[curRow, curCol] = existChessPieces[preRow, preCol];
            existChessPieces[preRow, preCol] = null;
        }
    }

    void ConvertNameToIndices(string name, out int row, out int col)
    {
        col = name[0] - 'A';

        row = int.Parse(name.Substring(1)) - 1;
       // Debug.Log(name + "/" + row + " / " + col);
    }

    public bool SetSelectableBoard(int rValue, int cValue)
    {
        int r = Mathf.Clamp(rValue, -1, 8);
        int c = Mathf.Clamp(cValue, -1, 8);

        if (c < col &&  c > -1 && r < row && r > -1)
        {
            if (CheckExistChessPieces(r, c))
            {
                // 다른 색의 말이 있다면 해당 말 잡을 수 있게 
                return false;
            }
            else
            {
                boardPieces[r, c].SetSelectableValue(true);

                SetMaterial(GetBoardRenderer(r, c), SelectType.Board_Selectable);
                return true;
            }

        }
        else
        {
            return false;
        }
    }

    Renderer GetBoardRenderer(int row, int col)
    {
        return boardPieces[row, col].GetRenderer();
    }
    BoardPiece GetBoardPiece(int row, int col)
    {
        return boardPieces[row, col];
    }

    public void ResetAllBoard()
    {
        for(int r = 0; r < row ; r++)
        {
            for (int c = 0; c < col; c++)
            {
                //Debug.Log("r" + r + "c" + c);
                Renderer tempRenderer = boardPieces[r, c].GetRenderer();
                SelectType selectType = boardPieces[r, c].GetSelectType();
                if (tempRenderer != selectMaterialDics[selectType])
                {
                    // 딕셔너리에 있는 렌더러랑 같은 것인지 확인 후 bool 값 확인 
                    boardPieces[r, c].SetSelectableValue(false);
                    SetMaterial(tempRenderer, selectType);
                }
            }    
        }
    }

    public void SetMaterial(Renderer renderers , SelectType type = SelectType.None )
    {
        if (type == SelectType.None)
            return;
        
       
        if(selectMaterialDics.ContainsKey(type) && renderers.material != selectMaterialDics[type])
        {
            renderers.material = selectMaterialDics[type];
        }
        else
        {
            Debug.LogError("Empty Material!!");
        }
    }
    SelectType tempType;

    public void SetSelectedMaterial(Renderer renderers, SelectType type = SelectType.None)
    {
        if (type == SelectType.None)
            return;
        else
        {
            switch (type)
            {
                case SelectType.ChessPiece_Default_White:
                    ResetAllBoard();
                    tempType = SelectType.ChessPiece_White_Selected;
                    SetCurrentActiveChessPiece(renderers.GetComponent<ChessPiece>());
                    break;
                case SelectType.ChessPiece_Default_Black:
                    ResetAllBoard();
                    tempType = SelectType.ChessPiece_Black_Selected;
                    SetCurrentActiveChessPiece(renderers.GetComponent<ChessPiece>());
                    break;
                case SelectType.Board_Default_Black:
                case SelectType.Board_Default_White:
                   
                    tempType = SelectType.Board_Selected;
                  
                  
                    curBoardPieces = renderers.GetComponent<BoardPiece>();
                    if (curChessPiece && curChessPiece.GetMoveUp() && curBoardPieces.GetSelectableValue())
                    {
                        Vector3 destination = new Vector3(curBoardPieces.transform.localPosition.x, 1f, curBoardPieces.transform.localPosition.z);
                        int[] temp = curBoardPieces.GetColRow();
                          if(CheckExistChessPieces(temp[0],temp[1]) == false)
                          {
                            if (curChessPiece)
                            {
                                int[] preTemp = curChessPiece.GetColRow();
                            }
                            curChessPiece.SetLocalPosition(destination, temp[0], temp[1]);
                            curChessPiece = null;
                        }
                  
                    }
                    break;
            }
        }
        if (selectMaterialDics.ContainsKey(tempType))
        {
            renderers.material = selectMaterialDics[tempType];
        }
        else
        {
            Debug.LogError("Empty Material!!");
        }
    }

    void SetCurrentActiveChessPiece(ChessPiece chessPiece)
    {
        if (curChessPiece != null)
            curChessPiece.MoveToggle();

        curChessPiece = chessPiece;
    }

    public void SetBoardPiecesNull()
    {
        curBoardPieces = null;
    }


}
