using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMangerScript : MonoBehaviour
{
    //配列の作成
    int[] map;

    // Start is called before the first frame update
    void Start()
    {
        //配列の実態の作成と初期化
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            //メソット化した処理を使用
            int playerIndex = GetPlayerIndex();

            //移動処理を関数化
            MoveNumber(1, playerIndex, playerIndex + 1);
            PrintArray();
        }
    }

    void PrintArray()
    {
        //文字列の宣言と初期化
        string debugText = "";

        for(int i = 0; i < map.Length; i++)
        {
            //文字列を連携していく
            debugText += map[i].ToString() + ",";
        }

        //結合した文字列を出力
        Debug.Log(debugText);
    }

    int GetPlayerIndex()
    {
        //要素数はmap.Lengthで取得
        for (int i = 0; i < map.Length; i++)
        {
            if (map[i] == 1)
            {
                return i;
            }
        }
        return -1;
    }

    bool MoveNumber(int number, int moveFrom, int moveTo)
    {
        //移動先が範囲外なら移動不可
        if (moveTo < 0 || moveTo >= map.Length) { return false; }
        //移動先に2が居たら
        if (map[moveTo] == 2)
        {
            //どの方向へ移動するかを算出
            int velocity = moveTo - moveFrom;
            bool success = MoveNumber(2, moveFrom, moveTo + velocity);
            //もし箱が移動失敗したら、プレイヤーの移動も失敗
            if (!success) { return false; }
        }
        //プレイヤー・箱関わらずの移動処理
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }
}
