using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
    public float damage;
    public float timeBeforeSelfDestruct;
    private float speed;
    private GameObject emitter;

    public void SetSpeed(float _speed) { speed = _speed; }
    public void SetEmitter(GameObject _emitter) {
        emitter = _emitter;
        Physics.IgnoreCollision(emitter.GetComponent<Collider>(), GetComponent<Collider>());
    }

    private void Start()
    {
        StartCoroutine(SelfDestruct(timeBeforeSelfDestruct));
    }

    private void Update()
    {
        float moveDistance = Time.deltaTime * speed;
        transform.Translate(Vector3.forward * moveDistance);
        CheckCollision(moveDistance);
    }

    private void CheckCollision(float moveDistance)
    {
        Ray ray = new Ray(transform.position, transform.forward);
        RaycastHit hit;
        if (Physics.Raycast(ray, out hit, moveDistance))
        {
            IDamageable damageableObject = hit.collider.gameObject.GetComponent<IDamageable>();
            if (damageableObject != null)
                damageableObject.TakeHit(damage);
            Explode();
        }

    }

    /*private void OnCollisionEnter(Collision collision)
    {
        IDamageable damageableObject = collision.gameObject.GetComponent<IDamageable>();
        if (damageableObject != null)
            damageableObject.TakeHit(damage);
        Explode();
    }*/

    IEnumerator SelfDestruct(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Explode();
    }

    private void Explode()
    {
        Destroy(gameObject);
    }
}
