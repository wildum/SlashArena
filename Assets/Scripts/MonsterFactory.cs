using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum MonsterType
{
    Default
}

public class MonsterFactory : MonoBehaviour
{
    public GameObject monster;
    public Transform target;

    public GameObject createMonster(MonsterType type)
    {
        GameObject newMonster = null;
        switch (type)
        {
            case MonsterType.Default:
                newMonster = Instantiate(monster, new Vector3(0, 2, 30), Quaternion.LookRotation(Vector3.back, Vector3.up));
                newMonster.GetComponent<BodyLimbsMonitoring>().setBodyPartsType(
                    new List<BodyPartName>{BodyPartName.neck, BodyPartName.neckLeft, BodyPartName.neckRight}, 
                    new List<BodyPartName>{BodyPartName.head, BodyPartName.lowerBack, BodyPartName.upperBack},
                    new List<BodyPartName>{BodyPartName.footLeft, BodyPartName.footRight, BodyPartName.handLeft, BodyPartName.handRight});
                break;
        }
        MonsterBehavior m = newMonster.GetComponent<MonsterBehavior>();
        assignTargets(m);
        return newMonster;
    }

    private void assignTargets(MonsterBehavior m)
    {
        m.target = target;
        // m.leftKickTarget = new GameObject("leftKickTarget").transform;
        // m.leftFoot.data.target = m.leftKickTarget;
        // m.rightKickTarget = new GameObject("rightKickTarget").transform;
        // m.rightFoot.data.target = m.rightKickTarget;
        // m.leftPunchTarget = new GameObject("leftPunchTarget").transform;
        // m.leftHand.data.target = m.leftPunchTarget;
        // m.rightPunchTarget = new GameObject("rightPunchTarget").transform;
        // m.rightHand.data.target = m.rightPunchTarget;
    }
}
