using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    bool isBlackTurn;

   public void CheckPos()
   {
         bool isCheck =  PieceManager.Instance.CheckKing(isBlackTurn);
        if (isCheck)
            Debug.LogError("isCHECK!!!!!!!!!!!!!!!!!!!!");
   }
}
