using Assets.Script.Resourse;
using Resource;
using UnityEngine;
using Zenject;

public class InputInstaller : MonoInstaller
{
    [SerializeField] private InputHandler _input;
    public override void InstallBindings()
    {
        BindInputHandler();
    }

    private void BindInputHandler()
    {
        Container.
            Bind<InputHandler>().
            FromInstance(_input).
            AsSingle().NonLazy();
    }
}