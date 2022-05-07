using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(TankController))]
public class PlayerTankControl : LivingEntity
{
    public GameObject sceneQuitterPrefab;

    TankController controller;

    public override void Start()
    {
        base.Start();
        controller = GetComponent<TankController>();
    }

    void Update()
    {
        HandleTurret();
        Shoot();
    }

    private void FixedUpdate()
    {
        HandleMovement();
    }

    private void OnDestroy()
    {
        Instantiate(sceneQuitterPrefab);
    }

    #region Custom Methods
    protected virtual void HandleMovement()
    {
        Vector3 desiredDirection = new Vector3(Input.GetAxisRaw("Horizontal"), 0, Input.GetAxisRaw("Vertical"));
        controller.MoveTowards(desiredDirection);
    }

    protected virtual void HandleTurret()
    {
        Ray ray = Camera.main.ScreenPointToRay(Input.mousePosition);
        Plane hitPlane = new Plane(Vector3.up, controller.turret.transform.position);
        float rayDistance;
        if (hitPlane.Raycast(ray, out rayDistance))
        {
            Vector3 hitPoint = ray.GetPoint(rayDistance);
            controller.LookAt(hitPoint);
        }
    }

    protected virtual void Shoot()
    {
        if (Input.GetMouseButton(0))
            controller.Shoot_Cannon();
        if (Input.GetMouseButton(1))
            controller.Shoot_MachineGun();
    }
    #endregion Custom Methods
}
