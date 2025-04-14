using UnityEngine;
using UnityEngine.SceneManagement;
using System.Collections;
using System.Collections.Generic;
using System;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using Unity.Collections;

public class GameManager : MonoBehaviour
{

    public static GameManager manager;
    public string currentLevel;
    public float health;
    public float previousHealth;
    public float maxHealth;

    public bool Level1;
    public bool Level2;
    public bool Level3;
    public bool Level4;
    public bool LevelSnow;


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

    public void Save()
    {
        Debug.Log("Game saved");
        BinaryFormatter bf = new BinaryFormatter();
        FileStream file = File.Create(Application.persistentDataPath + "/playerInfo.dat");
        PlayerData data = new PlayerData();
        data.health = health;
        data.currentLevel = currentLevel;
        data.previousHealth = previousHealth;
        data.maxHealth = maxHealth;
        data.level1 = Level1;
        data.level2 = Level2;
        data.level3 = Level3;
        data.level4 = Level4;
        data.levelSnow = LevelSnow;
        bf.Serialize(file, data);
        file.Close();
    }

    public void Load()
    {

        if (File.Exists(Application.persistentDataPath + "/playerInfo.dat"))
        {
            Debug.Log("Game loaded");
            BinaryFormatter bf = new BinaryFormatter();
            FileStream file = File.Open(Application.persistentDataPath + "/playerInfo.dat", FileMode.Open);
            PlayerData data = (PlayerData)bf.Deserialize(file);
            file.Close();
            health = data.health;
            currentLevel = data.currentLevel;
            previousHealth = data.previousHealth;
            maxHealth = data.maxHealth;
            Level1 = data.level1;
            Level2 = data.level2;
            Level3 = data.level3;
            Level4 = data.level4;
            LevelSnow = data.levelSnow;

        }
    }

}

[Serializable]
class PlayerData
{
    public string currentLevel;
    public float health;
    public float maxHealth;
    public float previousHealth;
    public bool level1;
    public bool level2;
    public bool level3;
    public bool level4;
    public bool levelSnow;

}
