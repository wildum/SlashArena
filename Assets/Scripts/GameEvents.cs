using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;

public class GameEvents : MonoBehaviour
{
    public static GameEvents current;

    private void Awake()
    {
        current = this;
    }

    public event UnityAction onSelectableSelected;
    public void SelectableSelected()
    {
        if (onSelectableSelected != null)
        {
            onSelectableSelected();
        }
    }

    public event UnityAction onScrollUp;
    public void ScrollUp()
    {
        if (onScrollUp != null)
        {
            onScrollUp();
        }
    }

    public event UnityAction onScrollDown;
    public void ScrollDown()
    {
        if (onScrollDown != null)
        {
            onScrollDown();
        }
    }
}
