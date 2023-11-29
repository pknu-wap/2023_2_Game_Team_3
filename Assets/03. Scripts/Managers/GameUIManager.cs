using UnityEngine;
using UnityEngine.SceneManagement;

public class GameUIManager : Singleton<GameUIManager>
{
    #region �ʱ� ����

    protected override void Awake()
    {
        base.Awake();
    }

    private void Start()
    {
        SceneManager.activeSceneChanged += OnSceneChanged;

        AssignObjects();

        HideInventoryUI();
        HideInteractionUI();
        HidePausePanelUI();
        HideGameOverUI();
    }

    private void OnSceneChanged(Scene current, Scene next)
    {
        AssignObjects();

        HideInventoryUI();
        HideInteractionUI();
        HidePausePanelUI();
        HideGameOverUI();
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
        try
        {
            inventory = GameObject.Find("Inventory").GetComponent<Inventory>();
        }
        catch { }
        try
        {
            interactionUIObj = GameObject.Find("Interaction Button");
        }
        catch { }
        try
        {
            itemInfoWindow = GameObject.Find("Item Info Window").GetComponent<ItemInfoWindow>();
        }
        catch { }
        try
        {
            pausePanel = GameObject.Find("Pause Panel");
        }
        catch { }
        try
        {
            gameOverPanel = GameObject.Find("GameOver Panel");
        }
        catch { }
    }

    public void SetInventoryObject(Inventory inventory)
    {
        this.inventory = inventory;
    }
    #endregion �ʱ� ����

    #region Inventory
    [Header("�κ��丮")]
    [SerializeField] private Inventory inventory;
    [SerializeField] private ItemInfoWindow itemInfoWindow;

    // �κ��丮 UI�� ���� ������ ����, ���� ������ �Ѵ� �Լ�
    public void ToggleInventoryUI()
    {
        if (inventory == null)
        {
            return;
        }

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
        if (inventory == null)
        {
            return;
        }

        inventory.HideInventoryUI();
    }

    public void HideInventoryUI()
    {
        if(inventory == null || itemInfoWindow == null)
        {
            return;
        }

        inventory.HideInventoryUI();
        itemInfoWindow.HideInfoUI();
    }
    #endregion Inventory

    #region Interaction UI
    [Header("��ȣ �ۿ�")]
    [SerializeField] private GameObject interactionUIObj;
    [SerializeField] private Vector3 interactOffset = new(0f, 1f, 0f);

    public void ShowInteractionUI(Transform targetTransform)
    {
        if(interactionUIObj == null)
        {
            return;
        }

        MoveInteractionUI(targetTransform);
        interactionUIObj.SetActive(true);
    }

    public void HideInteractionUI()
    {
        if (interactionUIObj == null)
        {
            return;
        }

        interactionUIObj.SetActive(false);
    }

    public void MoveInteractionUI(Transform targetTransform)
    {
        if (interactionUIObj == null)
        {
            return;
        }

        interactionUIObj.transform.position = Camera.main.WorldToScreenPoint(targetTransform.position + interactOffset);
    }
    #endregion Interaction UI

    #region �Ͻ�����
    [Header("�Ͻ� ����")]
    [SerializeField] private GameObject pausePanel;
    [SerializeField] private bool isPaused = false;

    public void TogglePauseState()
    {
        if (pausePanel == null)
        {
            return;
        }

        if (isPaused)
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
        if (pausePanel == null)
        {
            return;
        }

        Time.timeScale = 0f;
        ShowPausePanelUI();
        isPaused = true;
    }

    public void ResumeGame()
    {
        if (pausePanel == null)
        {
            return;
        }

        Time.timeScale = 1f;
        HidePausePanelUI();
        isPaused = false;
    }

    public void ShowPausePanelUI()
    {
        if (pausePanel == null)
        {
            return;
        }

        pausePanel.SetActive(true);
    }

    public void HidePausePanelUI()
    {
        if (pausePanel == null)
        {
            return;
        }

        pausePanel.SetActive(false);
    }
    #endregion �Ͻ�����

    #region ���ӿ���
    [Header("���� ����")]
    [SerializeField] private GameObject gameOverPanel;
    public void ShowGameOverUI()
    {
        if (gameOverPanel == null)
        {
            return;
        }

        gameOverPanel.SetActive(true);
    }

    public void HideGameOverUI()
    {
        if (gameOverPanel == null)
        {
            return;
        }

        gameOverPanel.SetActive(false);
    }
    #endregion ���ӿ���
}
