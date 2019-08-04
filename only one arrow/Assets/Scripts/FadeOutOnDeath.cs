﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutOnDeath : MonoBehaviour
{

    GameObject gameObject;

   public void OnEnable()
    {

        Arrow.TriggerDeletion += FadeOut;
        EnemyMovementBehaviour.TriggerDeletion += FadeOut;

    }

    void FadeOut(Arrow _)
    {

        Destroy(gameObject);

    }

    void FadeOut(EnemyMovementBehaviour _)
    {

        Destroy(gameObject);

    }

}
