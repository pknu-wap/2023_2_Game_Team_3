using UnityEngine;

public class GameUIManager : Singleton<GameUIManager>
{
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
        HidePausePanelUI();
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.B))
        {
            ToggleInventoryUI();
        }


        if (Input.GetKeyDown(KeyCode.Escape))
        {
            TogglePauseState();
        }
    }

    private void AssignObjects()
    {
        inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        interactionUIObj = GameObject.Find("Interaction Button");
        itemInfoWindow = GameObject.Find("Item Info Window").GetComponent<ItemInfoWindow>();
        PausePanel = GameObject.Find("Pause Panel");
    }
    #endregion �ʱ� ����

    #region Inventory
    [Header("�κ��丮")]
    private Inventory inventory;
    private ItemInfoWindow itemInfoWindow;

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
    [Header("��ȣ �ۿ�")]
    private GameObject interactionUIObj;
    private Vector3 interactOffset = new(0f, 1f, 0f);

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

    #region �Ͻ�����
    [Header("�Ͻ� ����")]
    private GameObject PausePanel;
    private bool isPaused = false;

    public void TogglePauseState()
    {
        if(isPaused)
        {
            ResumeGame();
        }

        else
        {
            PauseGame();
        }
    }
    
    public void PauseGame()
    {
        Time.timeScale = 0f;
        ShowPausePanelUI();
        isPaused = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        HidePausePanelUI();
        isPaused = false;
    }

    public void ShowPausePanelUI()
    {
        PausePanel.SetActive(true);
    }

    public void HidePausePanelUI()
    {
        PausePanel.SetActive(false);
    }
    #endregion �Ͻ�����
}
