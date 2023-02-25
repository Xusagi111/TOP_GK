using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public sealed class PlayerView : MonoBehaviour, IPlayerView
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController chController;

    public bool isGrounded { 
        get 
        {
            if (chController.isGrounded)
                return true;
            else return false;
        } 
    }

    public void Move(Vector3 moveDirection)
    {
        chController.Move(moveDirection * Time.deltaTime);
    }

    public void RotateToWards(Quaternion quaternion, float speed)
    {
        transform.rotation = Quaternion.RotateTowards(transform.rotation, quaternion, speed);
    }

    public Vector3 GetTransformInverseVector(Vector3 inputVect) => transform.InverseTransformDirection(inputVect);

    public void DynamicFloatAnimate(string name, float value, float dampTime)
    {
        animator.SetFloat(name, value, dampTime, Time.deltaTime);
    }
}
