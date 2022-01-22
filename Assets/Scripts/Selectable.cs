using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Selectable : MonoBehaviour
{
    public Text text;
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
    public string Text { get {return text.text;} set {text.text = value;}}
}
