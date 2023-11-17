using UnityEngine;

public class ItemTrigger : MonoBehaviour
{
    public Item item;
    private Inventory inventory;
    private GameUIManager gameUIManager;

    private void Awake()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        gameUIManager = GameObject.Find("Game UI Manager").GetComponent<GameUIManager>();
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾ ���� ���� ������ �ڽ��� ���� ���ͷ��� ��ư UI�� ����.
            gameUIManager.ShowInteractionUI(transform);
        }
    }

    private void OnTriggerExit2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            // �÷��̾ ���� �������� ����� �ڽ� ���� ���ͷ��� ��ư UI�� �����Ѵ�.
            gameUIManager.HideInteractionUI();
        }
    }

    /// <summary>
    /// �������� ȹ���Ѵ�.
    /// </summary>
    public void GetItem()
    {
        AddToInventory();
        gameUIManager.HideInteractionUI();
        DestroyItem();
    }

    /// <summary>
    /// �ڽ��� �κ��丮�� �߰��Ѵ�.
    /// </summary>
    public void AddToInventory()
    {
        inventory.AddItem(item);
    }

    /// <summary>
    /// ������ ȹ�� �� �ڱ� �ڽ��� �����ϴ� �Լ�
    /// �켱 ��Ȱ��ȭ�� �����صξ���.
    /// </summary>
    public void DestroyItem()
    {
        gameObject.SetActive(false);
    }
}
