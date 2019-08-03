using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VisualizeBowCharge : MonoBehaviour
{

    public Transform arrow;
    public Transform bowLeft;
    public Transform bowRight;
    public GameObject player;

    Transform playerTrans;

    float chargeLevel;

    PlayerShooting playerShooting;

    void Awake()
    {

        playerShooting = player.GetComponent<PlayerShooting>();
        playerTrans = player.transform;

    }

    void Update()
    {

        chargeLevel = playerShooting.currentDrawUnits;

        if (chargeLevel == 5 || chargeLevel == 15)
            return;

        arrow.Translate(Vector3.right * Time.deltaTime * -0.15f);

    }

}