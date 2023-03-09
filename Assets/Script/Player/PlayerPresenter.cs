using System;
using System.Linq;
using UniRx.Triggers;
using UniRx;
using UnityEngine;
using Zenject;

public sealed class PlayerPresenter : IInitializable, ITickable, IDisposable, IPlayerPresenter
{
    private readonly InputHandler _inputHandler;
    private readonly PlayerView _playerView;
    private readonly PlayerModel _playerModel;

    private Vector3 _cameraDotinputVect;
    private Vector3 moveDirection;

    private CompositeDisposable _disposable = new CompositeDisposable();
    public readonly ReactiveCommand<Collider> colliderReactCommand = new ReactiveCommand<Collider>();

    public PlayerPresenter(InputHandler input, PlayerView view, PlayerModel playerModel) 
    {
        _inputHandler = input;
        _playerView = view;
        _playerModel = playerModel;
    }

    public void Initialize() => SubscribesTrigger();  // подписка на тригеры из вьюшки

    public void Tick()
    {
        CalculateCameraDotVector();   // просчет урезаного импута для анимаций и подгонка под камеру
        ProcessMoveLogik();
        ProcessRotateLogik();
        ProccesMoveAnimation();
    }
    public void Dispose() => DisposableExtension.Dispose(_disposable);

    private void SubscribesTrigger()
    {
       _playerView.MyTrigger.OnTriggerEnterAsObservable()
            .Where(t => t.gameObject.layer == LayerMask.NameToLayer(
                _playerView.TriggerNamesLayer.FirstOrDefault(name => name == LayerMask.LayerToName(t.gameObject.layer))))
            .Subscribe(other =>
                colliderReactCommand.Execute(other)
            ).AddTo(_disposable);

        colliderReactCommand
            .Subscribe(collider => TriggerLogik(collider))
            .AddTo(_disposable);
    }

    private void CalculateCameraDotVector()
    {
        Vector2 inputClamp =
            Vector2.ClampMagnitude(new Vector2(_inputHandler.JoystickHorizontal, _inputHandler.JoystickVertical), 1);

        _cameraDotinputVect =
            Quaternion.Euler(0, Camera.allCameras[0].transform.eulerAngles.y, 0) * new Vector3(inputClamp.x, 0, inputClamp.y);
    }

    private void ProcessMoveLogik()
    {
        moveDirection = 
            new Vector3(_cameraDotinputVect.x * _playerModel.Speed, moveDirection.y, _cameraDotinputVect.z * _playerModel.Speed);

        if (_playerView.isGrounded)
            moveDirection.y = 0;

        moveDirection.y -= Time.deltaTime * _playerModel.Gravity;
        _playerView.Move(moveDirection);
    }

    private void ProcessRotateLogik()
    {
        float rot = Mathf.Atan2(moveDirection.x, moveDirection.z) * Mathf.Rad2Deg;
        _playerView.RotateToWards(Quaternion.Euler(0, rot, 0), _playerModel.Speed);

        _cameraDotinputVect = _playerView.GetTransformInverseVector(_cameraDotinputVect);
    }

    private void ProccesMoveAnimation()
    {
        _playerView.DynamicFloatAnimate("InputMagnitude", _cameraDotinputVect.magnitude, 0.5f);
        _playerView.DynamicFloatAnimate("Horizontal", _cameraDotinputVect.x, 0.05f);
        _playerView.DynamicFloatAnimate("Vertical", _cameraDotinputVect.z, 0.05f);
    }
    private void TriggerLogik(Collider coll)
    {
        Debug.Log(coll.name);
    }
}