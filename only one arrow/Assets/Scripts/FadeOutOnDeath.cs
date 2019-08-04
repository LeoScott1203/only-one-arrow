using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutOnDeath : MonoBehaviour
{

    Transform trans;

    void Start()
    {
        trans = GetComponent<Transform>();
    }

   public void OnEnable()
    {

        Arrow.TriggerDeletion += FadeOut;

    }

    void FadeOut( Arrow _ )
    {
        Debug.Log("a");
        Vector3 desiredPos = new Vector3(trans.position.x + (Random.value * 3), trans.position.y + (Random.value * 3), 0);
        Vector3.Lerp(trans.position, desiredPos, 0.5f);

    }

}
