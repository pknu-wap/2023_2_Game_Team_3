using UnityEngine;

public class CubicItem : HandItem
{
    public Cubic cubic;

    PlayerCollision player;

    protected override void Awake()
    {
        base.Awake();

        player = GameObject.FindWithTag("Player").GetComponent<PlayerCollision>();
    }

    public override bool ActivateItem()
    {
        if(player.currentKeySword == null)
        {
            return false;
        }

        if (cubic == player.currentKeySword.cubic)
        {
            player.DestroyKeySword();
            DestroyItem();
            return true;
        }

        else
        {
            GameUIManager.Instance.AlertMessage("�� ������ �ƴ� ���ϴ�.");
        }
        return false;
    }

    public override bool DeactivateItem()
    {
        return true;
    }
}
