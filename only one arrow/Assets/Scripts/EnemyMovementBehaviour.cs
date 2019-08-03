using UnityEngine;
using System;

public abstract class EnemyMovementBehaviour : MonoBehaviour
{
    private bool completed = false;
    public bool dead = false;

    [SerializeField]
    float speed = 0.1f;

    static public Action<EnemyMovementBehaviour> TriggerMenu = delegate { };

    public void Update()
    {

        if (completed)
            return;

        if (dead)
            return;

        DetermineFacing();
        Move();

    }

    void gameOver(Collider2D col)
    {
        // PlayerMovement PM = col.gameObject.GetComponent<PlayerMovement>();
        // PM.speed = 0f; // TODO: figure out how to get around this
        TriggerMenu(this);

    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (!dead)
        {

            if (col.gameObject.tag == "Player")
            {

                completed = true;
                gameOver(col);

            }

        }

    }

    protected abstract void DetermineFacing();

    protected void Move()
    {

        transform.Translate(Vector3.up * speed * Time.deltaTime);

    }
}