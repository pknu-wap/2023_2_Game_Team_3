using TMPro;


public class InfoWindow : Singleton<InfoWindow>
{
    public TextMeshProUGUI nameText;
    private TextMeshProUGUI descriptionText;

    private void Awake()
    {
        InfoWindow.instance = this;

        AssignObjects();
        HideInfoUI();
    }

    void AssignObjects()
    {
        nameText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        descriptionText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    /// <summary>
    /// �������� ������ ����
    /// </summary>
    /// <param name="item"></param>
    public void UpdateInfo(ItemData item)
    {
        if(item == null)
        {
            return;
        }

        nameText.text = item.itemName;
        descriptionText.text = item.description;
    }

    /// <summary>
    /// ������â ǥ��
    /// </summary>
    public void FloatInfoUI()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// ������â ����
    /// </summary>
    public void HideInfoUI()
    {
        gameObject.SetActive(false);
    }
}
