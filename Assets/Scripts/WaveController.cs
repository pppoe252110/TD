using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class WaveController : MonoBehaviour
{
    public Unit[] unitsPrefabs;
    public float waveDelay = 15f;
    public float spawnDelay = 1f;
    public int waveSkip = 5;
    public AnimationCurve waveCurve;
    public Text waveTimer;
    private int toSpawn = 0;
    private float timer = 0f;

    void Update()
    {
        if(toSpawn<=0)
        {
            timer += Time.deltaTime;

            if (timer > waveDelay)
            {
                timer = 0;
                StartWave();
            }
        }

        if (timer > 0)
        {
            waveTimer.text = "Волна через: " + (int)(waveDelay-timer);
        }
        else
        {
            waveTimer.text = "Осталось крипов: " + toSpawn;
        }
    }

    public void StartWave()
    {
        StartCoroutine(WaveCoroutine());
    }

    IEnumerator WaveCoroutine()
    {
        var max = Mathf.Clamp(GameStats.currentWave, 1, 100);
        toSpawn = max;
        while (toSpawn > 0) 
        {
            Spawn(toSpawn, max);
            toSpawn--;
            yield return new WaitForSeconds(spawnDelay);
        }
        GameStats.currentWave++;
    }

    public void Spawn(int id, int max)
    {
        int skip = (unitsPrefabs.Length + waveSkip) - Mathf.Clamp(GameStats.currentWave, 0, unitsPrefabs.Length + waveSkip);
        int index = (int)Mathf.Clamp(waveCurve.Evaluate(((float)id) / ((float)max)) * (unitsPrefabs.Length - 1) - skip, 0, unitsPrefabs.Length - 1);

        var creep = Instantiate(unitsPrefabs[index]);
        creep.health *= GameStats.currentWave;
        creep.Init();
    }
}
