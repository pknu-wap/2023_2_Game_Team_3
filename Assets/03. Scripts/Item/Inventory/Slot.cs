using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    #region ����
    public Item item;
    public Image icon;
    public DragSlot dragSlot;
    public ItemInfoWindow infoWindow;
    #endregion ����

    #region �ʱ� ����
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
        dragSlot = GameObject.Find("DragSlot").GetComponent<DragSlot>();
        infoWindow = GameObject.Find("ItemInfoWindow").GetComponent<ItemInfoWindow>();
    }
    #endregion �ʱ� ����

    #region ���콺 �̺�Ʈ
    /// <summary>
    /// Ŭ�� �� �̺�Ʈ
    /// </summary>
    /// <param name="eventData"></param>
    public void OnPointerClick(PointerEventData eventData)
    {
        // ��Ŭ�� ��
        if(eventData.button == PointerEventData.InputButton.Right)
        {
            // �������� ����Ѵ�.
            item.UseItem();
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
        if(item == null)
        {
            return;
        }

        dragSlot.SetItem(item);
        dragSlot.ShowImage();
    }

    /// <summary>
    /// �巡�� �� ȣ��ȴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (item == null)
        {
            return;
        }

        // �巡�� ������ ���콺�� ���󰣴�.
        dragSlot.transform.position = eventData.position;
    }

    /// <summary>
    /// �巡�װ� ������ �� ȣ��ȴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag(PointerEventData eventData)
    {
        if (item == null)
        {
            return;
        }

        dragSlot.HideImage();
        AddItem(dragSlot.dragItem);
        UpdateSlot();
    }

    /// <summary>
    /// ���� ������ ���콺�� �������� �� ȣ��, OnEndDrag���� ���� ����ȴ�.
    /// </summary>
    public void OnDrop(PointerEventData eventData)
    {
        if(dragSlot.dragItem == null)
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
        Item temp = item;

        AddItem(dragSlot.dragItem);
        UpdateSlot();

        if (temp == null)
        {
            dragSlot.ClearItem();
        }
        else
        {
            // ���� ���Կ� �������� �־��ٸ�, �ٸ� ���Կ� �߰��ϱ� ���� dragItem�� temp�� �־�д�.
            dragSlot.SetItem(temp);
        }
    }

    /// <summary>
    /// ������ �߰�
    /// </summary>
    /// <param name="item"></param> 
    public void AddItem(Item item)
    {
        this.item = item;
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public void RemoveItem()
    {
        item = null;

        UpdateSlot();
    }

    /// <summary>
    /// ���� ���������� UI ����
    /// </summary>
    public void UpdateSlot()
    {
        if(item == null)
        {
            icon.enabled = false;
            return;
        }

        // �������� �����ϰ� ����
        icon.sprite = item.itemData.icon;
        // + ���ΰ� �տ� ��������Ʈ ��ġ�� ���� ���� ����
        icon.enabled = true;
    }

    /// <summary>
    /// ������ ǥ��
    /// </summary>
    public void ShowInfo()
    {
        // �������� ���ٸ� ����� �ʴ´�.
        if(item == null)
        {
            return;
        }

        // �ڽ��� ��ġ�� ������â�� �̵�
        infoWindow.ChangePosition(transform.position);
        infoWindow.UpdateInfo(item.itemData);
        infoWindow.ShowInfoUI();
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public void HideInfo()
    {
        // �������� ���ٸ� ȣ������ �ʴ´�.
        if (item == null)
        {
            return;
        }

        infoWindow.HideInfoUI();
    }

    /// <summary>
    /// ��� �ִ� �����̶�� true ��ȯ
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return item == null;
    }
}
