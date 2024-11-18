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
        //if (!activeScreens.Contains(screen))
        //{
            Instantiate(screen);
           // activeScreens.Add(screen);
        //}
    }

    public void UnLoadScreen(GameObject screen)
    {
        //activeScreens.Remove(screen);
        Destroy(screen);
        
    }

    public void LoadPopUp(GameObject popUp)
    {
        //if (!activePopUp.Contains(popUp))
        //{
            Instantiate(popUp);
           // activePopUp.Add(popUp);
        //}
    }

    public void UnLoadPopUp(GameObject popUp)
    {
        PopUpSpawn popUpSpawn = popUp.GetComponentInChildren<PopUpSpawn>();

        popUpSpawn.DespawnPopUp();
        //activePopUp.Remove(activePopUp.Find(x => x.name == popUp.name));
        Destroy(popUp,popUpSpawn.dissapearTime);
    }

}
