using System.Collections;
using System.Collections.Generic;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class Slot : MonoBehaviour
{
    public Item itemInfo;
    Image icon;

    public GameObject infoPanel;
    public TextMeshProUGUI nameText;
    public TextMeshProUGUI descriptionText;

    private void Awake()
    {
        AssignObjects();
        UpdateSlot(itemInfo);
        infoPanel.SetActive(false);
    }

    void AssignObjects()
    {
        // �ڽ� ������Ʈ Slot Item�� �̹���
        icon = transform.GetChild(0).GetComponent<Image>();
        // Info ������Ʈ
        infoPanel = transform.GetChild(1).gameObject;
        nameText = infoPanel.transform.GetChild(0).GetComponent<TextMeshProUGUI>();
        descriptionText = infoPanel.transform.GetChild(1).GetComponent<TextMeshProUGUI>();
    }

    public void UpdateSlot(Item item)
    {
        if(item == null)
        {
            return;
        }

        itemInfo = item;
        icon.sprite = item.Icon;
        nameText.text = item.Name;
        descriptionText.text = item.Description;
    }

    public void FloatInfo()
    {
        infoPanel.SetActive(true);
    }
}
