using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab;
    public Transform playerTransform;
    public float verticalOffset = 2.5f;
    public float horizontalRange = 2f;
    public float minHorizontalDistance = 1.0f;

    public GameObject GroundFloor;

    private GameObject lastPlatform;
    private PlatformManager platformManager;

    void Start()
    {
        platformManager = FindObjectOfType<PlatformManager>();
        lastPlatform = GroundFloor.gameObject;
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
