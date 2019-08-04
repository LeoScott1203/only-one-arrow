using System;
using UnityEngine;

public class Player : MonoBehaviour
{
    static int comboCountToChargeAbility = 4;
    static public Player MainPlayer // I'd name it Player but I can't, probably for good reason; it's just so enemies know who to aim for
    {
        get;
        private set;
    }

    static public Action<Player> OnComboChange = delegate { };

    PlayerShooting shooting;

    public int ComboCount
    {
        get;
        private set;
    } = 0;

    int comboSinceLastAbilityUsage = 0;
    float telegibTime = 0.0f;

    public bool IsTelegibbing
    {
        get
        {
            return telegibTime > 0;
        }
    }

    public bool AbilityReady
    {
        get
        {
            return comboSinceLastAbilityUsage >= comboCountToChargeAbility;
        }
    }

    public bool HasArrow
    {
        get
        {
            return shooting.HasArrow;
        }
    }

    public void Awake()
    {
        shooting = GetComponent<PlayerShooting>();
        MainPlayer = this;
    }

    public void OnEnable()
    {
        Arrow.OnArrowStoppedWithoutHitting += OnArrowStoppedWithoutHitting;
        Arrow.OnHit += OnArrowHit;
        Arrow.OnSpecialShot += OnAbilityUsed;
        Reset.OnReset += OnReset;
    }

    public void OnDisable()
    {
        Arrow.OnArrowStoppedWithoutHitting -= OnArrowStoppedWithoutHitting;
        Arrow.OnHit -= OnArrowHit;
        Arrow.OnSpecialShot -= OnAbilityUsed;
        Reset.OnReset -= OnReset;
    }

    public void Update()
    {
        if(telegibTime > 0)
        {
            telegibTime = Mathf.Max(0.0f, telegibTime - Time.deltaTime);
        }
    }

    void OnArrowStoppedWithoutHitting(Arrow arrow)
    {
        EndCombo();
    }

    void OnArrowHit(Collider2D other, ITarget target, Arrow arrow)
    {
        EnemyMovementBehaviour EMB = other.GetComponent<EnemyMovementBehaviour>();

        if (EMB.dead == false)
            IncrementCombo();
        else
            EndCombo();
    }

    void IncrementCombo()
    {
        ComboCount++;
        comboSinceLastAbilityUsage++;
        OnComboChange(this);
    }

    void EndCombo()
    {
        ComboCount = 0;
        comboSinceLastAbilityUsage = 0;
        OnComboChange(this);
    }

    void OnAbilityUsed()
    {
        comboSinceLastAbilityUsage = 0;
        OnComboChange(this);
    }
    
    void OnReset()
    {
        ComboCount = 0;
        comboSinceLastAbilityUsage = 0;
        OnComboChange(this);

        transform.position = Vector3.zero;
    }

    public void ActivateTelegib()
    {
        telegibTime = 0.1f;
    }
}