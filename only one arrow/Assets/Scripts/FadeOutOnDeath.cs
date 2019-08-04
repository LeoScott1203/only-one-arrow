using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FadeOutOnDeath : MonoBehaviour
{
    Color _tempColor;

    void Start()
    {

        _tempColor = GetComponent<SpriteRenderer>().color;

    }

   public void OnEnable()
    {

        EnemyMovementBehaviour.TriggerDeletion += FadeOut;

    }

    void FadeOut( EnemyMovementBehaviour EMB ) // not being called
    {

        if (_tempColor.a > 0f)
        {

            _tempColor.a -= Time.deltaTime / 0.5f;
            GetComponent<SpriteRenderer>().color = _tempColor;

        }
        else if (_tempColor.a <= 0f)
        {

            Destroy(GetComponent<GameObject>());

        }

    }

}
