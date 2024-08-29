using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;          // Referenz zum Spieler, um seine Position zu verfolgen
    public float startScrollSpeed = 1f; // Anfangsgeschwindigkeit des Scrollens
    public float speedIncreaseRate = 0.1f; // Geschwindigkeitserhöhung pro Sekunde
    public float maxScrollSpeed = 5f;  // Maximale Scrollgeschwindigkeit

    private float currentScrollSpeed;

    void Start()
    {
        currentScrollSpeed = startScrollSpeed; // Setze die Anfangsgeschwindigkeit
    }

    void Update()
    {
        // Erhöhe die Scrollgeschwindigkeit allmählich bis zur Maximalgeschwindigkeit
        currentScrollSpeed += speedIncreaseRate * Time.deltaTime;
        currentScrollSpeed = Mathf.Clamp(currentScrollSpeed, startScrollSpeed, maxScrollSpeed);

        // Bewege die Kamera nach oben
        transform.position += new Vector3(0, currentScrollSpeed * Time.deltaTime, 0);

        // Optional: Die Kamera folgt der Spielfigur, um sicherzustellen, dass der Spieler nicht zu tief fällt
        if (player.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }
    }
}
