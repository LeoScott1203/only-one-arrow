using UnityEngine;

[RequireComponent(typeof(Player))]
public class PlayerMovement : MonoBehaviour, IChargeLevelProvider
{
    Player parent;

    static float startingDashCooldown = 2.0f; // See arrow for getters affected by perks and stuff

    public bool ableToMove = true;

    float speed
    {
        get
        {
            float speed = Perks.IsUnlocked(Perk.PlayerSpeed) ? StatsSingleton.PlayerSpeedWithPerk : StatsSingleton.PlayerSpeedWithoutPerk;

            if(Perks.IsUnlocked(Perk.PlayerSpeedWhenWithoutArrow) && !parent.HasArrow)
            {
                speed *= StatsSingleton.PlayerSpeedMultiplierWithoutArrowWithPerk;
            }

            return speed;
        }
    }

    Vector3 mousePosition;
    Vector2 lookDirection;
    Vector2 moveDirection = Vector2.up; // Doesn't matter, but there should be a default nonetheless

    float currentDashCooldown = 0.0f;

    public float ChargeLevel
    {
        get
        {
            return currentDashCooldown / startingDashCooldown;
        }
    }

    public void Awake()
    {
        parent = GetComponent<Player>();
    }

    // TODO: allow key bindings, fairly easy with Unity's default input tools
    public void Update()
    {

        if (ableToMove)
        {
            Vector2 moveDirection = Vector2.zero;

            if (Input.GetKey(KeyCode.A))
            {
                moveDirection += Vector2.left * speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.D))
            {
                moveDirection += Vector2.right * speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.W))
            {
                moveDirection += Vector2.up * speed * Time.deltaTime;
            }

            if (Input.GetKey(KeyCode.S))
            {
                moveDirection += Vector2.down * speed * Time.deltaTime;
            }

            if(moveDirection != Vector2.zero)
            {
                Vector2 newPosition = (Vector2)transform.position + moveDirection;

                if(MapBoundaries.InBounds(newPosition))
                {
                    transform.position = newPosition;
                    this.moveDirection = moveDirection.normalized;
                }
            }

            if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
            {

                Dash();

            }

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lookDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            transform.up = lookDirection;

            if (currentDashCooldown > 0)
            {

                currentDashCooldown = Mathf.Max(0.0f, currentDashCooldown - Time.deltaTime);

            }

        }

    }

    void Dash()
    {
        // TODO: this needs to dash towards where you're moving, but I just need to test some things so it doesn't actually do anything atm

        currentDashCooldown = startingDashCooldown;
    }
}