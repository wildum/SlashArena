using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public MenuController menuController;

    void Start()
    {
        menuController.createLevelList();
        menuController.createMusicList();
        menuController.displayLevelsList();
    }
}
