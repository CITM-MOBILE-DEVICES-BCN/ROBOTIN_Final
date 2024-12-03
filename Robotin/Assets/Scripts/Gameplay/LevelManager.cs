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
    public int level = 1;
    public bool isHardMode = false;
    public enum LevelState
    {
        Playing,
        LevelPassed,
        Pause,
        GameOver
    }

    public void Init(int level)
    {
        if(level > GameManager.instance.maxLevelsPerLoop)
        {
            isHardMode = true;
        }
        else
        {
            isHardMode = false;
        }


        //Dependiendo de la dificultad inicializar flood con mas velocidad o menos 

        // Dependiendo del nivel desbloquea unas habilidades u otras
        player.GetComponent<PlayerController>().Init(level);


    }

    void Start()
    {
        timerManager = new TimerManager(30);
        
    }

    private void Update()
    {
        timerManager.UpdateTime();       
    }



}
