using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
public class Navigation : MonoBehaviour
{
    
    public static Navigation instance;

    public List<GameObject> activeScreens;

    public List<GameObject> activePopUp;


    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }

      
    }

    public void LoadScene(string scene)
    {
        SceneManager.LoadScene(scene);
    }

    public void LoadScreen(GameObject screen)
    {

            Instantiate(screen);

    }

    public void UnLoadScreen(GameObject screen)
    {

        Destroy(screen);
        
    }

    public GameObject LoadPopUp(GameObject popUp)
    {

        var menu = Instantiate(popUp);
        return menu;

    }

    public void UnLoadPopUp(GameObject popUp)
    {
        PopUpSpawn popUpSpawn = popUp.GetComponentInChildren<PopUpSpawn>();

        popUpSpawn.DespawnPopUp();

        Destroy(popUp,popUpSpawn.dissapearTime);
    }

}
