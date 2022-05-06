using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent (typeof(Renderer))]
public class TankWheelTextureScroller : MonoBehaviour
{
    private Rigidbody tankRigidbody;
    private Renderer wheelRenderer;
    private float wheelOffset = 0f;

    void Start()
    {
        tankRigidbody = GetComponentInParent<Rigidbody>();
        wheelRenderer = GetComponent<Renderer>();
    }

    void Update()
    {
        if (tankRigidbody.velocity.magnitude != 0)
        {
            float scrollSpeed = 0.05f; // TODO to be computed
            wheelOffset = (wheelOffset + scrollSpeed * Time.deltaTime) % 1f;
            wheelRenderer.material.SetTextureOffset("_MainTex", new Vector2(wheelOffset, 0f));
        }
    }
}
