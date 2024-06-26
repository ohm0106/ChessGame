using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

//[ToDoList] 순서대로 할 것. 
// 말 공격 여부 확인 ( 완료 )
// 말 공격 애니메이션 제작 
// 게임 매니저 생성 
// UI 인터페이스 제작 
// Scene Manager 제작 
// Object Pooling 추가
// 알파 베타 가지치기 AI Bot 추가 

public enum SelectType
{
    None,
    ChessPiece_Default_White = 100, // 흰색
    ChessPiece_Default_Black = 200, // 블랙
    ChessPiece_White_Selected = 101, // 흰색 체스 말 선택
    ChessPiece_Black_Selected = 201, // 블랙 체스 말 선택
    ChessPiece_Hit = 001,
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

    List<ChessPiece> chessPieces;
    List<ChessPiece> blackChessPieces;
    List<ChessPiece> whiteChessPieces;
    int[,] existChessPieces;
    ChessPiece curChessPiece;
    ChessPiece whiteKingChess;
    ChessPiece blackKingChess;

    int row = 8;
    int col = 8;
    BoardPiece[,] boardPieces;
    BoardPiece curBoardPieces;

    bool IsBlackTurn { get { return GameManager.Instance.GetTurn(); } }

    protected override void InternalAwake()
    {
        base.InternalAwake();
        
    }

    void Start()
    {
        Init();
        selectMaterialDics = new Dictionary<SelectType, Material>();
        selectMaterialDics.Add(SelectType.None, null);
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
            existChessPieces = new int[row, col];

            int r;
            int c;
            foreach (var piece in boardPiecesTemp)
            {
                ConvertNameToIndices(piece.gameObject.name, out r, out c);
                boardPieces[r, c] = piece;
                piece.SetColRow(r, c);
            }


            var chessPiecesTemp = FindObjectsOfType<ChessPiece>();
            chessPieces = new List<ChessPiece>();
            blackChessPieces = new List<ChessPiece>();
            whiteChessPieces = new List<ChessPiece>();

            chessPieces.AddRange(chessPiecesTemp);

            foreach (var arr in chessPiecesTemp)
            {
                if (arr.GetSelectType() == SelectType.ChessPiece_Default_White)
                {
                    blackChessPieces.Add(arr);
                    if (arr.GetChessPieceType() == ChessPieceType.King)
                        blackKingChess = arr;
                }
                else
                {
                    whiteChessPieces.Add(arr);
                    if (arr.GetChessPieceType() == ChessPieceType.King)
                        whiteKingChess = arr;
                }
                int[] temp = arr.GetColRow();
                existChessPieces[temp[0], temp[1]] = (int)arr.GetChessPieceType();
            }

    }

    public bool CheckExistChessPieces(int r, int c)
    {
        if (existChessPieces[r, c] == 0)
        {
            return false;
        }
        return true;
    }

    public void SetExistChessPieces(int[] perPos, int[] curPos , int value)
    {
        if (CheckExistChessPieces(perPos[0], perPos[1]))
        {
            existChessPieces[curPos[0], curPos[1]] = value;
            existChessPieces[perPos[0], perPos[1]] = 0;

            //Debug.Log("cur" + existChessPieces[curRow, curCol].gameObject.name + " / Check " + (existChessPieces[preRow, preCol] == null));
        }
        else
        {
            existChessPieces[curPos[0], curPos[1]] = value;
        }
    }

    public void SetExistChessPieces(int preR , int preC, int curR , int curC, int value)
    {
        existChessPieces[curR, curC] = value;
        existChessPieces[preR, preC] = 0;
    }

    void ConvertNameToIndices(string name, out int row, out int col)
    {
        col = name[0] - 'A';

        row = int.Parse(name.Substring(1)) - 1;
       // Debug.Log(name + "/" + row + " / " + col);
    }

