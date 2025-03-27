using System.Collections;
using System.Collections.Generic;
using System.Data.Common;
using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public string LevelToLoad;
    // Start is called before the first frame update
    public bool cleared;
    void Start()
    {
        if (GameManager.manager.GetType().GetField(LevelToLoad).GetValue(GameManager.manager).ToString() == "True")
        {
            Cleared(true);
        }
    }

    // Update is called once per frame
    void Update()
    {

    }
    public void Cleared(bool isClear)
    {
        cleared = true;
        GameManager.manager.GetType().GetField(LevelToLoad).SetValue(GameManager.manager, true);
        if (isClear == true)
        {
            transform.GetChild(1).GetComponent<SpriteRenderer>().enabled = true;
            GetComponent<CircleCollider2D>().enabled = false;
        }
    }
}
