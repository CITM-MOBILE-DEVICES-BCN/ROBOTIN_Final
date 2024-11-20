using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavigationSystem;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    Navigation navigation;
    private GameObject currentPopUp;

    void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(this.gameObject);
            navigation = new Navigation();
        }
        else
        {
            Destroy(this.gameObject);
        }       
    }

    public void LoadScene(string scene)
    {
        navigation.LoadScene(scene);
    }

    public void LoadScreen(GameObject screen)
    {
        navigation.LoadScreen(screen);
    }

    public void UnLoadScreen(GameObject screen)
    {
        navigation.UnLoadScreen(screen);
    }

    public void LoadPopUp(GameObject popUp)
    {
        if (!currentPopUp)
        {
            currentPopUp = navigation.LoadPopUp(popUp);
        }
    }

    public void UnLoadPopUp(GameObject popUp)
    {

        navigation.UnLoadPopUp(popUp);


    }
}
