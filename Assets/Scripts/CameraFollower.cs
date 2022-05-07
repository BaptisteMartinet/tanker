using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraFollower : MonoBehaviour
{
    public Transform target;
    public float followEase;

    private Vector3 offset;

    void Start()
    {
        offset = transform.position - target.transform.position;
    }

    private void FixedUpdate()
    {
        if (target)
            transform.position = Vector3.Lerp(transform.position, target.position + offset, followEase * Time.deltaTime);
    }
}
