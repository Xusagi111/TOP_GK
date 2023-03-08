namespace Assets.Script.Game_Buildings.State
{
    public interface IBuildingState
    {
        public void Enter();
        public void Exit();
        public void IUpdate();
    }
}
