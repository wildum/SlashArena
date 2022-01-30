using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public MenuController menuController;

    void Start()
    {
        menuController.initLists();
        menuController.createSelectables();
        menuController.displayLevelsList();
    }
}
