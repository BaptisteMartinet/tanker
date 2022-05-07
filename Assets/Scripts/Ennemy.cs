using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(TankController))]
public class Ennemy : LivingEntity
{
    TankController controller;
    Transform target;
    public float moveDistance;
    public float attackDistance;

    public override void Start()
    {
        base.Start();
        controller = GetComponent<TankController>();
        target = GameObject.FindGameObjectWithTag("Player").transform;
    }

    private void FixedUpdate()
    {
        if (!target)
            return;
        float distanceFromPlayer = Vector3.Distance(this.transform.position, target.position);
        Vector3 dir = (target.position - this.transform.position).normalized;
        if (distanceFromPlayer <= moveDistance)
            controller.MoveTowards(dir);
        if (distanceFromPlayer <= attackDistance)
        {
            controller.LookAt(target.position);
            controller.Shoot_Cannon();
        }
    }
}
