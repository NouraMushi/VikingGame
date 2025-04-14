using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Increasehelth : MonoBehaviour
{
    [SerializeField] private float Increasemaxhelth = 30f;
    [SerializeField] private float increasehelth = 30f;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {
            GameManager.manager.maxHealth += Increasemaxhelth;
            GameManager.manager.health += increasehelth;

            if (GameManager.manager.health > GameManager.manager.maxHealth)
            {
                GameManager.manager.health = GameManager.manager.maxHealth;
            }
            Destroy(gameObject);

        }

    }
}
