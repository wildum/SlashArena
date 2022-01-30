using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.XR.Interaction.Toolkit;

public class Hand : MonoBehaviour
{
    const float FLYING_SPEED = 1.0f;
    const float HOOK_STOP_DISTANCE = 0.5f;

    [SerializeField]
    private Hook hook;

    [SerializeField]
    private Rigidbody myBody;

    [SerializeField]
    private Cable cable;

    private ActionBasedController controller;

    // Start is called before the first frame update
    void Start()
    {
        controller = GetComponent<ActionBasedController>();
        controller.activateAction.action.performed += Hook_performed;
    }

    private void Hook_performed(UnityEngine.InputSystem.InputAction.CallbackContext obj)
    {
        if (hook != null)
        {
            Rigidbody rigidbody = hook.gameObject.GetComponent<Rigidbody>();
            if (!hook.Sent)
                rigidbody.AddForce(transform.forward * Hook.HOOK_SPEED, ForceMode.Force);
            rigidbody.isKinematic = false;
            hook.Sent = !hook.Sent;
            hook.Hooked = false;
        }
    }

    void Update()
    {
        if (!hook.Sent)
        {
            hook.gameObject.transform.position = transform.position;
            cable.gameObject.SetActive(false);
        }
        else
        {
            cable.gameObject.SetActive(true);
        }

        if (hook.Hooked)
        {
            Vector3 direction = (hook.gameObject.transform.position - transform.position).normalized;
            myBody.AddForce(direction * FLYING_SPEED, ForceMode.Force);
        }
    }

    public bool Hooked {get {return hook.Hooked;}}
}
