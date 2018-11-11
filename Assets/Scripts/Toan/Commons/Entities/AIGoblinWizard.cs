using Common.Entity;
using InterfaceCollection;
using UnityEngine;

public class AIGoblinWizard : AIAgent
{
    public Rigidbody effect;

    public Transform startpoisition;
    public float ShootForce;





    public void Update()
    {
    }
    public override void Attack()
    {       
        base.Attack();
        if(TargetEntity != null)
        {
            Rigidbody ef = Instantiate(effect, startpoisition.position, transform.rotation);
            ef.gameObject.SetActive(true);
            ef.AddForce(transform.forward * ShootForce);

            TargetEntity.TakeDamage(Damage);
        }
    }
}