    public bool SetSelectableBoard(int rValue, int cValue , bool isIgnore = false)
    {
        int r = Mathf.Clamp(rValue, -1, 8);
        int c = Mathf.Clamp(cValue, -1, 8);

        if (c < col &&  c > -1 && r < row && r > -1)
        {
            if (CheckExistChessPieces(r, c))
            {
                // 색깔이 다를 경우 활성화 ( 추후 체스 말 머티리얼을 변경하는 방법도 고려 할 것.) 
                if( !CheckColorBetweenChess(curChessPiece,r,c) && !isIgnore)
                {
                    boardPieces[r, c].SetSelectableValue(true);

                    SetMaterial(GetBoardRenderer(r, c), SelectType.Board_Selectable);

                    GetExistChessPiece(r, c).SetCanHit(false);
                }
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

                if(existChessPieces[r, c] != 0)
                {
                    GetExistChessPiece(r, c).SetCanHit(false);
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
        tempType = SelectType.None;

        if (type == SelectType.None)
            return;
        else
        {
            switch (type)
            {
                case SelectType.ChessPiece_Default_White:
                    if (!IsBlackTurn)
                    {
                        ResetAllBoard();
                        tempType = SelectType.ChessPiece_White_Selected;
                        SetCurrentActiveChessPiece(renderers.GetComponent<ChessPiece>());
                    }
                    break;
                case SelectType.ChessPiece_Default_Black:
                    if (IsBlackTurn)
                    {
                        ResetAllBoard();
                        tempType = SelectType.ChessPiece_Black_Selected;
                        SetCurrentActiveChessPiece(renderers.GetComponent<ChessPiece>());
                    }
                    break;
                case SelectType.ChessPiece_Hit:
                    int[] tempB = renderers.GetComponent<ChessPiece>().GetColRow();
                    if (curChessPiece && curChessPiece.GetMoveUp() && boardPieces[tempB[0], tempB[1]].GetSelectableValue())
                    {
                        Vector3 destination = new Vector3(renderers.transform.localPosition.x, 1f, renderers.transform.localPosition.z);

                        if (CheckExistChessPieces(tempB[0], tempB[1]))
                        {
                            if (!CheckColorBetweenChess(curChessPiece, tempB[0], tempB[1]))
                            {
                                DistroyChessPiece(GetExistChessPiece(tempB[0], tempB[1]));
                                curChessPiece.SetLocalPosition(destination, tempB[0], tempB[1]);
                                //existChessPieces[tempB[0], tempB[1]] = (int)curChessPiece.GetChessPieceType();
                                curChessPiece = null;
                                ResetAllBoard(); 
                            }
                        }

                    }
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
                            //existChessPieces[temp[0], temp[1]] = (int)curChessPiece.GetChessPieceType();
                            curChessPiece = null;
                        }
                        else
                        {
                            if (!CheckColorBetweenChess(curChessPiece, temp[0], temp[1]))
                            {
                                DistroyChessPiece(GetExistChessPiece(temp[0], temp[1]));
                                curChessPiece.SetLocalPosition(destination, temp[0], temp[1]);
                                //existChessPieces[temp[0], temp[1]] = (int)curChessPiece.GetChessPieceType();
                                curChessPiece = null;
                                ResetAllBoard();
                            }
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

    bool CheckColorBetweenChess(ChessPiece chessPiece,int r, int c)
    {
        if (GetExistChessPiece(r, c).GetColor() == chessPiece.GetColor())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    bool CheckColorBetweenChess(ChessPiece chessPiece)
    {
        if (curChessPiece.GetColor() == chessPiece.GetColor())
        {
            return true;
        }
        else
        {
            return false;
        }
    }
    void DistroyChessPiece(ChessPiece chessPiece)
    {
        if (chessPiece == null)
            return;


        (chessPiece.GetColor() ? whiteChessPieces : blackChessPieces).Remove(chessPiece);
        //Todo : 제거되는 체스 말이 '킹'일 때 게임 종료 처리

        Destroy(chessPiece.gameObject);
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
    Renderer GetBoardRenderer(int row, int col)
    {
        return boardPieces[row, col].GetRenderer();
    }
    BoardPiece GetBoardPiece(int row, int col)
    {
        return boardPieces[row, col];
    }

    public bool CheckKing(bool isBlack)
    {
        ChessPiece kingTemp = isBlack ? whiteKingChess : blackKingChess;
        int[] tempPos = kingTemp.GetColRow();

        foreach (var arr in (isBlack ?blackChessPieces : whiteChessPieces))
        {
            if(arr.CheckKing(tempPos[0], tempPos[1]))
            {
                return true;
            }
        }
        return false;
    }

    public int[,] GetChessPieces() { return existChessPieces; }

    public int[,] GetColorChessPieces(bool isBlack)
    {
        int[,] temp = new int[8, 8];

        foreach (var chess in (isBlack? blackChessPieces: whiteChessPieces))
        {
            int[] pos = chess.GetColRow();
            temp[pos[0], pos[1]] = (int)chess.GetChessPieceType();
        }

        return temp;
    }

  
    public ChessPiece GetExistChessPiece(int r, int c)
    {
        foreach(var arr in chessPieces)
        {
            int[] tempPos = arr.GetColRow();
            if (r == tempPos[0] && c == tempPos[1])
                return arr;
        }

        return null;
    }
}
