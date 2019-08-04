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

        if (Input.GetMouseButton(0)) // LMB held down
        {
            currentDrawUnits = Mathf.Min(maxDrawUnits, currentDrawUnits + drawUnitsPerSecond * Time.deltaTime);

            if (!audioCooldown)
            {
                GetComponent<AudioSource>().Play();
                audioCooldown = true;
            }
        }

        if (Input.GetMouseButtonUp(0)) // LMB; TODO: better behaviour but this is just for testing
        {
            Shoot();
            GetComponent<AudioSource>().Stop();
            audioCooldown = false;
            shootAudio.Play();
        }

    }

    void Shoot()
    {
        arrow.Shoot(_shootPoint, currentDrawUnits);
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
