using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Arrow : MonoBehaviour
{
    // RN all these consts are just random bits of nonsense, need some in-game stuff to actually see what those values should be - Urist
    // Some will just be set up once; some will be something like { get { return Perks.IsUnlocked(Perk.SomePerk) ? 5.0f : 4.0f; } }
    // startingSpeedPerDrawUnit is currently set up as example of above

    // Gets multiplied by initial draw units (based on how long the shooting button has been held for )
    static float startingSpeedPerDrawUnit
    {
        get
        {
            return Perks.IsUnlocked(Perk.ArrowStartingSpeedPerDrawUnit) ? 0.8f : 0.5f;
        }
    }

    // These values govern how fast the arrow stops after being shot. Visualisation should probably tie into that as well if we have time.
    // Arrow starts at falloffStart falloff, gets falloffAcceleration per second up to a max of falloffMax
    static float falloffStart = 0.0f;
    static float falloffAcceleration = 1.0f;
    static float falloffMax = 2.0f;

    new Collider2D collider; // Ugh new and legacy named variables.

    [SerializeField]
    Transform shootPoint; // Arrows will be shot from here; should be a specialised GameObject on the player

    float speed = 0.0f;
    float falloff = 0.0f;

    bool isShot = false; // While is shot, move forward and stuff :)

    public void Awake()
    {
        collider = GetComponent<Collider2D>();
    }

    public void Update()
    {
        // We'll probably not do anything while it's not actually shot
        if(!isShot)
        {
            return;
        }

        if(speed > 0)
        {
            transform.Translate(Vector3.forward * speed); // I _think_ that should work, been a bit since I last touched transforms like that; need testing
            
            falloff = Mathf.Min(falloffMax, falloff + falloffAcceleration * Time.deltaTime);
            speed = Mathf.Max(0f, speed - falloff);
        }
    }

    public void Shoot(float drawUnits)
    {
        isShot = true;

        speed = drawUnits * startingSpeedPerDrawUnit;
        falloff = falloffStart;
        transform.SetPositionAndRotation(shootPoint.position, shootPoint.rotation);

        collider.enabled = true;
    }

    public void PickUp() // Later connect with on collide
    {
        isShot = false;

        transform.SetPositionAndRotation(new Vector3(10000, 10000, 10000), Quaternion.identity); // Just moving it awaaay so it doesn't really do anything. Won't matter anyway. Could disable sprite and everything but not necessary at this point.

        collider.enabled = false; // Just in case.
    }
}
