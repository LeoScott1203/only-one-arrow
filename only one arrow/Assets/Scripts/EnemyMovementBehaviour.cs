using UnityEngine;

public abstract class EnemyMovementBehaviour : MonoBehaviour
{
    private bool completed = false;
    public bool dead = false;

    [SerializeField]
    float speed = 0.1f;

    public Canvas cv;

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

        PlayerMovement PM = col.gameObject.GetComponent<PlayerMovement>();
        // PM.speed = 0f; // TODO: fix
        cv.enabled = true;

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
