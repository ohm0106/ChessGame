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

public class SelectedManager : MonoBehaviour
{
    
    [SerializeField]
    ChessMaterialData chessMaterialData;

    Dictionary<SelectType, Material> selectMaterialDics;

    private void Start()
    {
        selectMaterialDics = new Dictionary<SelectType, Material>();
        for (int i = 0; i < chessMaterialData.ChessMaterial.Length; i++)
        {
            ChessMaterial chessMaterial = chessMaterialData.ChessMaterial[i];
            selectMaterialDics.Add(chessMaterial.type, chessMaterial.material);
        }
    }

    public void SetMaterial(Renderer renderers , SelectType type = SelectType.None )
    {
        if (type == SelectType.None)
            return;
        
       
        if(selectMaterialDics.ContainsKey(type))
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
                    tempType = SelectType.ChessPiece_White_Selected;
                    break;
                case SelectType.ChessPiece_Default_Black:
                    tempType = SelectType.ChessPiece_Black_Selected;

                    break;
                case SelectType.Board_Default_Black:
                case SelectType.Board_Default_White:
                    tempType = SelectType.Board_Selected;

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

}
