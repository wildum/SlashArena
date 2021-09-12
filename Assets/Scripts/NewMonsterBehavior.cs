using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class NewMonsterBehavior : MonoBehaviour
{
    const float ATTACK_RANGE = 10;
    const float ATTACK_TIME = 5;
    const float ATTACK_ANGLE = 30;
    const float MOVE_ANGLE = 10;
    const float MOVE_SPEED = 4;
    const float ROTATION_SPEED = 1f;

    public Transform target;
    static Animator animator;

    private BodyLimbsMonitoring bodyLimbsMonitoring;
    private MonsterState state = MonsterState.MOVING;
    private float timeAttack = 0;


    void Start()
    {
        bodyLimbsMonitoring = GetComponent<BodyLimbsMonitoring>();
        animator = GetComponent<Animator>();
    }

    void Update()
    {
        if (bodyLimbsMonitoring.Alive)
        {
            if (!isAttacking())
            {
                if (targetInRange())
                {
                    animator.SetBool("isIdle", true);
                    animator.SetBool("isWalking", false);
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

    void move()
    {
        Vector3 direction = target.position - transform.position;
        direction.y = 0;
        float angle = Vector3.Angle(direction, transform.forward);
        if (angle < MOVE_ANGLE && getDistanceWithTarget() > ATTACK_RANGE)
        {
            this.transform.Translate(0,0,MOVE_SPEED*Time.deltaTime);
        }
        animator.SetBool("isIdle", false);
        animator.SetBool("isWalking", true);
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
        Debug.Log(dist);
        return dist;
    }

    void attack()
    {
        state = MonsterState.ATTACKING;
    }

    bool isAttacking()
    {
        if (state == MonsterState.ATTACKING)
        {
            timeAttack += Time.deltaTime;
            if (timeAttack > ATTACK_TIME)
            {
                state = MonsterState.MOVING;
                timeAttack = 0;
            }
        }
        else
        {
            timeAttack = 0;
        }
        return state == MonsterState.ATTACKING;
    }
}
