using System.Collections;
using System.Collections.Generic;
using UnityEngine;

static class EntityDestroyer
{
    static Material deadMaterial;

    public static void DestroyEntity(GameObject gameObject, float time = 5)
    {
        if (gameObject == null)
            return;
        if (deadMaterial == null)
        {
           deadMaterial = Resources.Load("Materials/Dead", typeof(Material)) as Material;
        }
        if (gameObject.GetComponent<Renderer>() != null)
            gameObject.GetComponent<Renderer>().material = deadMaterial;
        UnityEngine.Object.Destroy(gameObject, time); // assign fadeout script
    }
}
