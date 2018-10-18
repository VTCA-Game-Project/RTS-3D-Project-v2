﻿using EnumCollection;
using InterfaceCollection;
using Pattern;
using UnityEngine;

namespace Common.Building
{
    public class Barrack : Construct,IProduce
    {
        protected override void Start()
        {
            SoilderElement[] buySoliderButtons = FindObjectsOfType<SoilderElement>();
            if(buySoliderButtons != null)
            {
                for (int i = 0; i < buySoliderButtons.Length; i++)
                {
                    buySoliderButtons[i].setsomething(Produce);
                }
            }
            Id = ConstructId.Barrack;
            base.Start();
        }

        public GameObject GetSoldier(System.Enum type)
        {
            if (type.GetType() == typeof(Soldier))
            {
                return Singleton.AssetUtils.GetAsset(type.ToString()) as GameObject;
            }
            return null;
        }

        public void Produce(System.Enum type)
        {
            GetSoldier(type);
        }
    }
}
