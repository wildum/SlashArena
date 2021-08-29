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
        if (gameObject.GetComponent<Renderer>() != null)
        {
            rend = gameObject.GetComponent<Renderer>();
            rend.material = deadMaterial;
            StartCoroutine("fadeOut");
        }
        else
        {
            Destroy(gameObject);
        }
    }

    IEnumerator fadeOut()
    {
        for (float f = 1f; f >= -0.05f; f -= 0.05f)
        {
            Color c = rend.material.color;
            c.a = f;
            rend.material.color = c;
            yield return new WaitForSeconds(0.1f);
        }
        Destroy(gameObject);
    }
}
