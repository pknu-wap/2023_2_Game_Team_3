using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    public ItemData itemData;
    public Image icon;
    public Sprite iconSp;

    private void Awake()
    {
        AssignObjects();

        // ���̺� ���Ͽ��� �������� �޾ƿ� ��
        
        UpdateSlot();
    }

    #region ���콺 �̺�Ʈ
    /// <summary>
    /// Ŭ�� �� �̺�Ʈ, ���� �������
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        if(eventData.button == PointerEventData.InputButton.Right)
        {

        }
    }

    /// <summary>
    /// ���콺�� ���� ���� �ö���� �� ȣ��ȴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerEnter(PointerEventData eventData)
    {
        ShowInfo();
    }

    /// <summary>
    /// ���콺�� ���Կ��� ����� �� ȣ��ȴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerExit(PointerEventData eventData)
    {
        HideInfo();
    }

    /// <summary>
    /// �巡�װ� ���۵Ǹ� ȣ��ȴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag(PointerEventData eventData)
    {
        if(itemData == null)
        {
            return;
        }

        DragSlot.Instance.SetItem(itemData);
        DragSlot.Instance.ShowImage();
    }

    /// <summary>
    /// �巡�� �� ȣ��ȴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (itemData == null)
        {
            return;
        }

        // �巡�� ������ ���콺�� ���󰣴�.
        DragSlot.Instance.transform.position = eventData.position;
    }

    /// <summary>
    /// �巡�װ� ������ �� ȣ��ȴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (itemData == null)
        {
            return;
        }

        DragSlot.Instance.HideImage();
        AddItem(DragSlot.Instance.dragItem);
        UpdateSlot();
    }

    /// <summary>
    /// ���� ������ ���콺�� �������� �� ȣ��, OnEndDrag���� ���� ����ȴ�.
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        if(DragSlot.Instance.dragItem == null)
        {
            return;
        }

        ChangeSlot();
    }
    #endregion ���콺 �̺�Ʈ

    /// <summary>
    /// ������ ���� �����ϴ� �Լ�.
    /// </summary>
    void ChangeSlot()
    {
        ItemData temp = itemData;

        AddItem(DragSlot.Instance.dragItem);
        UpdateSlot();

        if (temp == null)
        {
            DragSlot.Instance.ClearItem();
        }
        else
        {
            // ���� ���Կ� �������� �־��ٸ�, �ٸ� ���Կ� �߰��ϱ� ���� dragItem�� temp�� �־�д�.
            DragSlot.Instance.SetItem(temp);
        }
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

        // �������� �����ϰ� ����
        icon.sprite = itemData.icon;
        // + ���ΰ� �տ� ��������Ʈ ��ġ�� ���� ���� ����
        icon.enabled = true;
    }

    /// <summary>
    /// ������ ǥ��
    /// </summary>
    public void ShowInfo()
    {
        // �������� ���ٸ� ����� �ʴ´�.
        if(itemData == null)
        {
            return;
        }

        // �ڽ��� ��ġ�� ������â�� �̵�
        InfoWindow.Instance.ChangePosition(transform.position);
        InfoWindow.Instance.UpdateInfo(itemData);
        InfoWindow.Instance.ShowInfoUI();
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public void HideInfo()
    {
        // �������� ���ٸ� ȣ������ �ʴ´�.
        if (itemData == null)
        {
            return;
        }

        InfoWindow.Instance.HideInfoUI();
    }

    /// <summary>
    /// ��� �ִ� �����̶�� true ��ȯ
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return itemData == null;
    }
}
