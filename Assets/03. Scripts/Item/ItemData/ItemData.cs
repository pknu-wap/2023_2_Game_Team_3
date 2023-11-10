using UnityEngine;
using UnityEngine.Events;

public enum ItemTag
{
    Clue = 0,
    Explore = 1
}

[CreateAssetMenu(fileName = "ItemData", menuName = "Item Data", order = 0)]
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

    public ItemTag itemTag;
}
