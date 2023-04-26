using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class Turret : MonoBehaviour
{
    public float attackDistance = 3f;
    public float shootDelay = 0.25f;
    public float damage = 10;
    public Projectile projectilePrefab;

    private bool isActive = false;

    private float delay = 0;
    public void Init()
    {
        isActive = true;
    }

    private void Update()
    {
        delay += Time.deltaTime;

        if (!isActive) 
            return;

        var units = GetUnits();

        if (units.Length <= 0)
            return;
        var target = units.OrderByDescending(a => a.GetPath()).FirstOrDefault();

        if (delay >= shootDelay)
        {
            Shoot(target);
            delay = 0;
        }
    }

    public void Shoot(Unit unit)
    {
        var p = Instantiate(projectilePrefab, transform.position, Quaternion.identity);
        p.targetUnit = unit;
        p.damage = damage;
    }

    public Unit[] GetUnits()
    {
        return UnitsPath.Instance.units.Where(a => Vector3.Distance(transform.position, a.transform.position) < attackDistance).ToArray();
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.green;
        Gizmos.DrawWireSphere(transform.position, attackDistance);
    }
}
