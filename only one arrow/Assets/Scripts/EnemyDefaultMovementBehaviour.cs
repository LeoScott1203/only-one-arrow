using UnityEngine;

public class EnemyDefaultMovementBehaviour : EnemyMovementBehaviour
{
    protected override void DetermineFacing()
    {
        Vector2 lookDirection = new Vector2(Player.MainPlayer.transform.position.x - transform.position.x, Player.MainPlayer.transform.position.y - transform.position.y);
        transform.up = lookDirection;
    }
}
