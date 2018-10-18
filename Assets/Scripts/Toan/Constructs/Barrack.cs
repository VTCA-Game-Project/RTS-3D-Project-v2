using Common.Entity;
using EnumCollection;
using Pattern;
using UnityEngine;

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
            IsUsePower = false;
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
            GameObject soldier = Produce(type);
            if(soldier != null)
            {
                AIAgent agent = Instantiate(soldier, transform.position, Quaternion.identity).GetComponent<AIAgent>();
                agent.SetTarget(TargetType.Place, transform.position + new Vector3(0, 0, 10));
            }
        }
    }
}
