using UnityEngine;

public class ArmorItem : Item
{
    public override void ActivateItem()
    {
        // ������ �÷��̾�� �߰��Ѵ�.
        Debug.Log("���� ����");
    }


    public override void DeactivateItem()
    {
        // ������ �����Ѵ�.
        Debug.Log("���� ����");
    }
}
