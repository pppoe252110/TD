using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Unit : MonoBehaviour
{
    public float health = 100f;
    public float speed = 1f;
    private float t = 0f;
    public float multiplier = 1;
    public Slider hpSlider;

    public void Init()
    {
        hpSlider.maxValue = health;
        UnitsPath.Instance.units.Add(this);
        Move();
    }

    private void OnDestroy()
    {
        UnitsPath.Instance.units.Remove(this);
    }

    public float GetPath()
    {
        return t;
    }

    void Update()
    {
        Move();
        t += Time.deltaTime * (1f / speed);
        hpSlider.value = health;
    }

    public void Move()
    {
        transform.position = UnitsPath.Instance.GetPoint(t);
    }

    public void Hit(float damage = 1)
    {
        health -= damage;

        if (health <= 0)
        {
            GameStats.AddMoney(1);
            Destroy(gameObject);
        }
    }
}
