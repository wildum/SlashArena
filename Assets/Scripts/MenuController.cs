using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public SelectableList musicList;

    public void createMusicList()
    {
        musicList.addSelectable("This is a test");
    }

    public void displayMusicList()
    {
        musicList.displayList();
    }
}
