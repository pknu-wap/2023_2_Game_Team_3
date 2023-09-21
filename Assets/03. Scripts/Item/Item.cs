using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Debug.Log("�浹");

        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("�κ��丮�� �߰�");
            AddToInventory();
        }
    }


    public void AddToInventory()
    {
        Inventory.Instance.AddItem(itemData);
    }
}
