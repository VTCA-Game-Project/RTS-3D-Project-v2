using Common.Entity;
using InterfaceCollection;
using UnityEngine;

public class AIGoblinWizard : AIAgent,IAttackable
{
    public override void Attack()
    {
        Debug.Log("Fire event");
    }
}
