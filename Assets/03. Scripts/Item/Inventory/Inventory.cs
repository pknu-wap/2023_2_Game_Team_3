using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
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
        foreach(Transform slot in transform.GetChild(1).transform)
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

    /// <summary>
    /// �κ��丮�� ������ �����ϴ� �Լ�, ������ �ε����� ��ȯ
    /// </summary>
    public int TrimInventory()
    {
        // �� ĭ ���ֱ�
        int i = -1;
        while (++i < slots.Count && slots[i].itemData != null) ;

        if(i == slots.Count)
        {
            return i - 1;
        }

        int j = i;

        while (true)
        {
            while (++j < slots.Count && slots[j].itemData == null) ;

            if (j == slots.Count)
            {
                return i - 1;
            }

            slots[i].itemData = slots[j].itemData;
            slots[j].itemData = null;
            i++;
        }
    }

    /// <summary>
    /// �κ��丮�� ����(�ӽ÷� �켱 ������ �ο�)
    /// </summary>
    public void SortInventory()
    {
        int n = TrimInventory();

        // ����(�� ��ġ�� ������...)
        List<ItemData> items = new List<ItemData>();

        for(int i = 0; i <= n; i++)
        {
            items.Add(slots[i].itemData);
        }

        items = items.OrderBy(x => x.priority).ToList();

        for(int i = 0; i < slots.Count; i++)
        {
            if(i > n)
            {
                slots[i].AddItem(null);
                continue;
            }

            slots[i].AddItem(items[i]);
        }

        // UI ����
        foreach (Slot slot in slots)
        {
            slot.UpdateSlot();
        }
    }

    /// <summary>
    /// �̺�Ʈ ��Ͽ� �ӽ� �Լ�
    /// </summary>
    public void Trim()
    {
        int i = TrimInventory();

        // UI ����
        foreach (Slot slot in slots)
        {
            slot.UpdateSlot();
        }
    }
}