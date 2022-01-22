using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MenuController : MonoBehaviour
{
    public SelectableList musicList;

    public void createMusicList()
    {
        musicList.addSelectable("First");
        musicList.addSelectable("Second");
        musicList.addSelectable("Third");
        musicList.addSelectable("Fourth");
        musicList.addSelectable("Fifth");
    }

    public void displayMusicList()
    {
        musicList.displayList();
    }
}
