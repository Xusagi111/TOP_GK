using Assets.Script.Resourse;
using Building;
using Resource;
using System;
using System.Collections.Generic;
using TMPro;
using UnityEngine;


namespace Building
{
    //Рассположение основных точек взаимодействия на префабе 
    public class DataBulding : MonoBehaviour 
    {
        //Прокинуть триггеры через UNiRX
        [field: SerializeField] public Collider ConstructionBulding { get; private set; }
        [field: SerializeField] public Collider GetRes { get; private set; }
        [field: SerializeField] public Collider AddRes { get; private set; }
        [field: SerializeField] public TextMeshProUGUI TimeCreateOneResourceT { get; private set; }

        public void ConstructBuilding()
        {
            ConstructionBulding.gameObject.SetActive(true);
            GetRes.gameObject.SetActive(false);
            AddRes.gameObject.SetActive(false);
        }

        public void EndCreatingFactory() 
        {
            ConstructionBulding.gameObject.SetActive(false);
            GetRes.gameObject.SetActive(true);
            AddRes.gameObject.SetActive(true);
        }
        
        public void EndCreatingIcomeBuildings()
        {
            ConstructionBulding.gameObject.SetActive(false);
            GetRes.gameObject.SetActive(true);
        }
    }
}


public class BuildingState : MonoBehaviour
{
    private Dictionary<Type, StateBuilbing> behaviorMap;
    IBuildingState currentState;
    private DataBulding _dataBuilding;
    private void Awake()
    {
        InitBuildings();
    }

    private void InitBuildings()
    {
        //Прокидывания _dataBuilding не подходят
        behaviorMap = new Dictionary<Type, StateBuilbing>();
        behaviorMap[typeof(ConstructionBuilding<Log>)] = new ConstructionBuilding<Log>(_dataBuilding);
        behaviorMap[typeof(ConstructionBuilding<Log>)] = new ConstructionBuilding<Log>(_dataBuilding);
        behaviorMap[typeof(ConditionBuilding<MoneyObj>)] = new ConditionBuilding<MoneyObj>(_dataBuilding);
    }

    protected void SetBuilding(IBuildingState House)
    {
        if (currentState != null)
            currentState.Exit();

        currentState = House;
        currentState.Enter();
    }

    protected IBuildingState GetBuilding<T>() where T : IBuildingState
    {
        var type = typeof(T);
        return behaviorMap[type];
    }

    protected void SetConstruct<T>()
    {
        var behavior = GetBuilding<ConstructionBuilding<T>>();
        SetBuilding(behavior);
    }

    protected void SetCreateRes<AddResource, GetResource>(Collider TriggerContact)
    {
        var behavior = GetBuilding<ConditionBuilding<AddResource, GetResource>>();
        SetBuilding(behavior);
    }

    protected void SetCreateResNoAddResource<GetResource>()
    {
        var behavior = GetBuilding<ConditionBuilding<GetResource>>();
        SetBuilding(behavior);
    }
}


public class ConstructionBuilding<T> : StateBuilbing
{
    public ConstructionBuilding(DataBulding dataBulding)
    {
        dataBulding.ConstructBuilding();
        CurrentTypeRes = GetTypeBuilding.GetTypeRes(typeof(T));
    }
}

public class ConditionBuilding<AddResource, GetResource>: StateBuilbing
{
    protected EnumResource CurrentGetTypeRes;
    private Collider _addResTrigger;
    private Collider _getResTrigger;

    public ConditionBuilding(DataBulding dataBulding)
    {
        CurrentTypeRes = GetTypeBuilding.GetTypeRes(typeof(AddResource));
        CurrentGetTypeRes = GetTypeBuilding.GetTypeRes(typeof(GetResource));
        _addResTrigger = dataBulding.AddRes;
        _getResTrigger = dataBulding.GetRes;
        dataBulding.EndCreatingFactory();
    }
}

public class ConditionBuilding<GetResource> : StateBuilbing
{
    protected EnumResource CurrentGetTypeRes;
    private Collider _triggerContact;

    public ConditionBuilding(DataBulding dataBulding)
    {
        CurrentGetTypeRes = GetTypeBuilding.GetTypeRes(typeof(GetResource));
        dataBulding.EndCreatingIcomeBuildings(); //Возможно перемещение в  Enter and Exit
    }
}

public abstract class StateBuilbing : IBuildingState
{
    protected EnumResource CurrentTypeRes = EnumResource.NullType;
    [field: SerializeField] public TextMeshProUGUI TimeCreateOneResourceT { get; private set; } //Заинжектить данный тип 

    public virtual void Enter()
    {
        //Производить инициализацию базового типа
    }

    public virtual void Exit()
    {
       //Дестроить тип
    }
}

public interface IBuildingState
{
    public void Enter(); 
    public void Exit();
}

public static class GetTypeBuilding{
    public static EnumResource GetTypeRes(Type typeClass)
    {
        EnumResource CurrentTypeRes = EnumResource.NullType;

        if (typeClass == typeof(Log)) CurrentTypeRes = EnumResource.Log;
        else if (typeClass == typeof(Board)) CurrentTypeRes = EnumResource.Board;
        else if (typeClass == typeof(MoneyObj)) CurrentTypeRes = EnumResource.Money;
        else CurrentTypeRes = EnumResource.NullType;

        return CurrentTypeRes;
    } 
}