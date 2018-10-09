using EnumCollection;
using System.Collections.Generic;
using UnityEngine;
using Common;
namespace Manager
{
    public class GlobalGameStatus
    {
        protected static int remainPower = 0;
        protected static int requirePower = 0;

        public static int RequirePower
        {
            get { return requirePower; }
            protected set
            {
                requirePower = value;
                if (RemainPower < RequirePower)
                    StoredManager.PowerLow();
                else
                    StoredManager.PowerHight();
            }
        }
        public static int RemainPower
        {
            get { return remainPower; }
            protected set
            {
                remainPower = value;
                if (RemainPower < RequirePower)
                    StoredManager.PowerLow();
                else
                    StoredManager.PowerHight();
            }
        }
        public static float Gold { get; protected set; }

        public static List<ConstructId> ConstructsCantBuild = new List<ConstructId>();

        public static void NewConstructBuilded(Construct construct)
        {
            ConstructId[] unlock = construct.Owned;
            for (int i = 0; i < unlock.Length; i++)
            {
                ConstructsCantBuild.Add(unlock[i]);
#if UNITY_EDITOR
                Debug.Log(unlock[i] + " add cant build list");
#endif
            }
        }

        public static void ConstructDestroyed(Construct construct)
        {
            ConstructId[] unlock = construct.Owned;
            for (int i = 0; i < unlock.Length; i++)
            {
                ConstructsCantBuild.RemoveAt(ConstructsCantBuild.IndexOf((unlock[i])));
#if UNITY_EDITOR
                Debug.Log(unlock[i] + " remove cant build list");
#endif
            }
        }

        public static void ReceiveGold(float gold)
        {
            Gold += gold;
        }

        public static PayGoldStatus PayGold(float requireGold, out float debt)
        {
            debt = requireGold;
            if (Gold <= 0) return PayGoldStatus.Terminal;
            if (Gold < requireGold)
            {
                debt = requireGold - Gold;
                return PayGoldStatus.Pause;
            }
            debt = 0;
            return PayGoldStatus.Success;
        }

        public static void PowerBuilded(Power building)
        {
            RemainPower += building.PowerVolume;
        }

        public static void PowerBuildDestroyed(Power building)
        {
            RemainPower -= building.PowerVolume;
        }

        public static void IncreaseRequirePower(int plus)
        {
            RequirePower += plus;
        }
        public static void DecreaseRequirePower(int subtract)
        {
            RequirePower -= subtract;
        }
    }
}
