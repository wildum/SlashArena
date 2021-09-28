using UnityEngine;
using System.Collections;

public class Ligament : MonoBehaviour {

    public Transform start;
    public Transform end;
    
    public float factor = 0.1f;

    bool alive = true;
    
    void Start()
    {
        SetPos(start.position, end.position);
    }
    
    void Update ()
    {
        if (start != null && end != null)
        {
            SetPos(start.position, end.position);
        }
        else if (alive)
        {
            alive = false;
            Destroy(gameObject);
        }   
    }
    
    void SetPos(Vector3 start, Vector3 end)
    {
        var dir = end - start;
        var mid = (dir) / 2.0f + start;
        transform.position = mid;
        transform.rotation = Quaternion.FromToRotation(Vector3.up, dir);
        Vector3 scale = transform.localScale;
        scale.y = dir.magnitude * factor;
        transform.localScale = scale;
    }
}