using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public static class GameStats
{
    public static float money = 1000f;

    public static int health = 3;
    public static int currentWave = 1;

    public static bool CanAfford(float price)
    {
        if(price<=money)
            return true;
        return false;
    }

    public static void AddMoney(float multiplier = 1)
    {
        money += currentWave * multiplier * 100f;
    }
}
