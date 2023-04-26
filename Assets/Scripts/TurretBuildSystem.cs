using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class TurretBuildSystem : MonoBehaviour
{
    public Transform[] buildPoints;
    public Dictionary<Transform, TurretHandler> builtTurrets = new();
    private Turret currentBuildTurret;
    private TurretShopItem currentSelectedTurret;

    public static TurretBuildSystem Instance;
    private bool removeMode;
    private SpriteRenderer lastTurret; 

    private void Awake()
    {
        Instance = this;
    }

    public void RemoveMode()
    {
        removeMode = !removeMode;
        if (removeMode)
        {
            SelectBuildTurret(null);
            removeMode = true;
        }
    }

    public void SelectBuildTurret(TurretShopItem turret)
    {
        removeMode = false;
        if (currentBuildTurret)
            Destroy(currentBuildTurret.gameObject);
        if (turret)
        {

            if (turret == currentSelectedTurret)
                SelectBuildTurret(null);
            else
                currentBuildTurret = Instantiate(turret.turretPrefab);
        }
        currentSelectedTurret = turret;
    }

    private void Update()
    {
        var mousePos = Camera.allCameras[0].ScreenToWorldPoint(Input.mousePosition);
        mousePos.z = 0;

        if (removeMode)
        {
            var t = builtTurrets.Select(a => a.Key).FirstOrDefault(a => Vector3.Distance(a.position, mousePos) < 1f);

            if (lastTurret)
            {
                lastTurret.color = Color.white;
                lastTurret = null;
            }

            if (t)
            {
                var img = builtTurrets[t].turret.GetComponentInChildren<SpriteRenderer>();
                img.color = Color.red;
                lastTurret = img;
                if (!EventSystem.current.currentSelectedGameObject && Input.GetMouseButtonUp(0))
                {
                    GameStats.money += builtTurrets[t].turretItem.turretPrice / 2f;
                    Destroy(builtTurrets[t].turret.gameObject);
                    builtTurrets.Remove(t);
                }
            }
        }
        else if (currentBuildTurret)
        {
            var buildPos = buildPoints.FirstOrDefault(a => Vector3.Distance(a.position, mousePos) < 1f);

            currentBuildTurret.transform.position = buildPos == null ? mousePos : buildPos.position;

            if (!EventSystem.current.currentSelectedGameObject && Input.GetMouseButtonUp(0))
            {
                if (buildPos&&!builtTurrets.ContainsKey(buildPos))
                {
                    if (GameStats.CanAfford(currentSelectedTurret.turretPrice))
                    {
                        GameStats.money -= currentSelectedTurret.turretPrice;
                        builtTurrets.Add(buildPos, new TurretHandler { turret = currentBuildTurret, turretItem = currentSelectedTurret });
                        currentBuildTurret.Init();
                        currentSelectedTurret = null;
                        currentBuildTurret = null;
                    }
                }
            }
        }
    }

    private void OnDrawGizmos()
    {
        for (int i = 0; i < buildPoints.Length; i++)
        {
            Gizmos.DrawWireSphere(buildPoints[i].position, 0.5f);
        }
    }
}
public class TurretHandler
{
    public Turret turret;
    public TurretShopItem turretItem;
}