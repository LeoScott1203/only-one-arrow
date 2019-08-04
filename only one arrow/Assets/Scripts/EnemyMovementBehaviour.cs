using UnityEngine;
using System;

public abstract class EnemyMovementBehaviour : MonoBehaviour
{

    private bool completed = false;
    public bool dead = false;

    Color _tempColor;

    [SerializeField]
    float speed = 0.1f;

    float stunDuration = 0.0f;

    static public Action<EnemyMovementBehaviour> TriggerMenu = delegate { };
    static public Action<EnemyMovementBehaviour> TriggerDeletion = delegate { };

    public void Update()
    {
        if(stunDuration > 0)
        {
            stunDuration = Mathf.Max(0.0f, stunDuration - Time.deltaTime);

            return;
        }

        if ( completed || dead )
        {

            return;

        }

        DetermineFacing();
        Move();

    }

    void gameOver(Collider2D col)
    {

        PlayerMovement PM = col.gameObject.GetComponent<PlayerMovement>();
        PM.ableToMove = false;
        TriggerMenu(this);

    }

    void OnTriggerEnter2D(Collider2D col)
    {

        if (!dead)
        {
            Explosion explosion = col.GetComponent<Explosion>();

            if(explosion != null)
            {
                if(explosion.stunning)
                {
                    stunDuration = 1.0f;
                }
                else
                {
                    TriggerDeletion(this);
                }
                return;
            }

            if (col.gameObject.tag == "Player" && stunDuration == 0.0f)
            {
                if(col.GetComponent<Player>().IsTelegibbing)
                {
                    dead = true;
                    TriggerDeletion(this);
                }
                else
                {
                    completed = true;
                    gameOver(col);
                }

            }

        }

    }

    protected abstract void DetermineFacing();

    protected void Move()
    {

        transform.Translate(Vector3.up * speed * Time.deltaTime);

    }

    public void OnEnable()
    {

        EnemyMovementBehaviour.TriggerMenu += OnEnd;
        Reset.OnReset += OnReset;

    }

    void OnEnd(EnemyMovementBehaviour EMB)
    {

        completed = true;

    }

    void OnReset()
    {
        TriggerDeletion(this);
    }

    public static void ImJustHookingThingsUpToThisBecauseItsFaster() // This is absolutely atrocious and I'll want to clean it up sliiightly later; need a working product first
    {
        Player.MainPlayer.GetComponent<PlayerMovement>().ableToMove = false;
        TriggerMenu(FindObjectOfType<EnemyMovementBehaviour>());
    }

    public void TriggerDeletionFromElsewhere()
    {
        TriggerDeletion(this);
    }
}