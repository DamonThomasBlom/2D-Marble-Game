using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using UnityEngine;

public class NewBallCounter : MonoBehaviour
{
    public TextMeshProUGUI TimerTxt;
    public float Timer = 30f;

    private List<PlinkoMiniGameController> _miniGames;
    private float _initialTime;

    private void Awake()
    {
        _miniGames = FindObjectsOfType<PlinkoMiniGameController>().ToList();
        _initialTime = Timer;
    }

    private void Update()
    {
        Timer -= Time.deltaTime;
        string timeS = Timer.ToString("F0");

        if (TimerTxt.text != timeS) 
            TimerTxt.text = timeS;

        if (Timer <= 0)
        {
            Timer = _initialTime;
            foreach (var m in _miniGames) { m.AddBall(); }
        }
    }
}
