using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;

    // ������ ȹ�� ������ �ӽ÷� �÷��̾�� �ε����� ���� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AddToInventory();
        }
    }

    public void AddToInventory()
    {
        Inventory.Instance.AddItem(itemData);
    }
}
