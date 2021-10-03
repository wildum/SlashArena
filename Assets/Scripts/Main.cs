using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Main : MonoBehaviour
{
    public MonsterFactory monsterFactory;

    void Start()
    {
        monsterFactory.createMonster(MonsterType.Default);
    }
}
