using System.Collections.Generic;

namespace Building
{
    public class WarehouseCreateResourse<T,E>
    {
        public List<T> InputResources = new List<T>();
        public List<E> ExitingResourse =  new List<E>();
        public int CapacityInputR { get; private set; }
        public int CapacityExitR { get; private set; }

        public WarehouseCreateResourse(int InputR, int ExitR)
        {
            CapacityInputR = InputR;
            CapacityExitR = ExitR;
        }

        public void UpLevelInputR(float multiplierResourse)
        {
            CapacityInputR *= (int)(multiplierResourse * 100);
        }

        public void UpLevelExitR(float multiplierResourse)
        {
            CapacityExitR *= (int)(multiplierResourse * 100);
        }
    }
}