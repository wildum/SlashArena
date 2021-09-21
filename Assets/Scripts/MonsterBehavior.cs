using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Animations.Rigging;

public enum MonsterState
{
    MOVING, ATTACKING
}

public class MonsterBehavior : MonoBehaviour
{
    const float ATTACK_RANGE = 6;
    const float ATTACK_ANGLE = 30;
    const float MOVE_ANGLE = 10;
    const float MOVE_SPEED = 4;
    const float ROTATION_SPEED = 1f;
    const float PUNCH_SPEED = 1.0f;

    private enum RigAnimMode
    {
        off,
        inc,
        dec
    }

    public Transform target;

    private BodyLimbsMonitoring bodyLimbsMonitoring;
    private MonsterState state = MonsterState.MOVING;
    private float timeAttack = 0;

    public ChainIKConstraint leftHand;
    public Transform leftPunchTarget = null;
    private RigAnimMode leftHandRigAnim = RigAnimMode.off;

    public ChainIKConstraint rightHand;
    public Transform rightPunchTarget = null;
    private RigAnimMode rightHandRigAnim = RigAnimMode.off;
    
    public TwoBoneIKConstraint leftFoot;
    public Transform leftKickTarget = null;
    private RigAnimMode leftFootRigAnim = RigAnimMode.off;

    public TwoBoneIKConstraint rightFoot;
    public Transform rightKickTarget = null;
    private RigAnimMode rightFootRigAnim = RigAnimMode.off;

    void Start()
    {
        bodyLimbsMonitoring = GetComponent<BodyLimbsMonitoring>();
    }

    void Update()
    {
        if (bodyLimbsMonitoring.Alive)
        {
            if (!isAttacking())
            {
                if (targetInRange())
                {
                    rotate();
                    attack();
                }
                else
                {
                    rotate();
                    move();
                }
            }
        }
    }

    void FixedUpdate()
    {
        leftHand.weight = updateHitAnimation(leftHand.weight, ref leftHandRigAnim);
        rightHand.weight = updateHitAnimation(rightHand.weight, ref rightHandRigAnim);
        leftFoot.weight = updateHitAnimation(leftFoot.weight, ref leftFootRigAnim);
        rightFoot.weight = updateHitAnimation(rightFoot.weight, ref rightFootRigAnim);
    }

    float updateHitAnimation(float weight, ref RigAnimMode mode)
    {
        switch (mode)
        {
            case RigAnimMode.inc:
                weight = Mathf.Lerp(weight, 1, PUNCH_SPEED * Time.deltaTime);
                if (weight > 0.95)
                {
                    weight = 1;
                    mode = RigAnimMode.dec;
                }
                break;
            case RigAnimMode.dec:
                weight = Mathf.Lerp(weight, 0, PUNCH_SPEED * Time.deltaTime);
                if (weight > 0.1)
                {
                    weight = 0;
                    mode = RigAnimMode.off;
                }
                break;
            case RigAnimMode.off:
                weight = 0;
                break;
        }
        return weight;
    }

    void move()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, transform.forward);
        if (angle < MOVE_ANGLE && getDistanceWithTarget() > ATTACK_RANGE)
        {
            this.transform.Translate(0,0,MOVE_SPEED*Time.deltaTime);
        }
        state = MonsterState.MOVING;
    }

    void rotate()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), ROTATION_SPEED*Time.deltaTime);
    }

    bool targetInRange()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, transform.forward);
        return getDistanceWithTarget() <= ATTACK_RANGE && angle <= ATTACK_ANGLE;
    }

    float getDistanceWithTarget()
    {
        Vector3 targetPos = target.position;
        Vector3 myPos = transform.position;
        targetPos.y = 0;
        myPos.y = 0;
        float dist = Vector3.Distance(targetPos, myPos);
        return dist;
    }

    void attack()
    {
        state = MonsterState.ATTACKING;
        GameObject g = bodyLimbsMonitoring.getClosestPartFromTarget(target);
        if (g != null)
        {
            if (g.name == "LeftHand")
                hit(leftPunchTarget, ref leftHandRigAnim);
            else if (g.name == "RightHand")
                hit(rightPunchTarget, ref rightHandRigAnim);
            else if (g.name == "LeftFoot")
                hit(leftKickTarget, ref leftFootRigAnim);
            else if (g.name == "RightFoot")
                hit(rightKickTarget, ref rightFootRigAnim);
            leftHand.weight = 0;
            rightHand.weight = 0;
            leftFoot.weight = 0;
            rightFoot.weight = 0;
        }
    }

    void hit(Transform handTarget, ref RigAnimMode handRigAnim)
    {
        handTarget.position = new Vector3(target.position.x, target.position.y, target.position.z);
        handRigAnim = RigAnimMode.inc;
    }

    bool isAttacking()
    {
        return leftHandRigAnim != RigAnimMode.off || rightHandRigAnim != RigAnimMode.off || leftFootRigAnim != RigAnimMode.off || rightFootRigAnim != RigAnimMode.off;
    }
}
