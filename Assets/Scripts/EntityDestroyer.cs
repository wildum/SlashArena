using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EntityDestroyer : MonoBehaviour
{
    static Material deadMaterial;

    private Renderer rend;

    void Start()
    {
        if (deadMaterial == null)
           deadMaterial = Resources.Load("Materials/Dead", typeof(Material)) as Material;

        rend = gameObject.GetComponent<Renderer>();
        if (rend != null)
        {
            rend.material = deadMaterial;
            StartCoroutine("fadeOut");
        }
        else
        {
            Destroy(gameObject);
        }

        Rigidbody rg = gameObject.GetComponent<Rigidbody>();
        if (rg != null)
        {
            rg.useGravity = true;
            rg.isKinematic = false;
        }

        CharacterJoint[] characterJoints = gameObject.GetComponents<CharacterJoint>();
        foreach (CharacterJoint c in characterJoints)
        {
            Destroy(c);
        }
    }

    IEnumerator fadeOut()
    {
        for (float f = 1f; f >= 0.3f; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.4f);
        }
        Destroy(gameObject);
    }
}
