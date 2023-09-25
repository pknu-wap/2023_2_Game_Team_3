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
        while (i < slots.Count && slots[i].IsEmpty() == false)
        {
            i++;
        }

        return i;
    }

    /// <summary>
    /// ����ִ� �� �� ���Կ� ������ �߰�
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(ItemData item)
    {
        int i = SearchFirstEmptySlot();

        // ������ ��� á�ٸ� ����
        if(i == slots.Count)
        {
            Debug.Log("������ ��� á���ϴ�.");
            return;
        }

        slots[i].AddItem(item);
        slots[i].UpdateSlot();
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
