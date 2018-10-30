using Common.Entity;
using EnumCollection;
using InterfaceCollection;
using Manager;
using Pattern;
using UnityEngine;

namespace Common.Building
{
    public class Barrack : Construct,IProduce
    {
        protected override void Awake()
        {
            base.Awake();
        }
        protected override void Start()
        {            
            SoilderElement[] buySoliderButtons = FindObjectsOfType<SoilderElement>();
            Debug.Log(buySoliderButtons);
            if(buySoliderButtons != null)
            {
                for (int i = 0; i < buySoliderButtons.Length; i++)
                {
                    Debug.Log(buySoliderButtons[i].name);
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
          
            GameObject prefab = GetSoldier(type);
            if(prefab != null)
            {
                AIAgent agent = Instantiate(prefab, transform.position, Quaternion.identity).GetComponent<AIAgent>();
                agent.Owner = Player;
                agent.gameObject.SetActive(true);
                agent.SetTarget(TargetType.Place, Vector3.ProjectOnPlane(transform.position + transform.forward * 5,Vector3.up));
            }
        }
    }
}
