using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "Turret", menuName = "ScriptableObjects/TurretShopItem", order = 1)]
public class TurretShopItem : ScriptableObject
{
    public string turretName = "Турель";
    public float turretPrice = 1000;
    public Turret turretPrefab;
}
