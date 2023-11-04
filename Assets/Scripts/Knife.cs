using UnityEngine;

public class Knife : ItemHandler
{
    protected override void ItemPickedUp(GameObject player)
    {
        GameManager.Instance.EnableAttack(true);

        base.ItemPickedUp(player);
    }
}