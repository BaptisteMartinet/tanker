using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Projectile : MonoBehaviour
{
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
        transform.Translate(Vector3.forward * Time.deltaTime * speed);
    }

    private void OnCollisionEnter(Collision collision)
    {
        Explode();
    }

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
