using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;
    public string currentLevel;


    private void Awake()
    {
        // Singleton. The goal is to have only one manager in the game and prevent ever come another one. 
        // We will make sure there can be only one instance of manager in the game. Not more. 
        if (manager == null)
        {
            DontDestroyOnLoad(gameObject); // Do not destroy this gameobject when you change the scene. 
            manager = this; // We tell that this object will be the manager. 
        }
        else
        {
            // If for some reason the game tries to do an another game manager. We will destory it!!!
            Destroy(gameObject);
        }
    }


    // Update is called once per frame
    void Update()
    {
        // If we ever press M button on keyboard, the game will open MainMenu scene. 
        if (Input.GetKeyDown(KeyCode.M))
        {
            SceneManager.LoadScene("MainMenu");
        }
    }

}
