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
    public string[] inventoryItems = new string[90];

    public SaveData()
    {
        mapNumber = 0;
        mapIndex = 0;
        inventoryItems = new string[90];
    }
}
