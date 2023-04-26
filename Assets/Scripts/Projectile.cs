using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using static UnityEngine.ParticleSystem;

public class Projectile : MonoBehaviour
{
    public float speed;
    public float damage = 10;
    public Unit targetUnit;
    public ParticleSystem particles;
    private float t = 0;
    private Vector3 startPos;

    private void Start()
    {
        startPos = transform.position;
    }

    private void Update()
    {
        t += Time.deltaTime;
        if (!targetUnit)
        {
            if (particles.isPlaying)
                particles.Stop();
            if (particles.particleCount <= 0)
                Destroy(gameObject);
        }
        else
        {
            transform.position = Vector3.Lerp(startPos, targetUnit.transform.position, t);

            if (t >= 1)
            {
                targetUnit.Hit(damage);
                targetUnit = null;
            }
        }
    }
}
