using System.Collections;
using UnityEngine;

public class AxeThrower : MonoBehaviour
{
    public GameObject playerAxe;
    public GameObject flyingAxePrefab;
    public float mouseThrowDistance = 10f;
    public float throwDuration = 1f; // Duration of the throw
    public float rotationSpeed = 720f;
    private bool isAxeReady = true;
    private GameObject currentFlyingAxe;
    private Camera mainCamera;

    void Start()
    {
        mainCamera = Camera.main;
    }

    void Update()
    {
        if (isAxeReady)
        {
            if (Input.GetKeyDown(KeyCode.T)) // Throw forward
            {
                Vector3 throwDirection = transform.localScale.x > 0 ? Vector3.right : Vector3.left; // Determine direction based on scale
                Vector3 targetPosition = transform.position + throwDirection * mouseThrowDistance; // Calculate forward position
                StartCoroutine(ThrowAxe(targetPosition));
            }
            else if (Input.GetMouseButtonDown(0)) // throw based on mouse direction
            {
                ThrowToMouseDirection();
            }
        }
    }

    void ThrowToMouseDirection()
    {
        Vector3 mousePosition = mainCamera.ScreenToWorldPoint(Input.mousePosition);
        mousePosition.z = 0;

        Vector3 endPos = transform.position + (mousePosition - transform.position).normalized * mouseThrowDistance;
        StartCoroutine(ThrowAxe(endPos));
    }

    IEnumerator ThrowAxe(Vector3 targetPosition)
    {
        isAxeReady = false;
        playerAxe.SetActive(false);

        currentFlyingAxe = Instantiate(flyingAxePrefab, transform.position, Quaternion.identity); // player position
        currentFlyingAxe.SetActive(true);

        float startTime = Time.time;
        Vector3 startPos = transform.position; // Start from the player position

        while ((Time.time - startTime) < throwDuration)
        {
            float fracComplete = (Time.time - startTime) / throwDuration;
            currentFlyingAxe.transform.position = Vector3.Lerp(startPos, targetPosition, fracComplete);
            currentFlyingAxe.transform.Rotate(0, 0, rotationSpeed * Time.deltaTime);
            yield return null;
        }

        Destroy(currentFlyingAxe, 0.1f);
        playerAxe.SetActive(true);
        isAxeReady = true;
    }
}

