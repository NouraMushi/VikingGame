using UnityEngine;
using UnityEngine.SceneManagement;

public class MenuControl : MonoBehaviour
{
    public void LoadMap()
    {
        SceneManager.LoadScene("Map");
    }
}
