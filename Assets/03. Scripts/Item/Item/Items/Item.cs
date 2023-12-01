using UnityEngine;

public class Item : MonoBehaviour
{
    // ������ �ʴ� ������
    public ItemData itemData;
    // ���ϴ� ������ (�� �����۸��� ����)
    public int count = 1;
    public bool isEquiped;

    /// <summary>
    /// �������� ����Ѵ�.
    /// </summary>
    public virtual bool ActivateItem()
    {
        return true;
    }


    /// <summary>
    /// �������� �����Ѵ�. (Equip, Hand���� ���)
    /// </summary>
    public virtual bool DeactivateItem()
    {
        return true;
    }
}