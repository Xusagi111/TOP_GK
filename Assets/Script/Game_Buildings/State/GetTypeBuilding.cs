using Resource;
using System;

namespace Assets.Script.Game_Buildings.State
{
    public static class GetTypeBuilding
    {
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
}