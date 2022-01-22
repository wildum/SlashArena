using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public enum ScrollDirection
{
    Up,
    Down
}

public class Scroller : MonoBehaviour
{
    public ScrollDirection direction;

    void OnCollisionEnter(Collision collisionInfo)
    {
        if (direction == ScrollDirection.Up)
        {
            Debug.Log("Scroll up");
            GameEvents.current.ScrollUp();
        }
        else
        {
            Debug.Log("Scroll down");
            GameEvents.current.ScrollDown();
        }
    }
}
