using ModestTree;
using System;
using System.Linq;
using UniRx;
using UniRx.Triggers;
using UnityEngine;

public sealed class PlayerView : MonoBehaviour, IPlayerView
{
    [SerializeField] private Animator animator;
    [SerializeField] private CharacterController chController;

    [SerializeField] private string[] triggersNames;
    [SerializeField] private Collider _myTrigger;

    private CompositeDisposable _disposable = new CompositeDisposable();
    public readonly ReactiveCommand<Collider> _colliderReactCommand = new ReactiveCommand<Collider>();

    public bool isGrounded { 
        get 
        {
            if (chController.isGrounded)
                return true;
            else return false;
        } 
    }

    private void OnEnable()
    {
        _myTrigger.OnTriggerEnterAsObservable()
            .Where(t => t.gameObject.layer == LayerMask.NameToLayer(
                triggersNames.FirstOrDefault(name => name == LayerMask.LayerToName(t.gameObject.layer))))
            .Subscribe(other =>
                _colliderReactCommand.Execute(other)
            ).AddTo(_disposable);
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

    private void OnDisable()
    {
        _disposable.Clear();
    }
}
