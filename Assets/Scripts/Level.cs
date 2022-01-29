using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public struct Level
{
    public List<MonsterType> types;
    public Level(List<MonsterType> itypes)
    {
        types = itypes;
    }
}

public static class Levels
{
    public static Level level1 = new Level(new List<MonsterType>(){MonsterType.Default});
}