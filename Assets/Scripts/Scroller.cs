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

    void OnTriggerEnter(Collider other)
    {
        if (direction == ScrollDirection.Up)
        {
            GameEvents.current.ScrollUp();
        }
        else
        {
            GameEvents.current.ScrollDown();
        }
    }
}
