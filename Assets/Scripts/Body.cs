using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Body : MonoBehaviour
{
    const float MAX_HEIGHT = 15;

    [SerializeField]
    private Hook leftHook;

    [SerializeField]
    private Hook rightHook;

    Rigidbody rb;

    void Start()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (rightHook.Hooked || leftHook.Hooked)
        {
            rb.useGravity = false;
        }
        else
        {
            rb.useGravity = true;
        }
        Vector3 pos = transform.position;
        pos.y = Mathf.Min(pos.y, MAX_HEIGHT);
        transform.position = pos;
    }
}
