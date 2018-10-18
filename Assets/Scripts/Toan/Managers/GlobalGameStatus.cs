using EnumCollection;
using System.Collections.Generic;
using UnityEngine;
using Common;
using Common.Building;

namespace Manager
{
    public class GlobalGameStatus
    {
        public static GlobalGameStatus Instance = new GlobalGameStatus();

        public float Gold { get; private set; }
        public GameStatus Status { get; private set; }
        public static List<ConstructId> ConstructsCantBuild { get; private set; }
        
        private GlobalGameStatus()
        {
            ConstructsCantBuild = new List<ConstructId>();
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
    }
}
