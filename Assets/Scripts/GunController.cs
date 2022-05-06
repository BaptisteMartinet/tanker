using UnityEngine;

public class GunController : MonoBehaviour
{
    public Transform muzzleNoze;
    public Transform casingOrigin;
    public Projectile projectile;
    public Casing casing;
    public float muzzleVelocity;
    public float fireRate;
    public float casingForce;

    private float lastTime;

    public void Start()
    {
        lastTime = Time.time;
    }

    public void Shoot(GameObject emitter)
    {
        if ((Time.time - lastTime) < fireRate)
            return;
        lastTime = Time.time;
        Vector3 bulletOrigin = muzzleNoze ? muzzleNoze.position : transform.position;
        Projectile newProjectile = Instantiate(projectile, bulletOrigin, transform.rotation);
        newProjectile.SetEmitter(emitter);
        newProjectile.SetSpeed(muzzleVelocity);

        if (casing && casingOrigin)
        {
            Casing newCasing = Instantiate(casing, casingOrigin.position, transform.rotation);
            newCasing.SetEmitter(emitter);
            Rigidbody newCasingBody = newCasing.GetComponent<Rigidbody>();
            newCasingBody.AddForce(transform.right * casingForce);
        }
    }
}
