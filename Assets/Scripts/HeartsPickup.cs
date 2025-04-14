using UnityEngine;
public class HeartPickup : MonoBehaviour
{
    [SerializeField] private float healAmount = 25f;

    private void OnTriggerEnter2D(Collider2D collision)
    {

        if (collision.CompareTag("Player"))
        {

            GameManager.manager.health += healAmount;

            if (GameManager.manager.health > GameManager.manager.maxHealth)
            {
                GameManager.manager.health = GameManager.manager.maxHealth;
            }
            Destroy(gameObject);
        }
    }
}