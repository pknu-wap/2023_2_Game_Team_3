using UnityEngine;
using UnityEngine.Events;

#region �±�
public enum PurposeTag
{
    Puzzle = 0,
    Explore = 1
}

public enum UseTag
{
    Equip = 0,
    Hand = 1,
    Consume = 2
}
#endregion �±�

[CreateAssetMenu(fileName = "ItemData", menuName = "Item Data", order = 0)]
public class ItemData : ScriptableObject
{
    [Header("��������Ʈ")]
    public Sprite icon = null;
    public Sprite sprite = null;

    [Header("������ ����")]
    // name�� �̹� �ִ�.
    public string itemName = null;
    public string description = null;
    public int count = 1;
    // ���� �켱 ����
    public int priority = 0;

    [Header("������ �з�")]
    public PurposeTag purposeTag;
    public UseTag useTag;
}
