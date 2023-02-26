using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface IPlayerView
{
    public void Move(Vector3 moveDirection);
    public void RotateToWards(Quaternion quaternion, float speed);

    public void DynamicFloatAnimate(string name, float value, float dampTime);

}
