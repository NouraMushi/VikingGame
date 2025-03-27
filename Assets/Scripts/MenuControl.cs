using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void LoadMap()
    {
        SceneManager.LoadScene("Map");
    }
    public void SaveGame()
    {
        GameManager.manager.Save();
    }

    public void LoadGame()
    {
        GameManager.manager.Load();
    }
}
