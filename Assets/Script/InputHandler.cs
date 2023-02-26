using UniRx;
using UnityEngine;

public class InputHandler : MonoBehaviour
{
    [SerializeField] private Joystick joystick;

    public float JoystickHorizontal => joystick.Horizontal;
    public float JoystickVertical => joystick.Vertical;
    public Vector3 Direction => joystick.Direction;

    public readonly BoolReactiveProperty IsRightClickMouseProp = new BoolReactiveProperty(false);
    public readonly BoolReactiveProperty IsLeftClickMouseProp = new BoolReactiveProperty(false);
    public readonly BoolReactiveProperty IsTapProp = new BoolReactiveProperty(false);

    private void Update()
    {
        IsRightClickMouseProp.Value = Input.GetMouseButtonDown(0);
        IsLeftClickMouseProp.Value = Input.GetMouseButtonDown(1);
        IsTapProp.Value = Input.touchCount > 0;
    }
}
