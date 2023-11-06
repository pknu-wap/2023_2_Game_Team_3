using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    private Inventory inventory;

    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    // ������ ȹ�� ������ �ӽ÷� �÷��̾�� �ε����� ���� ����
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            AddToInventory();
            DestroyItem();
        }
    }

    /// <summary>
    /// �ڽ��� �κ��丮�� �߰��Ѵ�.
    /// </summary>
    public void AddToInventory()
    {
        inventory.AddItem(itemData);
    }

    /// <summary>
    /// ������ ȹ�� �� �ڱ� �ڽ��� �����ϴ� �Լ�
    /// �켱 ��Ȱ��ȭ�� �صξ���.
    /// </summary>
    public void DestroyItem()
    {
        gameObject.SetActive(false);
    }

    public void UseItem()
    {

    }
}
