using EnumCollection;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Common.Building;

namespace Manager
{
    public class GlobalGameStatus
    {

        private int remainPower = 0;
        private int requirePower = 0;
        public static GlobalGameStatus Instance = new GlobalGameStatus();

        public float Gold { get; private set; }
        public GameStatus Status { get; private set; }
        public static List<ConstructId> ConstructsCantBuild { get; private set; }
        
        private GlobalGameStatus()
        {
            ConstructsCantBuild = new List<ConstructId>();
        }
                 
        public int RequirePower
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
        public int RemainPower
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

        public void NewConstructBuilded(Construct construct)
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

        public void ConstructDestroyed(Construct construct)
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

        public void TakeGold(float gold)
        {
            Gold += gold;
        }

        public PayGoldStatus PayGold(float requireGold, out float debt)
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

        public void PowerBuilded(Power building)
        {
            RemainPower += building.PowerVolume;
        }

        public void PowerBuildDestroyed(Power building)
        {
            RemainPower -= building.PowerVolume;
        }

        public void IncreaseRequirePower(int plus)
        {
            RequirePower += plus;
        }

        public void DecreaseRequirePower(int subtract)
        {
            RequirePower -= subtract;
        }
    }
}
