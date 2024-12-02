using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using NavigationSystem;
public class GameManager : MonoBehaviour
{
    public static GameManager instance;

    Navigation navigation;

    public List<GameObject> screenList;

    public List<GameObject> popUpList;

    public List<GameObject> activeScreens;

    public List<GameObject> activePopUps;

    private GameObject currentPopUp;

    //TODO: implement a list of level managers (levels) and instantiate one depending on the data (current level)
    [SerializeField]
    private List<LevelManager> allLevels;

    public LevelManager currentLevel;

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

        if (activeScreens != null)
        {
            activeScreens.Clear();
        }

        if (activePopUps != null)
        {
            activePopUps.Clear();
        }       
    }

    public void LoadScreen(string screenName)
    {
        var screen = screenList.Find(x => x.name == screenName);
        if (screen != null)
        {
            activeScreens.Add(navigation.LoadScreen(screen));
        }
    }
    public void UnLoadScreen(string screenName)
    {
        var screen = activeScreens.Find(x => x.name.Equals(screenName));
    
        if (screen != null) 
        { 
            navigation.UnLoadScreen(screen);
            
            activeScreens.Remove(screen);
        }
    }

    public void LoadPopUp(string popUpName)
    {
        var popUp = popUpList.Find(x => x.name == popUpName);

        if (currentPopUp == null && popUp != null)
        {
            currentPopUp = navigation.LoadPopUp(popUp);
            Debug.Log(popUpName);
            activePopUps.Add(currentPopUp);
        }
    }

    public void UnLoadPopUp(string popUpName)
    {
        var popUp = activePopUps.Find(x => x.name.Equals(popUpName));
        
        if (popUp != null)
        {
            navigation.UnLoadPopUp(popUp);
            currentPopUp = null;
            activePopUps.Remove(popUp);
        }
    }

    public void LoadSceneAndLevel(string sceneName)
    {
        StartCoroutine(LoadSceneAndLevelCoroutine(sceneName));
    }

    private IEnumerator LoadSceneAndLevelCoroutine(string sceneName)
    {
        UnityEngine.SceneManagement.SceneManager.LoadScene(sceneName);

        yield return new WaitUntil(() => UnityEngine.SceneManagement.SceneManager.GetActiveScene().name == sceneName);

        LoadLevel();
    }

    //TODO: Implement with save and load to load the level that is required based on the level data (current level), for the moment it will just load the first level
    public void LoadLevel()
    {
        var level = Instantiate(allLevels[0]);
        currentLevel = level;
    }
}
