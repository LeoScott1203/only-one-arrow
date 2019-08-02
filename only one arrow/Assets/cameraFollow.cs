using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollow : MonoBehaviour
{

    public Transform player;

    void LateUpdate()
    {

        transform.position = Vector3.Lerp(transform.position, player.position + new Vector3(0, 0, -5), 0.2f);

    }

}