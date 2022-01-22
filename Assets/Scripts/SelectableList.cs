using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SelectableList : MonoBehaviour
{
    public GameObject selectable;

    private List<Selectable> selectables = new List<Selectable>();

    private Selectable current;

    private Vector3 currentPos = new Vector3(0, 4, 1.5f);
    private Vector3 nextPos = new Vector3(0, 4.5f, 1.5f);
    private Vector3 previousPos = new Vector3(0, 3.5f, 1.5f);

    private bool switching = false;
    private bool fadingOut = false;
    private bool fadingIn = false;

    private Selectable fadingOutTarget;
    private Selectable fadingInTarget;

    private const float SPEED_SWITCH = 1.0f;
    private const float SWITCH_PRECISION = 1.0f;
    private const float SPEED_FADE = 1.0f;

    private void Start()
    {
        GameEvents.current.onSelectableSelected += onSelectableSelected;
        GameEvents.current.onScrollDown += onScrollUp;
        GameEvents.current.onScrollUp += onScrollDown;
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

        if (fadingOut && fadingOutTarget != null)
        {
            Color c = fadingOutTarget.gameObject.GetComponent<Renderer>().material.color;
            c.a = Mathf.Lerp(c.a, 0, SPEED_FADE * Time.deltaTime);
            fadingOutTarget.gameObject.GetComponent<Renderer>().material.color = c;

            if (c.a == 0)
            {
                fadingOutTarget.gameObject.SetActive(false);
                fadingOut = false;
            }
        }

        if (fadingIn && fadingInTarget != null)
        {
            Color c = fadingInTarget.gameObject.GetComponent<Renderer>().material.color;
            c.a = Mathf.Lerp(c.a, 0, SPEED_FADE * Time.deltaTime);
            fadingInTarget.gameObject.GetComponent<Renderer>().material.color = c;

            if (c.a == 1.0f)
            {
                fadingInTarget.gameObject.SetActive(false);
                fadingIn = false;
            }
        }
    }

    public void displayList()
    {
        if (selectables.Count == 0)
            return;
        if (current == null)
            current = selectables[0];
        gameObject.SetActive(true);
        displaySelectables();
    }

    public void removeList()
    {
        gameObject.SetActive(false);
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
        if (current.Next != null)
        {
            current = current.Next;
            scroll(ScrollDirection.Up);
        }
    }

    public void onScrollDown()
    {
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
            setAlpha(current, 1.0f);
            if (current.Next != null)
            {
                current.Next.gameObject.SetActive(true);
                current.Next.gameObject.transform.position = nextPos;
                setAlpha(current.Next, 0.0f);
                fadingInTarget = current.Next;
                if (current.Next.Next != null)
                {
                    current.Next.Next.gameObject.SetActive(false);
                }
            }
            if (current.Previous != null)
            {
                current.Previous.gameObject.SetActive(true);
                current.Previous.gameObject.transform.position = currentPos;
                setAlpha(current.Previous, 1.0f);
                if (current.Previous.Previous != null)
                {
                    current.Previous.Previous.gameObject.SetActive(true);
                    current.Previous.Previous.gameObject.transform.position = previousPos;
                    setAlpha(current.Previous.Previous, 1.0f);
                    fadingOutTarget = current.Previous.Previous;
                }
            }
        }
        else
        {
            current.gameObject.transform.position = previousPos;
            setAlpha(current, 1.0f);
            if (current.Previous != null)
            {
                current.Previous.gameObject.SetActive(true);
                current.Previous.gameObject.transform.position = previousPos;
                setAlpha(current.Previous, 0.0f);
                fadingInTarget = current.Previous;
                if (current.Previous.Previous != null)
                {
                    current.Previous.Previous.gameObject.SetActive(false);
                }
            }
            if (current.Next != null)
            {
                current.Next.gameObject.SetActive(true);
                current.Next.gameObject.transform.position = currentPos;
                setAlpha(current.Next, 1.0f);
                if (current.Next.Next != null)
                {
                    current.Next.Next.gameObject.SetActive(true);
                    current.Next.Next.gameObject.transform.position = nextPos;
                    setAlpha(current.Next.Next, 1.0f);
                    fadingOutTarget = current.Next.Next;
                }
            }
        }
        switching = true;
        fadingIn = true;
        fadingOut = true;
    }

    private void setAlpha(Selectable s, float v)
    {
        Color c = s.gameObject.GetComponent<Renderer>().material.color;
        c.a = v;
        s.gameObject.GetComponent<Renderer>().material.color = c;
    }

    private void onSelectableSelected()
    {
        Debug.Log("selectable triggered");
    }

}
