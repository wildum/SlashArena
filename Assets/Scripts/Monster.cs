using System.Collections;
using System.Collections.Generic;
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
        if (head == null || body == null)
        {
            destroyAllElements();
        }
    }

    void destroyAllElements()
    {
        EntityDestroyer.DestroyEntity(head);
        EntityDestroyer.DestroyEntity(body);
        foreach (GameObject limb in limbs)
        {
            foreach (Transform child in limb.transform)
            {
                EntityDestroyer.DestroyEntity(child.gameObject);
            }
        }
        EntityDestroyer.DestroyEntity(gameObject, 6);
    }
}
