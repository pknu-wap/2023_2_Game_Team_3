using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : Singleton<Inventory>
{
    public Image bg;
    public List<Slot> slots;

    private void Awake()
    {
        AssignObjects();
    }

    /// <summary>
    /// ���� �Ҵ�
    /// </summary>
    void AssignObjects()
    {
        bg = GetComponent<Image>();

        // �ڽ� ������Ʈ slot�� ��� �Ҵ�
        foreach(Transform slot in transform)
        {
            slots.Add(slot.GetComponent<Slot>());
        }
    }

    /// <summary>
    /// ��� �ִ� �� �� ���� �ε��� ��ȯ
    /// </summary>
    /// <returns></returns>
    int SearchFirstEmptySlot()
    {
        int i = 0;

        // ������� �ʴٸ� i�� 1 ����
        while (slots[i].isEmpty() == false)
        {
            i++;
            Debug.Log(i + "��° ���� ������� ����");
        }

        Debug.Log(i + "��° ���� �������");
        return i;
    }

    /// <summary>
    /// ����ִ� �� �� ���Կ� ������ �߰�
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(ItemData item)
    {
        int i = SearchFirstEmptySlot();

        slots[i].AddItem(item);
    }

    /// <summary>
    /// i�� ������ ������ ����
    /// </summary>
    /// <param name="i"></param>
    public void RemoveItem(int i)
    {
        slots[i].RemoveItem();
    }
}
