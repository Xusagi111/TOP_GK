using Resourse;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace Building
{
    //При добавление новых ресурсов, класс должен расшириться.

    public class LogicContact : MonoBehaviour
    {
        public IEnumerator GetResource(BaseWarehouse baseWarehouse, List<BaseResourse> Inventory, Transform EndMovePosition)
        {
            int ShortageLog = 0;
            int ShortageBoard = 0;
            List<Log> LogInv = new List<Log>();
            List<Board> BoardInv = new List<Board>();

            if (baseWarehouse.LogResource != null)
            {
                ShortageLog = baseWarehouse.LogResource.MaxElement - baseWarehouse.LogResource.CountElement;
            }

            if (baseWarehouse.BoardResource != null)
            {
                ShortageBoard = baseWarehouse.BoardResource.MaxElement - baseWarehouse.BoardResource.CountElement;
            }

            for (int i = 0; i < Inventory.Count; i++)
            {
                var Data = Inventory[i];
                if (ShortageLog != 0 && Data is Log log) LogInv.Add(log);
                else if(ShortageBoard != 0 && Data is Board board) BoardInv.Add(board);
            }

            for (int i = 0; i < LogInv.Count; i++)
            {
                yield return MoveAnimationObj(LogInv, baseWarehouse.LogResource, EndMovePosition, Inventory);
            }
            for (int i = 0; i < BoardInv.Count; i++)
            {
                yield return MoveAnimationObj(BoardInv, baseWarehouse.BoardResource, EndMovePosition, Inventory);
            }
        }

        public float MoveAnimationObj<T>(List<T> Resourse, ResourceWarhouse<T> EndINventory, Transform EndPosition, List<BaseResourse> Inventory) where T : BaseResourse
        {
            if (EndINventory == null)
            {
                Debug.LogError("Inventory Null");
                return 0;
            }
            if (EndINventory.MaxElement < EndINventory.CountElement + 1)
            {
                return 0;
            }

            Resourse[0].transform.position = EndPosition.position;
            EndINventory.CountElement++;
            Inventory.Remove(Resourse[0]);
            Resourse.RemoveAt(0);
            return Global.s_TimeMoveResourse;
        }
    }
}