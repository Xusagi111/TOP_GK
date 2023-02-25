using System;
using System.Collections;
using UnityEngine;

[Serializable]
public class PlayerModel: IPlayerModel
{
    [SerializeField] private float speed = 10;
    [SerializeField] private float rotationSpeed = 200;
    [SerializeField] private float gravity = 20;

    public float Speed => speed;
    public float RotationSpeed => rotationSpeed;
    public float Gravity => gravity;
}
