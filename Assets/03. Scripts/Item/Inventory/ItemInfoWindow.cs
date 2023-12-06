using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class ItemInfoWindow : MonoBehaviour
{
    private RectTransform rectTransform;
    private TextMeshProUGUI nameText;
    private TextMeshProUGUI descriptionText;

    public float clampX, clampY;


    private void Awake()
    {
        AssignObjects();
        AssignValues();
    }

    private void Start()
    {
        HideInfoUI();
    }

    void AssignObjects()
    {
        rectTransform = GetComponent<RectTransform>();
        nameText = transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        descriptionText = transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    void AssignValues()
    {
        Vector2 resolution = transform.parent.GetComponent<CanvasScaler>().referenceResolution;

        clampX = resolution.x / 2 - rectTransform.rect.width;
        clampY = resolution.y / 2 - rectTransform.rect.height;
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
    /// position���� ��ġ�� �̵��Ѵ�. (���� ��ǥ�� ����)
    /// </summary>
    /// <param name="position"></param>
    public void ChangePosition(Vector3 position)
    {
        ClampPositionWithAnchor(position);
        Debug.Log(position);

        rectTransform.anchoredPosition = position;
    }

    /// <summary>
    /// ������â ǥ��
    /// </summary>
    public void ShowInfoUI()
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

    void ClampPositionWithAnchor(Vector2 position)
    {
        // ���� �ϴ�
        if(position.x < clampX && position.y < -clampY)
        {
            // �ǹ��� ���� �ϴ����� ����
            rectTransform.pivot = new Vector2(0, 0);
        }

        // ���� �ϴ�
        else if (position.x > clampX && position.y < -clampY)
        {
            // �ǹ��� ���� �ϴ����� ����
            rectTransform.pivot = new Vector2(1, 0);
        }

        // ���� ���
        else if (position.x > clampX && position.y > -clampY)
        {
            // �ǹ��� ���� ������� ����
            rectTransform.pivot = new Vector2(1, 1);
        }

        // ����Ʈ �ǹ��� ���� ���
        else
        {
            // �ǹ��� ���� ������� ����
            rectTransform.pivot = new Vector2(0, 1);
        }

        rectTransform.localPosition = position;
    }
}
