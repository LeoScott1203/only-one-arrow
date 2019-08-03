using UnityEngine;

public interface IChargeLevelProvider
{
    float ChargeLevel { get; }
}

public interface IArrowHolder
{
    Transform ShootPoint { get; }
}

public interface ITarget
{
    Transform TargetTransform { get; }
}