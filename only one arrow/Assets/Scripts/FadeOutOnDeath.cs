using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutOnDeath : MonoBehaviour
{

    GameObject SgameObject;
    EnemyMovementBehaviour EMB;

    void Start()
    {
        EMB = GetComponent<EnemyMovementBehaviour>();
        SgameObject = this.gameObject;
    }

   public void OnEnable()
    {

        EnemyMovementBehaviour.TriggerDeletion += FadeOut;

    }

    void FadeOut(EnemyMovementBehaviour EMB)
    {

        if(this.EMB == EMB)
        {
            Destroy(SgameObject);
        }

    }

}
