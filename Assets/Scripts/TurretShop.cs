using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class TurretShop : MonoBehaviour
{
    public GameObject turretShopItemPrefab;
    public TurretShopItem[] turretShopItems;
    public Transform shopContent;
    private GameObject[] turretItems;
    public static TurretShop Instance;

    private void Awake()
    {
        Instance = this;
    }

    private void Start()
    {
        for (int i = 0; i < turretShopItems.Length; i++)
        {
            var b = Instantiate(turretShopItemPrefab, shopContent);
            var text = b.GetComponentInChildren<Text>();
            text.text= turretShopItems[i].turretName;
            text.text += "\n" + turretShopItems[i].turretPrice + "$";
            var button = b.GetComponentInChildren<Button>();
            var index = i;
            button.onClick.AddListener(() => SelectItem(index));
        }
    }
    public void SelectItem(int index)
    {
        TurretBuildSystem.Instance.SelectBuildTurret(turretShopItems[index]);
    }
}
