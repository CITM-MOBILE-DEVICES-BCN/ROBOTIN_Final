using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TimerModule;
using System;
using TimerSampleScene;

public class LevelManager : MonoBehaviour
{

    public GameObject player;
    public TimerManager timerManager;
    public int levelOnWorld;
    public float timeToCompleteLevel;
    [SerializeField] private int level = 1;

    public enum LevelState
    {
        Playing,
        LevelPassed,
        Pause,
        GameOver
    }

    public void Init(int level)
    {
        levelOnWorld = level;




        //Dependiendo de la dificultad inicializar flood con mas velocidad o menos 

        // Dependiendo del nivel desbloquea unas habilidades u otras
        player.GetComponent<PlayerController>().Init(level);


    }

    void Start()
    {
        timeToCompleteLevel = (int)CalculateMaxTime(level);
        timerManager = new TimerManager(timeToCompleteLevel);
    }


    private void Update()
    {
        timerManager.UpdateTime();       
    }

    public float CalculateMaxTime(int dificulty)
    {
        //Base num 30
        return 30 - ((dificulty / GameManager.instance.maxLevelsPerLoop) * 5);
    }


}




