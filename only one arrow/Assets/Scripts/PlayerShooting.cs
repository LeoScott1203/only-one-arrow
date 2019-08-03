using UnityEngine;

public class PlayerShooting : MonoBehaviour
{
    [SerializeField]
    Transform shootPoint;

    [SerializeField]
    Arrow arrow;

    public void Update()
    {
        if(Input.GetMouseButtonDown(0)) // LMB; TODO: better behaviour but this is just for testing
        {
            Shoot();
        }
    }

    void Shoot()
    {
        arrow.Shoot(shootPoint, 10.0f); // testing with magic numbers
    }
}
