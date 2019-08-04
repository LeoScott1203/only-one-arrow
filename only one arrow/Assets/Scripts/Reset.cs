using System;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Reset : MonoBehaviour
{
    public static Action OnReset = delegate { };

    public void TriggerReset()
    {
        OnReset();

        GetComponentInParent<Canvas>().enabled = false; // just cleaning up a bit
    }

}