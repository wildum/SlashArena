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

    private Vector3 poshead;
    private Vector3 posneck;
    private Vector3 posneckRight;
    private Vector3 posshoulderRight;
    private Vector3 posupperArmRight;
    private Vector3 poslowerArmRight;
    private Vector3 poshandRight;
    private Vector3 posneckLeft;
    private Vector3 posshoulderLeft;
    private Vector3 posupperArmLeft;
    private Vector3 poslowerArmLeft;
    private Vector3 poshandLeft;
    private Vector3 posupperBack;
    private Vector3 poslowerBack;
    private Vector3 pospelvisLeft;
    private Vector3 posupperLegLeft;
    private Vector3 poslowerLegLeft;
    private Vector3 posfootLeft;
    private Vector3 pospelvisRight;
    private Vector3 posupperLegRight;
    private Vector3 poslowerLegRight;
    private Vector3 posfootRight;

    private Quaternion rotahead;
    private Quaternion rotaneck;
    private Quaternion rotaneckRight;
    private Quaternion rotashoulderRight;
    private Quaternion rotaupperArmRight;
    private Quaternion rotalowerArmRight;
    private Quaternion rotahandRight;
    private Quaternion rotaneckLeft;
    private Quaternion rotashoulderLeft;
    private Quaternion rotaupperArmLeft;
    private Quaternion rotalowerArmLeft;
    private Quaternion rotahandLeft;
    private Quaternion rotaupperBack;
    private Quaternion rotalowerBack;
    private Quaternion rotapelvisLeft;
    private Quaternion rotaupperLegLeft;
    private Quaternion rotalowerLegLeft;
    private Quaternion rotafootLeft;
    private Quaternion rotapelvisRight;
    private Quaternion rotaupperLegRight;
    private Quaternion rotalowerLegRight;
    private Quaternion rotafootRight;

    private bool alive = true;

    private bool leftLegAlive = true;
    private bool rightLegAlive = true;
    private bool leftArmAlive = true;
    private bool rightArmAlive = true;

    private List<GameObject> m_criticals = new List<GameObject>();
    private int m_criticalInitSize = 0;
    private List<GameObject> m_armors = new List<GameObject>();
    private List<GameObject> m_weapons = new List<GameObject>();

    void Start()
    {
        poshead = head.transform.localPosition;
        posneck = neck.transform.localPosition;
        posneckRight = neckRight.transform.localPosition;
        posshoulderRight = shoulderRight.transform.localPosition;
        posupperArmRight = upperArmRight.transform.localPosition;
        poslowerArmRight = lowerArmRight.transform.localPosition;
        poshandRight = handRight.transform.localPosition;
        posneckLeft = neckLeft.transform.localPosition;
        posshoulderLeft = shoulderLeft.transform.localPosition;
        posupperArmLeft = upperArmLeft.transform.localPosition;
        poslowerArmLeft = lowerArmLeft.transform.localPosition;
        poshandLeft = handLeft.transform.localPosition;
        posupperBack = upperBack.transform.localPosition;
        poslowerBack = lowerBack.transform.localPosition;
        pospelvisLeft = pelvisLeft.transform.localPosition;
        posupperLegLeft = upperLegLeft.transform.localPosition;
        poslowerLegLeft = lowerLegLeft.transform.localPosition;
        posfootLeft = footLeft.transform.localPosition;
        pospelvisRight = pelvisRight.transform.localPosition;
        posupperLegRight = upperLegRight.transform.localPosition;
        poslowerLegRight = lowerLegRight.transform.localPosition;
        posfootRight = footRight.transform.localPosition;
        rotahead = head.transform.localRotation;
        rotaneck = neck.transform.localRotation;
        rotaneckRight = neckRight.transform.localRotation;
        rotashoulderRight = shoulderRight.transform.localRotation;
        rotaupperArmRight = upperArmRight.transform.localRotation;
        rotalowerArmRight = lowerArmRight.transform.localRotation;
        rotahandRight = handRight.transform.localRotation;
        rotaneckLeft = neckLeft.transform.localRotation;
        rotashoulderLeft = shoulderLeft.transform.localRotation;
        rotaupperArmLeft = upperArmLeft.transform.localRotation;
        rotalowerArmLeft = lowerArmLeft.transform.localRotation;
        rotahandLeft = handLeft.transform.localRotation;
        rotaupperBack = upperBack.transform.localRotation;
        rotalowerBack = lowerBack.transform.localRotation;
        rotapelvisLeft = pelvisLeft.transform.localRotation;
        rotaupperLegLeft = upperLegLeft.transform.localRotation;
        rotalowerLegLeft = lowerLegLeft.transform.localRotation;
        rotafootLeft = footLeft.transform.localRotation;
        rotapelvisRight = pelvisRight.transform.localRotation;
        rotaupperLegRight = upperLegRight.transform.localRotation;
        rotalowerLegRight = lowerLegRight.transform.localRotation;
        rotafootRight = footRight.transform.localRotation;
    }

    void FixedUpdate()
    {
        updateLocalTransform(head, poshead, rotahead);
        updateLocalTransform(neck, posneck, rotaneck);
        updateLocalTransform(neckLeft, posneckLeft, rotaneckLeft);
        updateLocalTransform(neckRight, posneckRight, rotaneckRight);
        updateLocalTransform(shoulderRight, posshoulderRight, rotashoulderRight);
        updateLocalTransform(shoulderLeft, posshoulderLeft, rotashoulderLeft);
        updateLocalTransform(upperArmLeft, posupperArmLeft, rotaupperArmLeft);
        updateLocalTransform(upperArmRight, posupperArmRight, rotaupperArmRight);
        updateLocalTransform(lowerArmLeft, poslowerArmLeft, rotalowerArmLeft);
        updateLocalTransform(lowerArmRight, poslowerArmRight, rotalowerArmRight);
        updateLocalTransform(handRight, poshandRight, rotahandRight);
        updateLocalTransform(handLeft, poshandLeft, rotahandLeft);
        updateLocalTransform(upperBack, posupperBack, rotaupperBack);
        updateLocalTransform(lowerBack, poslowerBack, rotalowerBack);
        updateLocalTransform(pelvisLeft, pospelvisLeft, rotapelvisLeft);
        updateLocalTransform(upperLegLeft, posupperLegLeft, rotaupperLegLeft);
        updateLocalTransform(lowerLegLeft, poslowerLegLeft, rotalowerLegLeft);
        updateLocalTransform(footLeft, posfootLeft, rotafootLeft);
        updateLocalTransform(pelvisRight, pospelvisRight, rotapelvisRight);
        updateLocalTransform(upperLegRight, posupperLegRight, rotaupperLegRight);
        updateLocalTransform(lowerLegRight, poslowerLegRight, rotalowerLegRight);
        updateLocalTransform(footRight, posfootRight, rotafootRight);
    }

    void updateLocalTransform(GameObject g, Vector3 pos, Quaternion rota)
    {
        if (g != null && g.GetComponent<EntityDestroyer>() == null)
        {
            g.transform.localPosition = pos;
            g.transform.localRotation = rota;
        }
    }

    public void setBodyPartsType(List<BodyPartName> criticals, List<BodyPartName> armors, List<BodyPartName> weapons)
    {
        addBodyParts(criticals, m_criticals, BodyPartType.Critical);
        addBodyParts(weapons, m_weapons, BodyPartType.Weapon);
        addBodyParts(armors, m_armors, BodyPartType.Armor);
        m_criticalInitSize = m_criticals.Count;
    }

    void addBodyParts(List<BodyPartName> names, List<GameObject> mlist, BodyPartType type)
    {
        foreach (var bodyPartName in names)
        {
            GameObject part = getBodyPartByEnum(bodyPartName);
            part.GetComponent<BodyPart>().setType(type);
            mlist.Add(part);
        }
    }

    GameObject getBodyPartByEnum(BodyPartName name)
    {
        switch (name)
        {
            case BodyPartName.head:
                return head;
            case BodyPartName.neck:
                return neck;
            case BodyPartName.neckRight:
                return neckRight;
            case BodyPartName.shoulderRight:
                return shoulderRight;
            case BodyPartName.upperArmRight:
                return upperArmRight;
            case BodyPartName.lowerArmRight:
                return lowerArmRight;
            case BodyPartName.handRight:
                return handRight;
            case BodyPartName.neckLeft:
                return neckLeft;
            case BodyPartName.shoulderLeft:
                return shoulderLeft;
            case BodyPartName.upperArmLeft:
                return upperArmLeft;
            case BodyPartName.lowerArmLeft:
                return lowerArmLeft;
            case BodyPartName.handLeft:
                return handLeft;
            case BodyPartName.upperBack:
                return upperBack;
            case BodyPartName.lowerBack:
                return lowerBack;
            case BodyPartName.pelvisLeft:
                return pelvisLeft;
            case BodyPartName.upperLegLeft:
                return upperLegLeft;
            case BodyPartName.lowerLegLeft:
                return lowerLegLeft;
            case BodyPartName.footLeft:
                return footLeft;
            case BodyPartName.pelvisRight:
                return pelvisRight;
            case BodyPartName.upperLegRight:
                return upperLegRight;
            case BodyPartName.lowerLegRight:
                return lowerLegRight;
            case BodyPartName.footRight:
                return footRight;
        }
        return null;
    }

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

    bool checkAlive()
    {
        return m_weapons.Count != 0 && m_criticals.Count == m_criticalInitSize && (leftLegAlive || rightLegAlive);
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
        m_armors.Remove(part);
        m_armors.RemoveAll(o => o == null);
        m_criticals.Remove(part);
        m_criticals.RemoveAll(o => o == null);
        m_weapons.Remove(part);
        m_weapons.RemoveAll(o => o == null);
        if (part != null && part.GetComponent<EntityDestroyer>() == null)
            part.AddComponent<EntityDestroyer>();
    }

    public bool Alive { get { return alive; }}
}

public enum BodyPartName
{
    head,
    neck,
    neckRight,
    shoulderRight,
    upperArmRight,
    lowerArmRight,
    handRight,
    neckLeft,
    shoulderLeft,
    upperArmLeft,
    lowerArmLeft,
    handLeft,
    upperBack,
    lowerBack,
    pelvisLeft,
    upperLegLeft,
    lowerLegLeft,
    footLeft,
    pelvisRight,
    upperLegRight,
    lowerLegRight,
    footRight
}
