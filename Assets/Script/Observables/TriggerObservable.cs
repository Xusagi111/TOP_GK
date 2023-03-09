using System.Linq;
using UnityEngine;
using UniRx;
using UniRx.Triggers;

public class TriggerObservable: BaseObservable
{
    public readonly ReactiveCommand<Collider> colliderReactCommand = new ReactiveCommand<Collider>();
    private readonly string[] triggersNamesLayer;
    public TriggerObservable(string[] triggersNamesLayer)
    {
        this.triggersNamesLayer = triggersNamesLayer;
    }

    public void AddObservableTrigger(Collider trigger)
    {

    }
}
