using TMPro;
using UnityEngine;
using UnityEngine.Events;
using UnityEngine.EventSystems;
using UnityEngine.UI;
using static UnityEditor.Progress;

public class Slot : MonoBehaviour, IPointerClickHandler, IPointerEnterHandler, IPointerExitHandler, IBeginDragHandler, IDragHandler, IEndDragHandler, IDropHandler
{
    #region ����
    [Header("������")]
    public Item slotItem;
    public Image icon;

    [Header("�κ��丮")]
    private DragSlot dragSlot;
    private ItemInfoWindow infoWindow;
    private QuickSlot quickSlot;

    [Header("�÷��̾� ����")]
    private PlayerHand playerHand;

    [Header("�̺�Ʈ")]
    public UnityEvent[] clickEvent = new UnityEvent[3];

    // ���� ������ ���/����/�Һ� ���� �� � ���ΰ�?
    public UseTag slotTag;

    private GameObject itemStatus;  // E ��ũ, Q ��ũ, ���� �� ���¸� ��Ÿ���� ������Ʈ
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
        playerHand = GameObject.FindWithTag("Player").transform.GetChild(0).GetComponent<PlayerHand>();

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

    public void AddItem(Item item)
    {
        if(item == null)
        {
            slotItem = null;
            return;
        }

        // ���� �Һ� �������� �����Ѵٸ� ������ 1 ������Ű�� ����
        if(item.itemData.useTag == UseTag.Consume && item == slotItem)
        {
            item.count++;
            return;
        }

        // �� �� ���Կ� �� ������ �߰�
        slotItem = item;    // Load�� ��������� ��
        item.transform.parent = transform;
        item.gameObject.SetActive(false);
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

        // �Һ� �������� �ƴ϶�� ����,
        // �Һ� �������� ������ 1 ���� �� 0�̶�� ����.
        if(slotItem.itemData.useTag != UseTag.Consume || --slotItem.count <= 0)
        {
            slotItem = null;
        }

        UpdateSlotUI();
    }

    public void ClearSlot()
    {
        slotItem = null;

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

    void ToggleEquip()
    {
        if(slotItem.isEquiped == true)
        {
            UnequipItem();
        }

        else
        {
            EquipItem();
        }
    }

    void EquipItem()
    {
        if (slotItem.ActivateItem() == false)
        {
            return;
        }

        itemStatus.SetActive(true);
        slotItem.isEquiped = true;
        UpdateSlotUI();
    }

    public void UnequipItem()
    {
        if (slotItem.DeactivateItem() == false)
        {
            return;
        }

        itemStatus.SetActive(false);
        slotItem.isEquiped = false;
        UpdateSlotUI();
    }

    void ToggleHand()
    {
        if (slotItem.isEquiped == true)
        {
            UnhandItem();
        }

        else
        {
            HandItem();
        }
    }

    void HandItem()
    {
        if (slotItem == null)
        {
            return;
        }

        itemStatus.SetActive(true);
        slotItem.isEquiped = true;

        quickSlot.SetInventorySlot(this);
        quickSlot.SetItem(slotItem);

        UpdateSlotUI();

        // HandItemToPlayer();
    }
    
    public void UnhandItem()
    {
        if (slotItem == null)
        {
            return;
        }

        itemStatus.SetActive(false);
        slotItem.isEquiped = false;

        quickSlot.ClearQuickSlot();

        UpdateSlotUI();

        ReturnItemToSlot();
    }

    private void HandItemToPlayer()
    {
        slotItem.transform.parent = playerHand.transform;
        slotItem.transform.localPosition = Vector3.zero;
        slotItem.transform.localScale = Vector3.one;

        playerHand.HandItem(slotItem);
        slotItem.gameObject.SetActive(true);
    }

    private void ReturnItemToSlot()
    {
        slotItem.transform.parent = transform;
        slotItem.gameObject.SetActive(false);
        slotItem.transform.localPosition = Vector3.zero;
    }
    #endregion ������ ��ȣ�ۿ�

    #region UI
    public void UpdateSlotUI()
    {
        if(slotItem == null)
        {
            icon.enabled = false;
            itemStatus.SetActive(false);
            return;
        }

        icon.sprite = slotItem.itemData.icon;
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

        // ������ Hand �������̶�� ������ ���� �ֽ�ȭ
        if (slotItem.itemData.useTag == UseTag.Hand && slotItem.isEquiped)
        {
            quickSlot.currentSlot = this;
            HandItemToPlayer();
        }
    }

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

    public bool IsEmpty()
    {
        return slotItem == null;
    }
}
