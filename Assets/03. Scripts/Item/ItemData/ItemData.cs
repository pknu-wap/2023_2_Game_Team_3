using UnityEngine;
using UnityEngine.Events;

public class ItemData : ScriptableObject
{
    public Sprite icon;
    public Sprite sprite;

    // name�� �̹� �ִ�...
    public string itemName;
    public string description;

    // ���� �켱 ����
    public int priority;

    public UnityEvent useItemEvent;
}
