using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutOnDeath : MonoBehaviour
{

    GameObject SgameObject;

    void Start()
    {
        SgameObject = this.gameObject;
    }

   public void OnEnable()
    {

        Arrow.TriggerDeletion += FadeOut;
        EnemyMovementBehaviour.TriggerDeletion += FadeOut;

    }

    void FadeOut(Arrow _)
    {

        Destroy(SgameObject);

    }

    void FadeOut(EnemyMovementBehaviour _)
    {

        Destroy(SgameObject);

    }

}
