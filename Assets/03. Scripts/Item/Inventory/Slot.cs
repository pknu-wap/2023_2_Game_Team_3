using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    #region ����
    public Item slotItem;
    public Image icon;
    public DragSlot dragSlot;
    public ItemInfoWindow infoWindow;
    private QuickSlot quickSlot;

    public UnityEvent[] clickEvent = new UnityEvent[3];

    // ���� ������ ���/����/�Һ� ���� �� � ���ΰ�?
    public UseTag slotTag;

    [SerializeField]
    private GameObject itemStatus;  // E ��ũ, Q ��ũ, ���� �� ���¸� ��Ÿ���� ������Ʈ
    [SerializeField]
    private TextMeshProUGUI countText;
    #endregion ����

    #region �ʱ� ����
    private void Awake()
    {
        AssignObjects();
        AddEvent();

        // TODO: ���̺� ���Ͽ��� �������� �޾ƿ´�.

        UpdateSlotUI();
    }

    /// <summary>
    /// ���� �Ҵ�
    /// </summary>
    void AssignObjects()
    {
        // �ڽ� ������Ʈ Slot Item�� �̹���
        icon = transform.GetChild(0).GetComponent<Image>();
        dragSlot = GameObject.Find("DragSlot").GetComponent<DragSlot>();
        infoWindow = GameObject.Find("Item Info Window").GetComponent<ItemInfoWindow>();
        quickSlot = GameObject.Find("Quick Slot").GetComponent<QuickSlot>();

        itemStatus = transform.GetChild(1).gameObject;

        // �Һ� �����̶�� �ؽ�Ʈ�� �޾ƿ´�.
        if(slotTag == UseTag.Consume)
        {
            countText = itemStatus.GetComponent<TextMeshProUGUI>();
        }
    }

    void AddEvent()
    {
        // ��� ������ ��� ��
        clickEvent[(int)UseTag.Equip].AddListener(HideInfo);
        clickEvent[(int)UseTag.Equip].AddListener(ToggleEquip);

        // �ڵ� ������ ��� ��
        clickEvent[(int)UseTag.Hand].AddListener(HideInfo);
        clickEvent[(int)UseTag.Hand].AddListener(ToggleHand);

        // �Һ� ������ ��� ��
        clickEvent[(int)UseTag.Consume].AddListener(HideInfo);
        clickEvent[(int)UseTag.Consume].AddListener(ActivateCurrentItem);
        clickEvent[(int)UseTag.Consume].AddListener(DecreaseItemCount);
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
            ClickItem();
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
        if(slotItem == null)
        {
            return;
        }

        dragSlot.SetItem(slotItem);
        dragSlot.ShowImage();
    }

    /// <summary>
    /// �巡�� �� ȣ��ȴ�.
    /// </summary>
    /// <param name="eventData"></param>
    public void OnDrag(PointerEventData eventData)
    {
        if (slotItem == null)
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
        if (slotItem == null)
        {
            return;
        }

        dragSlot.HideImage();
        AddItem(dragSlot.dragItem);
        UpdateSlotUI();
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

    #region ������ ��ȣ�ۿ�
    /// <summary>
    /// ���� ���԰� �巡�� ������ �������� �����Ѵ�.
    /// </summary>
    void ChangeSlot()
    {
        Item temp = slotItem;

        AddItem(dragSlot.dragItem);
        UpdateSlotUI();

        // �ٸ� ���Կ� �߰��ϱ� ���� dragItem�� temp�� �־�д�.
        dragSlot.SetItem(temp);
    }

    /// <summary>
    /// ������ �߰�
    /// </summary>
    /// <param name="item"></param> 
    public void AddItem(Item item)
    {
        if(item == null)
        {
            slotItem = item;
            return;
        }

        // ���� �Һ� �������� �����Ѵٸ� ������ 1 ������Ű�� ����
        if(item.itemData.useTag == UseTag.Consume && item == slotItem)
        {
            item.count++;
            return;
        }

        // �� �� ���Կ� �� ������ �߰�
        slotItem = item;
        item.transform.parent = transform;
    }

    /// <summary>
    /// ������ ������ 1 ���δ�.
    /// </summary>
    public void DecreaseItemCount()
    {
        if(slotItem == null)
        {
            return;
        }

        // �Һ� �������� �ƴϰų�, �Һ� �������� ������ 1 ���� �� 0 ������ �� ����.
        if(slotItem.itemData.useTag != UseTag.Consume || --slotItem.count <= 0)
        {
            slotItem = null;
        }

        UpdateSlotUI();
    }

    /// <summary>
    /// ���Կ� ��ϵ� �������� ����Ѵ�. �ش� �������� clickEvent�� ����ȴ�.
    /// </summary>
    void ActivateCurrentItem()
    {
        if (slotItem == null)
        {
            return;
        }

        slotItem.ActivateItem();
    }

    /// <summary>
    /// ������ Ŭ������ �� ����Ǵ� �Լ�. ������ ������ ���� 3���� �̺�Ʈ �� �ϳ��� ����ȴ�.
    /// </summary>
    void ClickItem()
    {
        if (slotItem == null)
        {
            return;
        }

        // UseTag�� �´� �̺�Ʈ ����
        clickEvent[(int)slotItem.itemData.useTag].Invoke();
    }

    /// <summary>
    /// Equip/Unequip ���¸� ����Ѵ�.
    /// </summary>
    void ToggleEquip()
    {
        if(slotItem == null)
        {
            return;
        }

        if(slotItem.isEquiped == true)
        {
            UnequipItem();
            slotItem.DeactivateItem();
        }

        else
        {
            EquipItem();
            slotItem.ActivateItem();
        }
    }

    /// <summary>
    /// �������� ���������� ǥ���Ѵ�.
    /// </summary>
    void EquipItem()
    {
        itemStatus.SetActive(true);
        slotItem.isEquiped = true;
    }

    /// <summary>
    /// �������� ���������� ǥ���Ѵ�.
    /// </summary>
    void UnequipItem()
    {
        itemStatus.SetActive(false);
        slotItem.isEquiped = false;
    }

    /// <summary>
    /// Hand ���¸� ����Ѵ�.
    /// </summary>
    void ToggleHand()
    {
        if (slotItem == null)
        {
            return;
        }

        if (slotItem.isEquiped == true)
        {
            UnhandItem();
        }

        else
        {
            HandItem();
        }
    }

    /// <summary>
    /// �������� �����Կ� ����Ѵ�.
    /// </summary>
    void HandItem()
    {
        itemStatus.SetActive(true);
        slotItem.isEquiped = true;
        quickSlot.SetInventorySlot(this);
        quickSlot.SetItem(slotItem);
    }
    
    /// <summary>
    /// �������� �����Կ��� �����Ѵ�.
    /// </summary>
    public void UnhandItem()
    {
        itemStatus.SetActive(false);
        slotItem.isEquiped = false;
        quickSlot.ClearQuickSlot();
    }
    #endregion ������ ��ȣ�ۿ�

    #region UI
    /// <summary>
    /// ���� ���������� UI ����
    /// </summary>
    public void UpdateSlotUI()
    {
        if(slotItem == null)
        {
            icon.enabled = false;
            itemStatus.SetActive(false);
            return;
        }

        icon.sprite = slotItem.itemData.icon;
        // TODO: ���ΰ� �տ� ������ ��������Ʈ�� ��ģ��.
        icon.enabled = true;

        // �Һ� �������� ��� ������ �ֽ�ȭ
        if (slotItem.itemData.useTag == UseTag.Consume)
        {
            itemStatus.SetActive(true);
            countText.text = slotItem.count.ToString();
        }
        // �� �� �������� ���� ���� �ֽ�ȭ
        else
        {
            itemStatus.SetActive(slotItem.isEquiped);
        }
    }

    /// <summary>
    /// ������ ǥ��
    /// </summary>
    public void ShowInfo()
    {
        // �������� ���ٸ� ����� �ʴ´�.
        if(slotItem == null)
        {
            return;
        }

        // �ڽ��� ��ġ�� ������â�� �̵�
        infoWindow.ChangePosition(transform.position);
        infoWindow.UpdateInfo(slotItem.itemData);
        infoWindow.ShowInfoUI();
    }

    /// <summary>
    /// ������ ����
    /// </summary>
    public void HideInfo()
    {
        // �������� ���ٸ� ȣ������ �ʴ´�.
        if (slotItem == null)
        {
            return;
        }

        infoWindow.HideInfoUI();
    }
    #endregion UI

    /// <summary>
    /// ��� �ִ� �����̶�� true ��ȯ
    /// </summary>
    /// <returns></returns>
    public bool IsEmpty()
    {
        return slotItem == null;
    }
}
