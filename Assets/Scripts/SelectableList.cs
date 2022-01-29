using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableList : MonoBehaviour
{
    public GameObject selectable;

    private List<Selectable> selectables = new List<Selectable>();

    private Selectable current;

    private Vector3 currentPos = new Vector3(0, 4, 1.5f);
    private Vector3 nextPos = new Vector3(0, 5, 1.8f);
    private Vector3 previousPos = new Vector3(0, 3, 1.8f);

    private bool switching = false;

    private const float SPEED_SWITCH = 3.0f;
    private const float SWITCH_PRECISION = 0.05f;
    private const float SPEED_FADE = 1.0f;

    private void Start()
    {
        GameEvents.current.onScrollDown += onScrollDown;
        GameEvents.current.onScrollUp += onScrollUp;
    }

    private void Update()
    {
        if (switching)
        {
            current.gameObject.transform.position = Vector3.Lerp(current.gameObject.transform.position, currentPos, SPEED_SWITCH * Time.deltaTime);
            if (current.Previous != null)
                current.Previous.gameObject.transform.position = Vector3.Lerp(current.Previous.gameObject.transform.position, previousPos, SPEED_SWITCH * Time.deltaTime);
            if (current.Next != null)
                current.Next.gameObject.transform.position = Vector3.Lerp(current.Next.gameObject.transform.position, nextPos, SPEED_SWITCH * Time.deltaTime);
            if ((current.gameObject.transform.position - currentPos).magnitude < SWITCH_PRECISION
                && (current.Next == null || (current.Next.gameObject.transform.position - nextPos).magnitude < SWITCH_PRECISION)
                && (current.Previous == null || (current.Previous.gameObject.transform.position - previousPos).magnitude < SWITCH_PRECISION))
            {
                switching = false;
            }
        }
    }

    public void cyclicLink()
    {
        if (selectables.Count > 2)
        {
            selectables[0].Previous = selectables[selectables.Count - 1];
            selectables[selectables.Count - 1].Next = selectables[0];
        }
    }

    public void displayList()
    {
        if (selectables.Count == 0)
            return;
        if (current == null)
            current = selectables[0];
        cyclicLink();
        gameObject.SetActive(true);
        displaySelectables();
    }

    public void removeList()
    {
        foreach(Selectable selectable in selectables)
            Destroy(selectable.gameObject);
        gameObject.SetActive(false);
        selectables.Clear();
    }

    private void displaySelectables()
    {
        current.gameObject.SetActive(true);
        if (current.Next != null)
            current.Next.gameObject.SetActive(true);
        if (current.Previous != null)
            current.Previous.gameObject.SetActive(true);
        positionSelectables();
    }

    private void positionSelectables()
    {
        current.gameObject.transform.position = currentPos;
        if (current.Previous != null)
            current.Previous.gameObject.transform.position = previousPos;
        if (current.Next != null)
            current.Next.gameObject.transform.position = nextPos;
    }

    public void addSelectable(string text)
    {
        GameObject g = Instantiate(selectable, nextPos, Quaternion.identity);
        Selectable s = g.GetComponent<Selectable>();
        s.Text = text;
        if (selectables.Count == 0)
            current = s;
        selectables.Add(s);
        if (selectables.Count > 1)
        {
            s.Previous = selectables[selectables.Count - 2];
            selectables[selectables.Count - 2].Next = s;
        }
        g.SetActive(false);
    }

    public void onScrollUp()
    {
        Debug.Log("UP");
        if (current.Next != null)
        {
            current = current.Next;
            scroll(ScrollDirection.Up);
        }
    }

    public void onScrollDown()
    {
        Debug.Log("DOWN");
        if (current.Previous != null)
        {
            current = current.Previous;
            scroll(ScrollDirection.Down);
        }
    }

    private void scroll(ScrollDirection direction)
    {
        current.gameObject.SetActive(true);
        if (direction == ScrollDirection.Up)
        {
            current.gameObject.transform.position = nextPos;
            if (current.Next != null)
            {
                current.Next.gameObject.SetActive(true);
                current.Next.gameObject.transform.position = nextPos;
                if (current.Next.Next != null)
                {
                    current.Next.Next.gameObject.SetActive(false);
                }
            }
            if (current.Previous != null)
            {
                current.Previous.gameObject.SetActive(true);
                current.Previous.gameObject.transform.position = currentPos;
                if (current.Previous.Previous != null)
                {
                    current.Previous.Previous.gameObject.SetActive(false);
                    current.Previous.Previous.gameObject.transform.position = previousPos;
                }
            }
        }
        else
        {
            current.gameObject.transform.position = previousPos;
            if (current.Previous != null)
            {
                current.Previous.gameObject.SetActive(true);
                current.Previous.gameObject.transform.position = previousPos;
                if (current.Previous.Previous != null)
                {
                    current.Previous.Previous.gameObject.SetActive(false);
                }
            }
            if (current.Next != null)
            {
                current.Next.gameObject.SetActive(true);
                current.Next.gameObject.transform.position = currentPos;
                if (current.Next.Next != null)
                {
                    current.Next.Next.gameObject.SetActive(false);
                    current.Next.Next.gameObject.transform.position = nextPos;
                }
            }
        }
        switching = true;
    }

}
