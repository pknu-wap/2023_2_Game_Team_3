using UnityEngine;

public class Item : MonoBehaviour
{
    public ItemData itemData;
    private Inventory inventory;

    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾�� ������ �ڽ��� ���� ���ͷ��� ��ư UI�� ����.
            GameUIManager.Instance.FloatInteractionUI(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾�� ������ �ڽ� ���� ���ͷ��� ��ư UI�� �����Ѵ�.
            GameUIManager.Instance.CloseInteractionUI();
        }
    }

    public void GetItem()
    {
        AddToInventory();
        DestroyItem();
        Debug.Log("������ ȹ��");
    }

    /// <summary>
    /// �ڽ��� �κ��丮�� �߰��Ѵ�.
    /// </summary>
    public void AddToInventory()
    {
        inventory.AddItem(this);
    }

    /// <summary>
    /// ������ ȹ�� �� �ڱ� �ڽ��� �����ϴ� �Լ�
    /// �켱 ��Ȱ��ȭ�� �����صξ���.
    /// </summary>
    public void DestroyItem()
    {
        gameObject.SetActive(false);
    }

    /// <summary>
    /// �������� ����Ѵ�.
    /// </summary>
    public virtual void UseItem()
    {

    }
}
