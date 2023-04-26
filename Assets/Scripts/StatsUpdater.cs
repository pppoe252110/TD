using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class StatsUpdater : MonoBehaviour
{
    public Text moneyText;
    public Text waveText;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        moneyText.text = "Монеты: " + GameStats.money + "$";
        waveText.text = "Волна: " + GameStats.currentWave;
    }
}
