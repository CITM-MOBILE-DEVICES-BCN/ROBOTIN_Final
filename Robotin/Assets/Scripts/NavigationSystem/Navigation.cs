using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace NavigationSystem 
{ 
    public class Navigation
    {
        public void LoadScene(string scene)
        {
            SceneManager.LoadScene(scene);
        }
    
        public void LoadScreen(GameObject screen)
        {
            GameObject.Instantiate(screen);
        }
    
        public void UnLoadScreen(GameObject screen)
        {
            GameObject.Destroy(screen);   
        }

        //A pop up needs to have a PopUpSpawn script attached to it (not to the canvas)
        //so it has the spawn/despawn animation
        public GameObject LoadPopUp(GameObject popUp)
        {
            var menu = GameObject.Instantiate(popUp);
            return menu;
        }
    
        public void UnLoadPopUp(GameObject popUp)
        {
            PopUpSpawn popUpSpawn = popUp.GetComponentInChildren<PopUpSpawn>();
    
            popUpSpawn.DespawnPopUp();
    
            GameObject.Destroy(popUp,popUpSpawn.dissapearTime);
        }
    }
}