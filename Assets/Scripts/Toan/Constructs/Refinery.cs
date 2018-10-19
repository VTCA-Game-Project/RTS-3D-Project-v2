using System;
using EnumCollection;
using InterfaceCollection;
using Manager;
using UnityEngine;

namespace Common.Building
{
    public class Refinery : Construct,IProduce
    {
        public float RefreshTime;
        public int MaxRetainGold { get; protected set; }
        public int RemainingGold { get; protected set; }

        private float counter;
        protected override void Start()
        {
            counter = 0.0f;

            Id = ConstructId.Refinery;
            base.Start();
        }

        protected override void Update()
        {
            counter += Time.deltaTime;
            if(counter > RefreshTime)
            {
                counter = RefreshTime;
            }
            RemainingGold = (int)(MaxRetainGold * (counter / RefreshTime));
        }
        protected void OnMouseDown()
        {
            if (player.Group == Group.Player)
            {
                Produce(null);
            }
        }

        public void Produce(Enum type = null)
        {
            if (RemainingGold > 0)
            {
                player.TakeGold(RemainingGold);
                counter = 0.0f;
                RemainingGold = 0;
            }
        }
    }
}