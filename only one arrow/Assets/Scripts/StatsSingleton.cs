using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class StatsSingleton : MonoBehaviour
{

    public static StatsSingleton INSTANCE = null;

    [SerializeField]
    float playerSpeed;

    void Awake()
    {
        INSTANCE = this;
    }

    public static float PlayerSpeed
    {

        get
        {

            return INSTANCE.playerSpeed;

        }

    }

}