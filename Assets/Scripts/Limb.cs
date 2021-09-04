using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Limb : MonoBehaviour
{
    public abstract void perfomAttack(Transform target, float actionTime);
    public abstract void backToIdle(float actionTime);

    public Transform handle;
    public Transform secondLimbPart;

    protected Vector3 idleLocalPositionHandle;

    private float attackTiming = 0;

    private Transform target;
    private float attackDuration;

    private bool isAttacking = false;

    private void Start()
    {
        idleLocalPositionHandle = handle.localPosition;
    }

    private void Update()
    {
        if (handle != null)
        {
            if (isAttacking)
            {
                attackTiming += Time.deltaTime;
                if (attackTiming < attackDuration)
                {
                    perfomAttack(target, attackDuration);
                }
                else
                {
                    attackTiming = 0;
                    isAttacking = false;
                }
            }
            else
            {
                backToIdle(attackDuration);
            }
        }
    }

    public void attack(Transform itarget, float iattackDuration)
    {
        isAttacking = true;
        target = itarget;
        attackDuration = iattackDuration / 2;
    }

    public bool hasHandle()
    {
        return handle != null;
    }

}
