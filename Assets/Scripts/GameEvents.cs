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

    public event UnityAction onMonsterDead;
    public void MonsterDead()
    {
        if (onMonsterDead != null)
        {
            onMonsterDead();
        }
    }

    public event UnityAction<string> onSelectableSelected;
    public void SelectableSelected(string text)
    {
        if (onSelectableSelected != null)
        {
            onSelectableSelected(text);
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
