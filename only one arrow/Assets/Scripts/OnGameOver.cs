using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameOver : MonoBehaviour
{

    [SerializeField]
    Canvas cv;

    void OnEnable()
    {

        EnemyMovementBehaviour.TriggerMenu += OnEnd;

    }

    void OnEnd(EnemyMovementBehaviour EMB)
    {

        Debug.Log("h");
        cv.enabled = true;

    }

}