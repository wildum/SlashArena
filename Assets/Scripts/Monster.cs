using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public enum MonsterState
{
    MOVING, ATTACKING
}

public class Monster : MonoBehaviour
{
    const float ATTACK_RANGE = 10;
    const float ATTACK_TIME = 5;
    const float ATTACK_ANGLE = 30;
    const float MOVE_ANGLE = 10;
    const float MOVE_SPEED = 4; // unit/s
    const float ROTATION_SPEED = 1f;

    public Transform target;
    public GameObject head;
    public GameObject body;
    public GameObject neck;
    public Arm[] arms;
    public Leg[] legs;

    private MonsterState state = MonsterState.MOVING;

    private bool startVanishing = false;

    private List<Limb> limbs = new List<Limb>();

    private float timeAttack = 0;

    // Start is called before the first frame update
    void Start()
    {
        foreach(var g in arms)
            limbs.Add(g);
        foreach(var g in legs)
            limbs.Add(g);
    }

    void Update()
    {
        if (!startVanishing)
        {
            if (isDead())
            {
                destroyAllElements();
                startVanishing = true;
            }
            else
            {
                act();
            }
        }
    }

    void act()
    {
        if (!isAttacking())
        {
            if (targetInRange())
            {
                attack();
            }
            else
            {
                moveRotate();
            }
        }
    }

    void moveRotate()
    {
        Vector3 direction = target.position - transform.position;
        // can be adjusted here to have it look up or down
        direction.y = 0;
        transform.rotation = Quaternion.Slerp(transform.rotation, Quaternion.LookRotation(direction), ROTATION_SPEED*Time.deltaTime);
        float angle = Vector3.Angle(direction, transform.forward);
        if (angle < MOVE_ANGLE && Vector3.Distance(target.position, transform.position) > ATTACK_RANGE)
        {
            this.transform.Translate(0,0,MOVE_SPEED*Time.deltaTime);
        }
    }

    void attack()
    {
        state = MonsterState.ATTACKING;
        Limb attackLimb = findLimbToAttack();
        if (attackLimb != null)
        {
            attackLimb.attack(target, ATTACK_TIME);
        }
    }
    
    Limb findLimbToAttack()
    {
        Limb bestLimb = null;
        float bestDist = float.MaxValue;
        foreach (Limb l in limbs)
        {
            if (l.hasHandle())
            {
                float d = Vector3.Distance(l.transform.position, target.position);
                if (d < bestDist)
                {
                    bestDist = d;
                    bestLimb = l;
                }
            }
        }
        return bestLimb;
    }

    bool targetInRange()
    {
        Vector3 direction = target.position - transform.position;
        float distance = Vector3.Distance(target.position, transform.position);
        float angle = Vector3.Angle(direction, transform.forward);
        return distance <= ATTACK_RANGE && angle <= ATTACK_ANGLE;
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

    bool isDead()
    {
        return head == null || neck == null || body == null || !(limbs.Exists(x => x != null && x.transform.Find("Handle") != null));
    }

    void destroyAllElements()
    {
        if (head != null)
            head.AddComponent<EntityDestroyer>();
        if (body != null)
            body.AddComponent<EntityDestroyer>();
        if (neck != null)
            neck.AddComponent<EntityDestroyer>();
        foreach (Limb limb in limbs)
        {
            foreach (Transform child in limb.transform)
            {
                if (child.gameObject != null)
                    child.gameObject.AddComponent<EntityDestroyer>();
            }
        }
        Destroy(gameObject, 5);
    }
}
