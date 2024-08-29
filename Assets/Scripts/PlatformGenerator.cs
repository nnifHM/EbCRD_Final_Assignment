using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlatformGenerator : MonoBehaviour
{
    public GameObject platformPrefab; // Referenz auf das Plattform-Prefab
    public Transform playerTransform; // Referenz auf die Spielfigur
    public float verticalOffset = 2.5f; // Vertikaler Abstand zwischen Plattformen
    public float horizontalRange = 2f; // Horizontale Verschiebung der Plattformen
    public float minHorizontalDistance = 1.0f; // Mindestabstand zwischen Plattformen auf der X-Achse

    private List<GameObject> platforms = new List<GameObject>(); // Liste aller Plattformen
    private GameObject lastPlatform; // Die zuletzt erzeugte Plattform

    void Start()
    {
        // Erstelle die erste Plattform direkt unterhalb des Spielers
        Vector3 startPosition = new Vector3(playerTransform.position.x, playerTransform.position.y - verticalOffset, 0);
        lastPlatform = Instantiate(platformPrefab, startPosition, Quaternion.identity);
        platforms.Add(lastPlatform);
    }

    public void GenerateNextPlatform()
    {
        // Berechne die Position der nächsten Plattform basierend auf der Position der letzten Plattform
        float randomX;
        Vector3 newPosition;

        // Versuche, eine Plattform zu erzeugen, die nicht direkt über einer anderen Plattform liegt
        do
        {
            randomX = Random.Range(-horizontalRange, horizontalRange);
            newPosition = new Vector3(randomX, lastPlatform.transform.position.y + verticalOffset, 0);
        } 
        while (IsPositionAboveOtherPlatforms(newPosition));

        // Erstelle die neue Plattform und füge sie zur Liste hinzu
        GameObject newPlatform = Instantiate(platformPrefab, newPosition, Quaternion.identity);
        lastPlatform = newPlatform;
        platforms.Add(newPlatform);
    }

    private bool IsPositionAboveOtherPlatforms(Vector3 newPosition)
    {
        foreach (GameObject platform in platforms)
        {
            // Überprüfe, ob die neue Position zu nah an einer bestehenden Plattform ist
            if (Mathf.Abs(newPosition.x - platform.transform.position.x) < minHorizontalDistance &&
                Mathf.Abs(newPosition.y - platform.transform.position.y) < verticalOffset)
            {
                return true; // Die neue Position ist zu nah an einer bestehenden Plattform
            }
        }
        return false; // Die neue Position ist sicher
    }
}
