using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class Monster : MonoBehaviour
{
    public GameObject head;
    public GameObject body;
    public GameObject neck;
    public GameObject[] arms;
    public GameObject[] legs;

    private bool startVanishing = false;

    private List<GameObject> limbs = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        foreach(GameObject g in arms)
            limbs.Add(g);
        foreach(GameObject g in legs)
            limbs.Add(g);
    }

    void Update()
    {
        if (isDead() && !startVanishing)
        {
            destroyAllElements();
            startVanishing = true;
        }
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
        foreach (GameObject limb in limbs)
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
