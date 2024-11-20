using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class NavigationSceneButtons : MonoBehaviour 
{
    public void LoadScene(string scene)
    {
        GameManager.instance.LoadScene(scene);
    }

    public void LoadScreen(GameObject screen)
    {
        GameManager.instance.LoadScreen(screen);
    }

    public void UnLoadScreen(GameObject screen)
    {
        GameManager.instance.UnLoadScreen(screen);
    }

    public void LoadPopUp(GameObject popUp)
    {
        GameManager.instance.LoadPopUp(popUp);
    }

    public void UnLoadPopUp(GameObject popUp)
    {
        GameManager.instance.UnLoadPopUp(popUp);
    }
}
