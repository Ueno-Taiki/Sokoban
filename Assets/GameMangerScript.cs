using System.Collections;
using System.Collections.Generic;
using System.Runtime.InteropServices.WindowsRuntime;
using UnityEngine;

public class GameMangerScript : MonoBehaviour
{
    public GameObject playerPrefab;

    //配列の作成
    int[,] map;

    //ゲーム管理用の配列
    GameObject[,] field;

    // Start is called before the first frame update
    void Start()
    {
        //mapの生成
        map = new int[,]{
            { 1, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 },
            { 0, 0, 0, 0, 0 }
        };
        field = new GameObject
            [
            map.GetLength(0),
            map.GetLength(1)
            ];

        string debugText = "";

        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                if (map[y, x] == 1)
                {
                    field[y, x] = Instantiate(
                        playerPrefab,
                        new Vector3(x, map.GetLength(0) - 1 - y, 0), Quaternion.identity);
                }
                debugText += map[y, x].ToString() + ",";
            }
            debugText += "\n";
        }
        Debug.Log(debugText);
        //PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetKeyDown(KeyCode.UpArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, -1));
        }
        if (Input.GetKeyDown(KeyCode.DownArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(0, 1));
        }
        if (Input.GetKeyDown(KeyCode.LeftArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(-1, 0));
        }
        if (Input.GetKeyDown(KeyCode.RightArrow))
        {
            Vector2Int playerIndex = GetPlayerIndex();
            MoveNumber(playerIndex, playerIndex + new Vector2Int(1, 0));
        }
    }

    Vector2Int GetPlayerIndex()
    {
        //要素数はmap.Lengthで取得
        for (int y = 0; y < map.GetLength(0); y++)
        {
            for (int x = 0; x < map.GetLength(1); x++)
            {
                //nullだったらタグを調べず次の要素へ移る
                if (field[y, x] == null) { continue; }
                //タグの確認を行う
                if (field[y, x].tag == "Player")
                {
                    return new Vector2Int(x, y);
                }
            }
        }
        return new Vector2Int(-1, -1);
    }

    bool MoveNumber(Vector2Int moveFrom, Vector2Int moveTo)
    {
        //二次元配列に対応
        if (moveTo.y < 0 || moveTo.y >= field.GetLength(0)) { return false; }
        if (moveTo.x < 0 || moveTo.x >= field.GetLength(1)) { return false; }
        /*
        //移動先に2が居たら
        if (map[moveTo] == 2)
        {
            //どの方向へ移動するかを算出
            int velocity = moveTo - moveFrom;
            bool success = MoveNumber(2, moveFrom, moveTo + velocity);
            //もし箱が移動失敗したら、プレイヤーの移動も失敗
            if (!success) { return false; }
        }
        */
        //プレイヤー・箱関わらずの移動処理
        field[moveTo.y, moveTo.x] = field[moveFrom.y, moveFrom.x];
        field[moveFrom.y, moveFrom.x].transform.position =
            new Vector3(moveTo.x, map.GetLength(0) - moveTo.y, 0);
        field[moveFrom.y, moveFrom.x] = null;
        return true;
    }
}
