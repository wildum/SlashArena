using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum BodyPartType
{
    Critical,
    Armor,
    Sliceable,
    Weapon
}
public class BodyPart : MonoBehaviour
{
    BodyPartType m_type = BodyPartType.Sliceable;

    public Material armor, critical, weapon;

    public void setType(BodyPartType type)
    {
        m_type = type;
        switch (type)
        {
            case BodyPartType.Critical:
                GetComponent<Renderer>().material = critical;
                break;
            case BodyPartType.Armor:
                GetComponent<Renderer>().material = armor;
                if (GetComponent<Sliceable>() != null)
                    Destroy(GetComponent<Sliceable>());
                break;
            case BodyPartType.Weapon:
                GetComponent<Renderer>().material = weapon;
                gameObject.AddComponent<Weapon>();
                if (GetComponent<Sliceable>() != null)
                    Destroy(GetComponent<Sliceable>());
                break;
        }
        return;
    }
}
