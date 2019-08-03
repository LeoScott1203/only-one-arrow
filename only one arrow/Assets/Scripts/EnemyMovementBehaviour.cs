using UnityEngine;

public abstract class EnemyMovementBehaviour : MonoBehaviour
{
    [SerializeField]
    float speed = 0.1f;

    public void Update()
    {
        DetermineFacing();

        Move();
    }

    protected abstract void DetermineFacing();

    protected void Move()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }
}
