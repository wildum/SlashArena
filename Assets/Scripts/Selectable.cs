using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Selectable : MonoBehaviour
{
    public GameObject text;
    private Selectable previous;
    private Selectable next;

    private void OnDestroy()
    {
        Destroy(text);
    }

    public void slice()
    {
        GameEvents.current.SelectableSelected();
    }

    public Selectable Previous { get {return previous;} set {previous = value;}}
    public Selectable Next { get {return next;} set {next = value;}}
}
