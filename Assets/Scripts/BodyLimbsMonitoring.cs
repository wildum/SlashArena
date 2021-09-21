using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BodyLimbsMonitoring : MonoBehaviour
{
    public GameObject head;
    public GameObject neck;
    public GameObject neckRight;
    public GameObject shoulderRight;
    public GameObject upperArmRight;
    public GameObject lowerArmRight;
    public GameObject handRight;
    public GameObject neckLeft;
    public GameObject shoulderLeft;
    public GameObject upperArmLeft;
    public GameObject lowerArmLeft;
    public GameObject handLeft;
    public GameObject upperBack;
    public GameObject lowerBack;
    public GameObject pelvisLeft;
    public GameObject upperLegLeft;
    public GameObject lowerLegLeft;
    public GameObject footLeft;
    public GameObject pelvisRight;
    public GameObject upperLegRight;
    public GameObject lowerLegRight;
    public GameObject footRight;

    private bool alive = true;

    private bool leftLegAlive = true;
    private bool rightLegAlive = true;
    private bool leftArmAlive = true;
    private bool rightArmAlive = true;

    void Update()
    {
        if (alive)
        {
            if (checkAlive())
            {
                rightArmAlive = checkArmAlive(neckRight, shoulderRight, upperArmRight, lowerArmRight, handRight);
                leftArmAlive = checkArmAlive(neckLeft, shoulderLeft, upperArmLeft, lowerArmLeft, handLeft);
                rightLegAlive = checkLegAlive(pelvisRight, upperLegRight, lowerLegRight, footRight);
                leftLegAlive = checkLegAlive(pelvisLeft, upperLegLeft, lowerLegLeft, footLeft);
            }
            else
            {
                alive = false;
                destroyAllBody();
            }
        }
    }

    public GameObject getClosestPartFromTarget(Transform target)
    {
        List<GameObject> gs = new List<GameObject>();
        if (handLeft != null)  gs.Add(handLeft);
        if (handRight != null) gs.Add(handRight);
        if (footLeft != null && canWalk())  gs.Add(footLeft);
        if (footRight != null && canWalk()) gs.Add(footRight);

        float dist = float.MaxValue;
        GameObject best = null;
        foreach (GameObject g in gs)
        {
            float d = Vector3.Distance(g.transform.position, target.position);
            if (d < dist)
            {
                dist = d;
                best = g;
            }
        }
        return best;
    }

    public bool canWalk()
    {
        return rightLegAlive && leftLegAlive;
    }

    bool checkLegAlive(GameObject pelvis, GameObject upperLeg, GameObject lowerLeg, GameObject foot)
    {
        bool legAlive = true;
        if (upperBack == null)
        {
            destroyBodyPart(lowerBack);
            destroyBodyPart(pelvis);
            destroyBodyPart(upperLeg);
            destroyBodyPart(lowerLeg);
            destroyBodyPart(foot);
            legAlive = false;
        }
        else if (lowerBack == null)
        {
            destroyBodyPart(pelvis);
            destroyBodyPart(upperLeg);
            destroyBodyPart(lowerLeg);
            destroyBodyPart(foot);
            legAlive = false;
        }
        else if (pelvis == null)
        {
            destroyBodyPart(upperLeg);
            destroyBodyPart(lowerLeg);
            destroyBodyPart(foot);
            legAlive = false;
        }
        else if (upperLeg == null)
        {
            destroyBodyPart(lowerLeg);
            destroyBodyPart(foot);
            legAlive = false;
        }
        else if (lowerLeg == null)
        {
            destroyBodyPart(foot);
            legAlive = false;
        }
        else if (foot == null)
        {
            legAlive = false;
        }
        return legAlive;
    }

    bool checkArmAlive(GameObject neckSide, GameObject shoulder, GameObject upperArm, GameObject lowerArm, GameObject hand)
    {
        bool armAlive = true;
        if (neckSide == null)
        {
            destroyBodyPart(shoulder);
            destroyBodyPart(upperArm);
            destroyBodyPart(lowerArm);
            destroyBodyPart(hand);
            armAlive = false;
        }
        else if (shoulder == null)
        {
            destroyBodyPart(upperArm);
            destroyBodyPart(lowerArm);
            destroyBodyPart(hand);
            armAlive = false;
        }
        else if (upperArm == null)
        {
            destroyBodyPart(lowerArm);
            destroyBodyPart(hand);
            armAlive = false;
        }
        else if (lowerArm == null)
        {
            destroyBodyPart(hand);
            armAlive = false;
        }
        else if (hand == null)
        {
            armAlive = false;
        }
        return armAlive;
    }

    bool checkAlive()
    {
        return head != null && neck != null && (leftLegAlive || rightLegAlive || leftArmAlive || rightArmAlive);
    }

    void destroyAllBody()
    {
        destroyBodyPart(head);
        destroyBodyPart(neck);
        destroyBodyPart(neckRight);
        destroyBodyPart(shoulderRight);
        destroyBodyPart(upperArmRight);
        destroyBodyPart(lowerArmRight);
        destroyBodyPart(handRight);
        destroyBodyPart(neckLeft);
        destroyBodyPart(shoulderLeft);
        destroyBodyPart(upperArmLeft);
        destroyBodyPart(lowerArmLeft);
        destroyBodyPart(handLeft);
        destroyBodyPart(upperBack);
        destroyBodyPart(lowerBack);
        destroyBodyPart(pelvisLeft);
        destroyBodyPart(upperLegLeft);
        destroyBodyPart(lowerLegLeft);
        destroyBodyPart(footLeft);
        destroyBodyPart(pelvisRight);
        destroyBodyPart(upperLegRight);
        destroyBodyPart(lowerLegRight);
        destroyBodyPart(footRight);
    }

    void destroyBodyPart(GameObject part)
    {
        if (part != null && part.GetComponent<EntityDestroyer>() == null)
            part.AddComponent<EntityDestroyer>();
    }

    public bool Alive { get { return alive; }}
}
