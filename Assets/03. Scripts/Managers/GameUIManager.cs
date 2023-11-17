using UnityEngine;

public class GameUIManager : Singleton<GameUIManager>
{
    private Inventory inventory;
    private ItemInfoWindow itemInfoWindow;
    private GameObject interactionUIObj;
    private Vector3 interactOffset = new(0f, 1f, 0f);

    public void Test()
    {
        Debug.Log("����ĳ��Ʈ ���� ����");
    }

    #region �ʱ� ����
    private void Awake()
    {
        AssignObjects();
    }

    private void Start()
    {
        HideInventoryUI();
        HideInteractionUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventoryUI();
        }
    }

    private void AssignObjects()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        interactionUIObj = GameObject.Find("Interaction Button");
        itemInfoWindow = GameObject.Find("Item Info Window").GetComponent<ItemInfoWindow>();
    }
    #endregion �ʱ� ����

    #region Inventory
    // �κ��丮 UI�� ���� ������ ����, ���� ������ �Ѵ� �Լ�
    public void ToggleInventoryUI()
    {
        if (inventory.IsInventoryShowed() == false)
        {
            inventory.ShowInventoryUI();
        }

        else
        {
            inventory.HideInventoryUI();
            itemInfoWindow.HideInfoUI();
        }
    }

    public void ShowInventoryUI()
    {
        inventory.HideInventoryUI();
    }

    public void HideInventoryUI()
    {
        inventory.HideInventoryUI();
        itemInfoWindow.HideInfoUI();
    }
    #endregion Inventory

    #region Interaction UI
    public void ShowInteractionUI(Transform targetTransform)
    {
        MoveInteractionUI(targetTransform);
        interactionUIObj.SetActive(true);
    }

    public void HideInteractionUI()
    {
        interactionUIObj.SetActive(false);
    }

    public void MoveInteractionUI(Transform targetTransform)
    {
        interactionUIObj.transform.position = Camera.main.WorldToScreenPoint(targetTransform.position + interactOffset);
    }
    #endregion Interaction UI
}
