using UnityEngine;

public class ArmorItem : Item
{
    private Player player;

    private void Awake()
    {
        player = GameObject.FindWithTag("Player").GetComponent<Player>();
    }

    public override void ActivateItem()
    {
        // TODO: ��ȣ�� ����Ʈ�� �÷��̾ �߰� -> Ȱ��ȭ/��Ȱ��ȭ�� ����
        // player.armor++;, ��ȣ ���� �����ϱ�
    }


    public override void DeactivateItem()
    {
        // TODO: ��ȣ�� ����Ʈ�� �÷��̾ �߰� -> Ȱ��ȭ/��Ȱ��ȭ�� ����
        // player.armor--;, ��ȣ ���� �����ϱ�
        // ���������� armor�� 0 Ȥ�� 1�� �ϰ�, �������� �����ϴ� �� �� ���� ��
    }
}
