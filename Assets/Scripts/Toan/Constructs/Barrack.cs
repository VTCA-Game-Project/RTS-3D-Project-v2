﻿using EnumCollection;
using Pattern;
using UnityEngine;
using Utils;

namespace Common.Building
{
    public class Barrack : Construct
    {
        protected void Awake()
        {
            SoilderElement[] soilderElements = FindObjectsOfType<SoilderElement>();
            if (soilderElements != null)
                for (int i = 0; i < soilderElements.Length; i++)
                {
                    soilderElements[i].setsomething(CreateSoldier);
                }
        }
        protected override void Start()
        {
            Id = ConstructId.Barrack;
            IsActive = true;
            IsUsePower = false;
            ConsumePower = 0;
            base.Start();
        }

        public GameObject Produce(System.Enum type)
        {
            if (type.GetType() == typeof(Soldier))
            {
                return Singleton.AssetUtils.GetAsset(type.ToString()) as GameObject;
            }
            return null;
        }

        public void CreateSoldier(Soldier type)
        {
            Debug.Log("Create " + type);
        }
    }
}
