using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.UI;

public class Inventory : MonoBehaviour
{
    #region ����
    private Image bg;
    public Slot[][] slots = new Slot[3][];
    private ItemInfoWindow infoWindow;
    public GameObject[] inventoryTab;
    private int currentTab = 0;
    #endregion ����

    #region �ʱ� ����
    private void Awake()
    {
        AssignObjects();
        // ó���� ��� ���� ����д�.
        SwitchInventoryTab(0);

        AddItem(CreateNewItem("Lamp"));
        LoadInventory();
    }

    private void Start()
    {

    }

    void AssignObjects()
    {
        bg = GetComponent<Image>();
        infoWindow = GameObject.Find("Item Info Window").GetComponent<ItemInfoWindow>();
        inventoryTab = new GameObject[3];

        // �κ��丮 �� �Ҵ�
        for(int i = 0; i < inventoryTab.Length; i++)
        {
            inventoryTab[i] = transform.GetChild(1).GetChild(i).gameObject;
        }

        // slots �Ҵ� -> �� ���� ��� ������ �Ҵ��Ѵ�.
        for (int i = 0; i < slots.Length; i++) {
            slots[i] = new Slot[30];
            int j = 0;
            // �ڽ� ������Ʈ slot�� ��� �Ҵ�
            foreach (Transform slot in transform.GetChild(1).GetChild(i))
            {
                slots[i][j++] = slot.GetComponent<Slot>();
            }
        }
    }
    #endregion �ʱ� ����

    #region ������ ��ȣ�ۿ�
    /// <summary>
    /// ��� �ִ� �� �� ���� �ε��� ��ȯ�Ѵ�. �������� ������ slots.Coun��t ��ȯ�Ѵ�.
    /// </summary>
    /// <returns></returns>
    int SearchFirstEmptySlot(int useTag)
    {
        int i = 0;

        // ������� �ʴٸ� i�� 1 ����
        while (i < slots[useTag].Length && slots[useTag][i].IsEmpty() == false)
        {
            i++;
        }

        return i;
    }

    /// <summary>
    /// itemData�� ���� ���� �� �� ���� �ε����� ��ȯ�Ѵ�. �������� ������ slots.Count�� ��ȯ�Ѵ�.
    /// </summary>
    /// <param name="itemData"></param>
    /// <returns></returns>
    int SearchSlotIndex(ItemData itemData)
    {
        int i = 0;

        // ������� �ʴٸ� i�� 1 ����
        while (i < slots[(int)itemData.useTag].Length && slots[(int)itemData.useTag][i].slotItem.itemData == itemData)
        {
            i++;
        }

        return i;
    }

    /// <summary>
    /// ����ִ� �� �� ���Կ� ������ �߰�
    /// </summary>
    /// <param name="itemData"></param>
    public void AddItem(Item item)
    {
        if (item == null)
        {
            return;
        }

        int i;

        // �Һ� �������̶�� �̹� �����ϴ��� �˻��Ѵ�.
        if (item.itemData.useTag == UseTag.Consume)
        {
            i = SearchSlotIndex(item.itemData);

            // �������� �������� ������ �� ������ ã�´�.
            if(i >= slots[(int)item.itemData.useTag].Length)
            {
                i = SearchFirstEmptySlot((int)item.itemData.useTag);
            }
        }
        else
        {
            i = SearchFirstEmptySlot((int)item.itemData.useTag);
        }

        // ������ ��� á�ٸ� ����
        if(i == slots[(int)item.itemData.useTag].Length)
        {
            Debug.Log("������ ��� á���ϴ�.");
            return;
        }

        slots[(int)item.itemData.useTag][i].AddItem(item);
        slots[(int)item.itemData.useTag][i].UpdateSlotUI();
    }

    /// <summary>
    /// i�� ������ ������ ����
    /// </summary>
    /// <param name="i"></param>
    public void RemoveItem(int useTag, int i)
    {
        slots[useTag][i].ClearSlot();
    }
    #endregion ������ ��ȣ�ۿ�

    #region ����
    /// <summary>
    /// ���� ���� �����۵��� ���� ������.
    /// </summary>
    public void TrimCurrentTab()
    {
        TrimInventory(currentTab);

        // UI ����
        foreach (Slot slot in slots[currentTab])
        {
            slot.UpdateSlotUI();
        }
    }

