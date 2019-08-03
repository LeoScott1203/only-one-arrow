using UnityEngine;

public class Enemy : MonoBehaviour, ITarget
{
    [SerializeField]
    Transform _targetTransform;
    public Transform TargetTransform
    {
        get
        {
            return _targetTransform;
        }
    }
}