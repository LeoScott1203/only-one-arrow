using UnityEngine;

public class PlayerShooting : MonoBehaviour, IArrowHolder
{
    static float startingDrawUnits = 5.0f;
    static float drawUnitsPerSecond = 10.0f;
    static float maxDrawUnits = 15.0f;

    [SerializeField]
    Transform _shootPoint;
    public Transform ShootPoint
    {
        get
        {
            return _shootPoint;
        }
    }
    
    [SerializeField]
    Arrow arrow;

    float currentDrawUnits = startingDrawUnits;

    public void Update()
    {
        if(arrow.CurrentState != Arrow.State.Carried) // We're not doing anything if the arrow isn't held; Also TODO: since we have a thief enemy, redo that system
        {
            return;
        }

        if(Input.GetMouseButton(0)) // LMB held down
        {
            currentDrawUnits = Mathf.Min(maxDrawUnits, currentDrawUnits + drawUnitsPerSecond * Time.deltaTime);
        }
        if(Input.GetMouseButtonUp(0)) // LMB; TODO: better behaviour but this is just for testing
        {
            Shoot();
        }
    }

    void Shoot()
    {
        arrow.Shoot(_shootPoint, currentDrawUnits); // testing with magic numbers
        currentDrawUnits = startingDrawUnits;
    }
}