    /// <summary>
    /// �κ��丮�� ������ �����ϴ� �Լ�, ������ �ε����� ��ȯ
    /// </summary>
    public int TrimInventory(int index)
    {
        // ù��° ���� ã��
        int i = -1;
        while (++i < slots[index].Length && slots[index][i].slotItem != null) ;

        if(i == slots[index].Length)
        {
            // ������ ������ ��ȣ ��ȯ
            return i - 1;
        }

        int j = i;
        while (true)
        {
            // ������ �ƴ� ĭ���� j�� ������Ų��.
            while (++j < slots[index].Length && slots[index][j].slotItem == null) ;

            if (j == slots[index].Length)
            {
                // ������ �������� ��ȣ ��ȯ
                return i - 1;
            }

            // ���� ������ i�� ���� �������� �ְ�, i�� �� ĭ ������Ų��. �ݺ�
            slots[index][i].slotItem = slots[index][j].slotItem;
            slots[index][j].slotItem = null;
            i++;
        }
    }

    /// <summary>
    /// ���� ���� ���Ľ�Ų��.
    /// </summary>
    public void SortCurrentTab()
    {
        SortInventory(currentTab);
    }

    /// <summary>
    /// �κ��丮�� ����(�ӽ÷� �켱 ������ �ο�)
    /// </summary>
    public void SortInventory(int index)
    {
        // ������ �����⸦ �Բ� �����Ѵ�. n = ������ ������ ��ġ
        int n = TrimInventory(index);

        // ����(�� ��ġ�� ������...)
        List<Item> items = new();

        // �κ��丮�� ������ ����
        for(int i = 0; i <= n; i++)
        {
            items.Add(slots[index][i].slotItem);
        }

        // ������ ����
        items = items.OrderBy(x => x.itemData.priority).ToList();

        // ���Կ� ���ĵ� �������� ������.
        for(int i = 0; i < slots[index].Length; i++)
        {
            if(i <= n)
            {
                slots[index][i].AddItem(items[i]);
                continue;
            }

            slots[index][i].AddItem(null);
            slots[index][i].UpdateSlotUI();
        }
    }
    
    /// <summary>
    /// ��� Explore Item�� �����Ѵ�.
    /// </summary>
    public void DropExploreItems()
    {
        for(int i = 0; i < slots.Length; i++)
        {
            for (int j = 0; j < slots[i].Length; j++)
            {
                // Explore Item���� ����
                if (slots[i][j].slotItem == null || slots[i][j].slotItem.itemData.purposeTag != PurposeTag.Explore)
                {
                    continue;
                }

                // ���� ���̶�� ����
                if (slots[i][j].isEquiped)
                {
                    if (slots[i][j].slotItem.itemData.useTag == UseTag.Equip)
                    {
                        slots[i][j].UnequipItem();
                    }

                    else if(slots[i][j].slotItem.itemData.useTag == UseTag.Hand)
                    {
                        slots[i][j].UnhandItem();
                    }
                }

                slots[i][j].ClearSlot();
            }
        }
    }
    #endregion ����

    #region UI
    public void ShowInventoryUI()
    {
        gameObject.SetActive(true);
    }

    public void HideInventoryUI()
    {
        gameObject.SetActive(false);
        infoWindow.HideInfoUI();
    }

    public bool IsInventoryShowed()
    {
        return gameObject.activeSelf;
    }

    /// <summary>
    /// �κ��丮 ���� �ٲ۴�.
    /// </summary>
    /// <param name="index"></param>
    public void SwitchInventoryTab(int index)
    {
        for(int i = 0; i < slots.Length; i++)
        {
            if(i == index)
            {
                inventoryTab[i].SetActive(true);
                currentTab = i;
            }
            else
            {
                inventoryTab[i].SetActive(false);
            }
        }
    }
    #endregion UI

    #region ����
    public Item CreateNewItem(string itemName)
    {
        string path = "Prefebs/Items/" + itemName;
        GameObject obj = (GameObject)Resources.Load(path);

        if(obj == null)
        {
            return null;
        }

        Item item = Instantiate(obj, transform).GetComponent<Item>();

        return item;
    }

    public void SaveInventory()
    {
        SaveManager.Instance.SaveInventory(slots);
    }

    private void LoadInventory()
    {
        string[] itemNames = SaveManager.Instance.LoadInventory();

        for (int i = 0; i < 90; i++)
        {
            slots[i / 30][i % 30].AddItem(CreateNewItem(itemNames[i]));
            slots[i / 30][i % 30].UpdateSlotUI();
        }
    }
    #endregion ����
}