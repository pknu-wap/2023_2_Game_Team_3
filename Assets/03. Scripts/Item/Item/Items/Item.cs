using UnityEngine;

public class Item : MonoBehaviour
{
    // ������ �ʴ� ������
    public ItemData itemData;
    // ���ϴ� ������ (�� �����۸��� ����)
    public int count = 1;

    /// <summary>
    /// �������� ����Ѵ�.
    /// </summary>
    public virtual void ActivateItem()
    {

    }


    /// <summary>
    /// �������� �����Ѵ�. (Equip, Hand���� ���)
    /// </summary>
    public virtual void DeactivateItem()
    {

    }
}