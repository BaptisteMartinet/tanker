using UnityEngine;

[RequireComponent (typeof(Rigidbody))]
public class TankController : MonoBehaviour
{
    public float tankSpeed;
    public float rotationSpeed;
    public float turretRotationSpeed;

    public GameObject turret;
    public GunController canon;
    public GunController machineGun;

    public ParticleSystem explosionEffect;

    private Rigidbody rb;

    private AudioManager audioManager;
    private AudioSource idleSound;
    private AudioSource movingSound;

    private void Awake()
    {
        audioManager = FindObjectOfType<AudioManager>();
        idleSound = audioManager.createSourceInstance("tank_idle", this.gameObject);
        movingSound = audioManager.createSourceInstance("tank_move", this.gameObject);
    }

    void Start()
    {
        rb = GetComponent<Rigidbody>();
        idleSound.Play();
    }

    private void Update()
    {
        manageAudio();
    }

    private void OnDestroy()
    {
        audioManager.Play("tank_destruction");
        Instantiate(explosionEffect, transform.position, Quaternion.identity);
    }

    private void manageAudio()
    {
        if (rb.velocity.sqrMagnitude == 0 && !idleSound.isPlaying)
        {
            movingSound.Stop();
            idleSound.Play();
        }
        else if (rb.velocity.sqrMagnitude > 0 && !movingSound.isPlaying)
        {
            idleSound.Stop();
            movingSound.Play();
        }
    }

    #region Custom Methods
    public void MoveTowards(Vector3 dir)
    {
        if (dir.magnitude == 0f)
            return;
        if (Vector3.Dot(transform.forward, dir) < -0.95)
            dir = Quaternion.AngleAxis(20, Vector3.up) * dir;
        transform.forward = Vector3.Lerp(transform.forward, dir, rotationSpeed * Time.deltaTime);
        rb.MovePosition(transform.position + (transform.forward * tankSpeed * Time.deltaTime));
    }

    public void LookAt(Vector3 pos)
    {
        pos.y = turret.transform.position.y;
        Quaternion rotation = Quaternion.LookRotation(pos - turret.transform.position);
        turret.transform.rotation = Quaternion.Lerp(turret.transform.rotation, rotation, Time.deltaTime * turretRotationSpeed);
    }

    public void Shoot_Cannon()
    {
        canon?.Shoot(this.gameObject);
    }

    public void Shoot_MachineGun()
    {
        machineGun?.Shoot(this.gameObject);
    }
    #endregion Custom Methods
}