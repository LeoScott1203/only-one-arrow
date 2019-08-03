using UnityEngine;

public class PlayerMovement : MonoBehaviour, IChargeLevelProvider
{
    static float startingDashCooldown = 2.0f; // See arrow for getters affected by perks and stuff

    float speed
    {
        get
        {
            return StatsSingleton.PlayerSpeed;
        }
    }

    Vector3 mousePosition;
    Vector2 lookDirection;

    float currentDashCooldown = 0.0f;

    public float ChargeLevel
    {
        get
        {
            return currentDashCooldown / startingDashCooldown;
        }
    }

    // TODO: allow key bindings, fairly easy with Unity's default input tools
    public void Update()
    {

        if (Input.GetKey(KeyCode.A))
            transform.position += Vector3.left * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.D))
            transform.position += Vector3.right * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.W))
            transform.position += Vector3.up * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.S))
            transform.position += Vector3.down * speed * Time.deltaTime;

        if (Input.GetKey(KeyCode.LeftShift) || Input.GetKey(KeyCode.RightShift))
        {
            Dash();
        }

        if (speed != 0)
        {

            mousePosition = Camera.main.ScreenToWorldPoint(Input.mousePosition);
            lookDirection = new Vector2(mousePosition.x - transform.position.x, mousePosition.y - transform.position.y);
            transform.up = lookDirection;

        }

        if (currentDashCooldown > 0)
        {
            currentDashCooldown = Mathf.Max(0.0f, currentDashCooldown - Time.deltaTime);
        }
    }

    void Dash()
    {
        // TODO: this needs to dash towards where you're moving, but I just need to test some things so it doesn't actually do anything atm

        currentDashCooldown = startingDashCooldown;
    }
}