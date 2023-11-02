using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class DragSlot : MonoBehaviour
{
    Image icon;
    public ItemData dragItem;

    private void Start()
    {
        icon = GetComponent<Image>();

        HideImage();
    }

    /// <summary>
    /// �巡�� ������ ������(���� ���õ� ������)�� item���� �����Ѵ�.
    /// </summary>
    /// <param name="item"></param>
    public void SetItem(ItemData item)
    {
        dragItem = item;
        icon.sprite = item.sprite;
    }

    /// <summary>
    /// �巡�� ������ �������� ����.
    /// </summary>
    public void ClearItem()
    {
        dragItem = null;
    }

    /// <summary>
    /// �巡�� ������ ���̰� �Ѵ�.
    /// </summary>
    public void ShowImage()
    {
        gameObject.SetActive(true);
    }

    /// <summary>
    /// �巡�� ������ �����.
    /// </summary>
    public void HideImage()
    {
        gameObject.SetActive(false);
    }
}
