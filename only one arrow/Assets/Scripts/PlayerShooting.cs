using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerShooting : MonoBehaviour, IArrowHolder
{
    static float startingDrawUnits
    {
        get
        {
            return StatsSingleton.ArrowStartingDrawUnits;
        }
    }

    static float drawUnitsPerSecond
    {
        get
        {
            return Perks.IsUnlocked(Perk.DrawUnitsGainPerSecond) ? StatsSingleton.ArrowDrawUnitsPerSecondWithPerk : StatsSingleton.ArrowDrawUnitsPerSecondWithoutPerk;
        }
    }
    
    static float maxDrawUnits
    {
        get
        {
            return StatsSingleton.ArrowMaxDrawUnits;
        }
    }  

    bool audioCooldown = false;

    public AudioSource shootAudio;

    [SerializeField]
    Transform _shootPoint;
    public Transform ShootPoint
    {
        get
        {
            return _shootPoint;
        }
    }
    public Transform arrowTrans;

    [SerializeField]
    Arrow arrow;

    public float currentDrawUnits;

    bool usingSpecial = false;

    public bool HasArrow
    {
        get
        {
            return arrow != null;
        }
    }

    public void Start()
    {
        currentDrawUnits = startingDrawUnits;
    }

    public void OnEnable()
    {
        Arrow.OnPickedUpBy += PickUp;
    }

    public void Update()
    {

        if (!HasArrow)
            return; // We're not doing anything if the arrow isn't held

        if (Input.GetMouseButton(1))
        {
            usingSpecial = true;
        }

        if (Input.GetMouseButton(0) || Input.GetMouseButton(1)) // LMB/RMB held down
        {
            currentDrawUnits = Mathf.Min(maxDrawUnits, currentDrawUnits + drawUnitsPerSecond * Time.deltaTime);

            if (!audioCooldown)
            {
                GetComponent<AudioSource>().Play(); // This should be cached
                audioCooldown = true;
            }
        }

        if (Input.GetMouseButtonUp(0) || Input.GetMouseButtonUp(1)) // LMB; TODO: better behaviour but this is just for testing
        {
            Shoot(Player.MainPlayer.AbilityReady && usingSpecial);
            GetComponent<AudioSource>().Stop();
            audioCooldown = false;
            shootAudio.Play();

            usingSpecial = false;
        }

    }

    void Shoot(bool usingSpecial)
    {
        arrow.Shoot(_shootPoint, currentDrawUnits, usingSpecial);
        currentDrawUnits = startingDrawUnits;
        arrow = null;
    }

    void PickUp(IArrowHolder arrowHolder, Arrow arrow) // This is a dumb implementation, but fast enough to code
    {
        if (arrowHolder != this)
        {
            return;
        }

        this.arrow = arrow;
    }
}
