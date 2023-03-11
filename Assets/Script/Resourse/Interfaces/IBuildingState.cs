
using Assets.Script.Resourse;
using Building;
using Resource;

public abstract class BuildingState
{
    protected BaseResource[] baseResources;
    protected ResourceWarhouse resourceWarhouse;

    public bool isExit { get; private set; }

    protected BuildingState(ResourceWarhouse resourceWarhouse)
    {
        this.resourceWarhouse = resourceWarhouse;
    }

    protected virtual void GetResources(int countResources)
    {
        resourceWarhouse.RemoveResources(countResources);
    }

    public abstract void Enter();
    public virtual void Run()
    {
        if (resourceWarhouse.isStack)
            Exit();
    }
    public virtual void Exit()
    {
        isExit = true;
    }
}
