using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Casing : MonoBehaviour
{
    public float timeBeforeSlefDestruct;
    private GameObject emitter;

    public void SetEmitter(GameObject _emitter)
    {
        emitter = _emitter;
        Physics.IgnoreCollision(emitter.GetComponent<Collider>(), GetComponent<Collider>());
    }

    private void Start()
    {
        StartCoroutine(SelfDestruct(timeBeforeSlefDestruct));
    }

    IEnumerator SelfDestruct(float seconds)
    {
        yield return new WaitForSeconds(seconds);
        Destroy(this.gameObject);
    }
}
