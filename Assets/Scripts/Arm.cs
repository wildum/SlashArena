using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Arm : Limb
{
    public override void perfomAttack(Transform target, float actionTime)
    {
        handle.position = Vector3.Lerp(handle.position, target.position, actionTime * Time.deltaTime);
    }

    public override void backToIdle(float actionTime)
    {
        handle.localPosition = Vector3.Lerp(handle.localPosition, idleLocalPositionHandle, actionTime * Time.deltaTime);
    }
}
