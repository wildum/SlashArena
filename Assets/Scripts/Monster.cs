using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject head;
    public GameObject body;
    public GameObject[] limbs;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    void Update()
    {
        if (isDead())
        {
            destroyAllElements();
        }
    }

    bool isDead()
    {
        return head == null || body == null || !(Array.FindIndex(limbs, x => x != null && x.transform.Find("Handle") != null) > -1);
    }

    void destroyAllElements()
    {
        if (head != null)
            head.AddComponent<EntityDestroyer>();
        if (body != null)
            body.AddComponent<EntityDestroyer>();
        foreach (GameObject limb in limbs)
        {
            foreach (Transform child in limb.transform)
            {
                if (child.gameObject != null)
                    child.gameObject.AddComponent<EntityDestroyer>();
            }
        }
        Destroy(gameObject, 6);
    }
}
