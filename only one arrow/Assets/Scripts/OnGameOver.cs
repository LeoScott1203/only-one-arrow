using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class OnGameOver : MonoBehaviour
{

    [SerializeField]
    Canvas cv;

    void Start()
    {
        //DontDestroyOnLoad(cv);
        cv.enabled = false;
    }

    void OnEnable()
    {

        EnemyMovementBehaviour.TriggerMenu += OnEnd;

    }

    void OnEnd(EnemyMovementBehaviour EMB)
    {

        cv.enabled = true;

    }

}