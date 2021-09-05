using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : Limb
{
    public override void perfomAttack(Transform target)
    {
        Vector3 direction = (target.position - handle.position).normalized;
        handle.position = handle.position +  direction * (MOVE_LIMB_SPEED * Time.deltaTime);
    }

    public override void backToIdle()
    {
        if (Vector3.Distance(idleLocalPositionHandle, handle.localPosition) > MOVE_RADIUS)
        {
            Vector3 direction = (idleLocalPositionHandle - handle.localPosition).normalized;
            handle.localPosition = handle.localPosition + direction * (MOVE_LIMB_SPEED * Time.deltaTime);
        }
    }
}
