using TMPro;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public ItemData itemData;
    public Image icon;
    public Sprite iconSp;

    TextMeshProUGUI nameText;
    TextMeshProUGUI descriptionText;

    private void Awake()
    {
        AssignObjects();

        // ���̺� ���Ͽ��� �������� �޾ƿ� ��

        UpdateSlot();
    }

    /// <summary>
    /// ���� �Ҵ�
    /// </summary>
    void AssignObjects()
    {
        // �ڽ� ������Ʈ Slot Item�� �̹���
        icon = transform.GetChild(0).GetComponent<Image>();
    }

    /// <summary>
    /// ������ �߰�
    /// </summary>
    /// <param name="item"></param>
    public void AddItem(ItemData item)
    {
        itemData = item;

        UpdateSlot();
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public void RemoveItem()
    {
        itemData = null;

        UpdateSlot();
    }

    /// <summary>
    /// ���� ���������� UI ����
    /// </summary>
    public void UpdateSlot()
    {
        if(itemData == null)
        {
            icon.enabled = false;
            return;
        }

        iconSp = itemData.Icon;
        icon.sprite = itemData.Icon;
        Debug.Log(itemData.Name);
        icon.enabled = true;
    }

    /// <summary>
    /// ������ ǥ��
    /// </summary>
    public void FloatInfo()
    {
        InfoWindow.Instance.UpdateInfo(itemData);
        InfoWindow.Instance.FloatInfoUI();
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public void HideInfo()
    {
        InfoWindow.Instance.HideInfoUI();
    }

    /// <summary>
    /// ��� �ִ� �����̶�� true ��ȯ
    /// </summary>
    /// <returns></returns>
    public bool isEmpty()
    {
        return itemData == null;
    }
}
