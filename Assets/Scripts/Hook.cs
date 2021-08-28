using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Hook : MonoBehaviour
{
    public const float HOOK_SPEED = 3000f;

    private bool hooked = false;
    private bool sent = false;

    Vector3 lastVelocity;
    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void FixedUpdate()
    {
        lastVelocity = rb.velocity;
    }

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (sent)
        {
            rb.transform.position = collisionInfo.GetContact(0).point;
            rb.velocity = Vector3.zero;
            rb.angularVelocity = Vector3.zero;
            rb.isKinematic = true;
            hooked = true;
        }
    }

    public bool Hooked { get {return hooked;} set {hooked=value;}}
    public bool Sent { get {return sent;} set {sent=value;}}
}
