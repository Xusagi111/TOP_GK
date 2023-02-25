using UniRx;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Joystick joystick;

    public float JoystickHorizontal => joystick.Horizontal;
    public float JoystickVertical => joystick.Vertical;
    public Vector3 Direction => joystick.Direction;

    public ReactiveProperty<bool> IsRightClickMouseProp = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> IsLeftClickMouseProp = new ReactiveProperty<bool>(false);
    public ReactiveProperty<bool> IsTapProp = new ReactiveProperty<bool>(false);

    private void Update()
    {
        IsRightClickMouseProp.Value = Input.GetMouseButtonDown(0);
        IsLeftClickMouseProp.Value = Input.GetMouseButtonDown(1);
        IsTapProp.Value = Input.touchCount > 0;
    }
}
