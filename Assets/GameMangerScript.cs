using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameMangerScript : MonoBehaviour
{
    //�z��̍쐬
    int[] map;

    // Start is called before the first frame update
    void Start()
    {
        //�z��̎��Ԃ̍쐬�Ə�����
        map = new int[] { 0, 0, 0, 1, 0, 2, 0, 0, 0 };
        PrintArray();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.RightArrow))
        {
            //���\�b�g�������������g�p
            int playerIndex = GetPlayerIndex();

            //�ړ��������֐���
            MoveNumber(1, playerIndex, playerIndex + 1);
            PrintArray();
        }
    }

    void PrintArray()
    {
        //������̐錾�Ə�����
        string debugText = "";

        for(int i = 0; i < map.Length; i++)
        {
            //�������A�g���Ă���
            debugText += map[i].ToString() + ",";
        }

        //����������������o��
        Debug.Log(debugText);
    }

    int GetPlayerIndex()
    {
        //�v�f����map.Length�Ŏ擾
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
        //�ړ��悪�͈͊O�Ȃ�ړ��s��
        if (moveTo < 0 || moveTo >= map.Length) { return false; }
        //�ړ����2��������
        if (map[moveTo] == 2)
        {
            //�ǂ̕����ֈړ����邩���Z�o
            int velocity = moveTo - moveFrom;
            bool success = MoveNumber(2, moveFrom, moveTo + velocity);
            //���������ړ����s������A�v���C���[�̈ړ������s
            if (!success) { return false; }
        }
        //�v���C���[�E���ւ�炸�̈ړ�����
        map[moveTo] = number;
        map[moveFrom] = 0;
        return true;
    }
}
