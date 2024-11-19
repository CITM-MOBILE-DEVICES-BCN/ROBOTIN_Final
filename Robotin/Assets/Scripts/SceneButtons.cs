using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class SceneButtons : MonoBehaviour 
{

    private GameObject currentPopUp;

    public void LoadScene(string scene)
    {
        Navigation.instance.LoadScene(scene);
    }

    public void LoadScreen(GameObject screen)
    {
        Navigation.instance.LoadScreen(screen);
    }

    public void UnLoadScreen(GameObject screen)
    {
        Navigation.instance.UnLoadScreen(screen);
    }

    public void LoadPopUp(GameObject popUp)
    {
        if (!currentPopUp)
        {
            currentPopUp = Navigation.instance.LoadPopUp(popUp);

        }
    }

    public void UnLoadPopUp(GameObject popUp)
    {

         Navigation.instance.UnLoadPopUp(popUp);

        
    }

}
