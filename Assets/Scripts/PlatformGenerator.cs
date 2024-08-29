using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public Transform playerTransform;
    public float verticalOffset = 2.5f;
    public float horizontalRange = 2f;
    public float minHorizontalDistance = 1.0f;

    private GameObject lastPlatform;
    private PlatformManager platformManager;

    void Start()
    {
        //Findet PlatformManager in der Szene
        platformManager = FindObjectOfType<PlatformManager>();
        GenerateInitialPlatform();
    }

    void GenerateInitialPlatform()
    {
        Vector3 startPosition = new Vector3(playerTransform.position.x, playerTransform.position.y - verticalOffset, 0);
        lastPlatform = Instantiate(platformPrefab, startPosition, Quaternion.identity);
        platformManager.AddPlatform(lastPlatform);
    }

    public void GenerateNextPlatform()
    {
        Vector3 newPosition;
        do
        {
            float randomX = Random.Range(-horizontalRange, horizontalRange);
            newPosition = new Vector3(randomX, lastPlatform.transform.position.y + verticalOffset, 0);
        } while (!IsPositionValid(newPosition));

        GameObject newPlatform = Instantiate(platformPrefab, newPosition, Quaternion.identity);
        platformManager.AddPlatform(newPlatform);
        lastPlatform = newPlatform;
    }

    private bool IsPositionValid(Vector3 newPosition)
    {
        return true;
    }
}
