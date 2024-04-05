using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : Singleton<GameManager>
{
    [SerializeField]
    bool isBlackTurn;

    [SerializeField]
    bool isWorking;

    void Start()
    {
        StartGame();
    }

    public void CheckPos()
   {
        bool isCheck =  PieceManager.Instance.CheckKing(isBlackTurn);
        if (isCheck)
            Debug.LogError("isCHECK!!!!!!!!!!!!!!!!!!!!");
   }

    void StartGame()
    {
        isWorking = true;
        isBlackTurn = true;
        //검정말이 먼저 시작 
        StartCoroutine(CoStartGame());
    }

    IEnumerator CoStartGame()
    {
        bool preTurn = !isBlackTurn;
        while (isWorking)
        {
            //Black Turn 
            yield return new WaitUntil(() => !isBlackTurn); 
            //White Turn
            Debug.Log("Turn CHange!!!!!!");
            yield return new WaitUntil(() => isBlackTurn);
            Debug.Log("Turn CHange!!!!!!");
        }

    }


    public bool GetTurn() { return isBlackTurn; }
    public void SetTurn(bool isBlack) { isBlackTurn = isBlack; }
}
