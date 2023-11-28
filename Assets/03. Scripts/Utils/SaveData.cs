// MonoBehavior�� ��ӹ��� ������, ���ӿ�����Ʈ�� ���� �ʿ䰡 ����.
// https://glikmakesworld.tistory.com/14

using System.Collections.Generic;
using UnityEngine;

[System.Serializable]
public class SaveData
{
    // ���� ���� �ְ� ��������
    public int mapNumber;
    // �ְ� ���������� ����(������ �ĺ� �� �ϳ�)
    public int mapIndex;
    public List<Item>[] inventory;

    public SaveData()
    {
        mapNumber = 0;
        mapIndex = 0;
        inventory = new List<Item>[3];

        for(int i = 0; i < 3; i++)
        {
            inventory[i] = new List<Item>(new Item[30]);
        }
    }
}
