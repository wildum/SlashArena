using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public MonsterFactory monsterFactory;
    public MenuController menuController;

    void Start()
    {
        //monsterFactory.createMonster(MonsterType.Default);
        menuController.createMusicList();
        menuController.displayMusicList();
    }
}
