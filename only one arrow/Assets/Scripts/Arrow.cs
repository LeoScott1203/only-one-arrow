using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Arrow : MonoBehaviour, IChargeLevelProvider
{
    // RN all these consts are just random bits of nonsense, need some in-game stuff to actually see what those values should be - Urist
    // Some will just be set up once; some will be something like { get { return Perks.IsUnlocked(Perk.SomePerk) ? 5.0f : 4.0f; } }
    // startingSpeedPerDrawUnit is currently set up as example of above

    // Gets multiplied by initial draw units (based on how long the shooting button has been held for )
    static float startingSpeedPerDrawUnit
    {
        get
        {
            return Perks.IsUnlocked(Perk.ArrowStartingSpeedPerDrawUnit) ? 2.0f : 1.5f;
        }
    }

    // These values govern how fast the arrow stops after being shot. Visualisation should probably tie into that as well if we have time.
    // Arrow starts at falloffStart falloff, gets falloffAcceleration per second up to a max of falloffMax
    static float falloffStart = 0.0f;
    static float falloffAcceleration = 1.0f;
    static float falloffMax = 2.0f;

    new Collider2D collider; // Ugh new and legacy named variables.

    float speed = 0.0f;
    float falloff = 0.0f;

    State _currentState = State.Carried;
    public State CurrentState
    {
        get
        {
            return _currentState;
        }
    }

    public float ChargeLevel
    {
        get
        {
            return speed / 100f; // TODO: the right side should actually be max speed possible from max draw x speed per draw
        }
    }

    public void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    public void Update()
    {
        switch(_currentState)
        {
            case(State.Carried): // Dummies for now
            {
                break;
            }
            case(State.InFlight):
            {
                transform.Translate(Vector3.right * speed * Time.deltaTime);
                
                falloff = Mathf.Min(falloffMax, falloff + falloffAcceleration * Time.deltaTime);
                speed = Mathf.Max(0f, speed - falloff);

                if(speed == 0f)
                {
                    _currentState = State.Resting;
                }

                break;
            }
            case(State.Resting):
            {
                break;
            }
        }
    }

    public void OnTriggerEnter2D(Collider2D other)
    {
        Debug.Log("Collid");
        IArrowHolder arrowHolder = other.GetComponent<IArrowHolder>();
        
        if(arrowHolder != null)
        {
            PickUp(arrowHolder);
        }
    }

    public void Shoot(Transform shootPoint, float drawUnits)
    {
        _currentState = State.InFlight;

        speed = drawUnits * startingSpeedPerDrawUnit;
        falloff = falloffStart;
        transform.SetParent(null);

        collider.enabled = true;
    }

    public void PickUp(IArrowHolder arrowHolder) // Later connect with on collide
    {
        _currentState = State.Carried;

        transform.SetParent(arrowHolder.ShootPoint);
        transform.SetPositionAndRotation(arrowHolder.ShootPoint.position, arrowHolder.ShootPoint.rotation);

        collider.enabled = false; // Just in case.
    }

    public enum State
    {
        Carried,
        InFlight,
        Resting
    }
}
