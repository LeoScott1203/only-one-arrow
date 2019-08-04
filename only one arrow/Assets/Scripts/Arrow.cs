using System;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class Arrow : MonoBehaviour, IChargeLevelProvider
{
    // RN all these consts are just random bits of nonsense, need some in-game stuff to actually see what those values should be - Urist
    // Some will just be set up once; some will be something like { get { return Perks.IsUnlocked(Perk.SomePerk) ? 5.0f : 4.0f; } }
    // startingSpeedPerDrawUnit is currently set up as example of above

    // Gets multiplied by initial draw units (based on how long the shooting button has been held for )

    bool completed = false;

    public AudioSource killAudio;

    static float startingSpeedPerDrawUnit
    {
        get
        {
            return Perks.IsUnlocked(Perk.ArrowStartingSpeedPerDrawUnit) ? StatsSingleton.ArrowStartingSpeedPerDrawUnitWithPerk : StatsSingleton.ArrowStartingSpeedPerDrawUnitWithoutPerk;
        }
    }

    // These values govern how fast the arrow stops after being shot. Visualisation should probably tie into that as well if we have time.
    // Arrow starts at falloffStart falloff, gets falloffAcceleration per second up to a max of falloffMax
    static float falloffStart
    {
        get
        {
            return StatsSingleton.ArrowFalloffStart;
        }
    }

    static float falloffAcceleration
    {
        get
        {
            return Perks.IsUnlocked(Perk.ArrowFalloffAcceleration) ? StatsSingleton.ArrowFalloffAccelerationWithPerk : StatsSingleton.ArrowFalloffAccelerationWithoutPerk;
        }
    }

    static float falloffMax
    {
        get
        {
            return StatsSingleton.ArrowFalloffMax;
        }
    }

    static float bouncebackAngleDispersion
    {
        get
        {
            return Perks.IsUnlocked(Perk.ArrowBouncebackDispersion) ? StatsSingleton.ArrowBouncebackAngleDispersionWithPerk : StatsSingleton.ArrowBouncebackAngleDispersionWithoutPerk;
        }
    }

    static public Action<IArrowHolder, Arrow> OnPickedUpBy = delegate { };
    static public Action<Collider2D, ITarget, Arrow> OnHit = delegate { };
    static public Action<Arrow> OnArrowStoppedWithoutHitting = delegate { };
    static public Action OnSpecialShot = delegate { };
    static public Action<Arrow> TriggerDeletion = delegate { };

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

    bool hasHit = false;

    bool specialAbilityUsed = false;

    public float ChargeLevel
    {
        get
        {
            return speed / 100f; // TODO: the right side should actually be max speed possible from max draw x speed per draw
        }
    }

    public void Awake()
    {
        UnityEngine.Random.InitState((int)DateTime.Now.Ticks); // Doesn't really matter where we init it atm

        collider = GetComponent<Collider2D>();
    }

    public void OnEnable()
    {
        EnemyMovementBehaviour.TriggerMenu += OnEnd;
        Reset.OnReset += OnReset;
    }

    public void OnDisable()
    {
        EnemyMovementBehaviour.TriggerMenu -= OnEnd;
        Reset.OnReset -= OnReset;
    }

    void OnReset()
    {
        OnPickedUpBy(Player.MainPlayer.GetComponent<PlayerShooting>(), this);
        PickUpBy(Player.MainPlayer.GetComponent<PlayerShooting>());

        completed = false;
    }

    void OnEnd(EnemyMovementBehaviour EMB)
    {

        completed = true;

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

                if(!MapBoundaries.InBounds(transform.position))
                {
                    Bounce();
                }

                if(speed == 0f)
                {
                    if(!hasHit)
                    {
                        OnArrowStoppedWithoutHitting(this);
                    }

                    _currentState = State.Resting;

                    if(specialAbilityUsed)
                    {
                        specialAbilityUsed = false;

                        if(Perks.IsUnlocked(Perk.TeleportArrow))
                        {
                            Player.MainPlayer.transform.position = transform.position;
                        }
                    }
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
        IArrowHolder arrowHolder = other.GetComponent<IArrowHolder>();
        EnemyMovementBehaviour EMB = other.gameObject.GetComponent<EnemyMovementBehaviour>();

        if (arrowHolder != null)
        {
            OnPickedUpBy(arrowHolder, this);
            PickUpBy(arrowHolder);
            GetComponent<AudioSource>().Play();
        }

        if (CurrentState == State.InFlight) // Resting arrows shouldn't hit people
        {
            ITarget target = other.GetComponent<ITarget>();

            if (target != null)
            {
                OnHit(other, target, this);
                Hit(target, other.ClosestPoint(transform.position));
            }

            if (other.gameObject.tag == "Enemy")
            // kill enemy
            {

                killAudio.Play();
                EMB.dead = true;
                TriggerDeletion(this);

            }

        }

    }

    public void Shoot(Transform shootPoint, float drawUnits, bool usingSpecial)
    {
        if (!completed)
        {
            if(usingSpecial)
            {
                OnSpecialShot();
                specialAbilityUsed = true;
            }

            _currentState = State.InFlight;

            hasHit = false;

            speed = drawUnits * startingSpeedPerDrawUnit;
            falloff = falloffStart;
            transform.SetParent(null);

            collider.enabled = true;
        }
    }

    public void PickUpBy(IArrowHolder arrowHolder)
    {
        _currentState = State.Carried;

        hasHit = false;
        specialAbilityUsed = false;

        transform.SetParent(arrowHolder.ShootPoint);
        transform.SetPositionAndRotation(arrowHolder.ShootPoint.position, arrowHolder.ShootPoint.rotation);

        collider.enabled = false; // Just in case.
    }

    public void Hit(ITarget target, Vector2 perimeterPoint)
    {
        hasHit = true;

        Bounce();
    }

    void Bounce()
    {
        float angle = UnityEngine.Random.Range(180 - bouncebackAngleDispersion, 180 + bouncebackAngleDispersion);

        transform.Rotate(0, 0, angle);
    }

    public enum State
    {
        Carried,
        InFlight,
        Resting
    }
}
