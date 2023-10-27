using UnityEngine;

public enum ItemTag
{
    Clue = 0,
    Explore = 1
};

public class ItemData : ScriptableObject
{
    public Sprite icon = null;
    public Sprite sprite = null;

    // name�� �̹� �ִ�.
    public string itemName = null;
    public string description = null;

    // ���� �켱 ����
    public int priority = 0;

    public ItemTag tag;
}
