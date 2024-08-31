using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class CameraController : MonoBehaviour
{

    public Transform player;          // Referenz zum Spieler, um seine Position zu verfolgen
    public float startScrollSpeed = 1f; // Anfangsgeschwindigkeit des Scrollens
    public float speedIncreaseRate = 0.1f; // Geschwindigkeitserhöhung pro Sekunde
    public float maxScrollSpeed = 5f;  // Maximale Scrollgeschwindigkeit

    public Transform bg1;
    public Transform bg2;
    public float size;

    private float currentScrollSpeed;

    private bool shouldFollowPlayer = true;

    private bool gameActive;

    void Start()
    {
        gameActive = false;

        currentScrollSpeed = startScrollSpeed; // Setze die Anfangsgeschwindigkeit
        
        size = bg1.GetComponent<BoxCollider2D>().size.y;
    }

    void Update()
    {
        if (!gameActive) return;

        // Erhöhe die Scrollgeschwindigkeit allmählich bis zur Maximalgeschwindigkeit
        currentScrollSpeed += speedIncreaseRate * Time.deltaTime;
        currentScrollSpeed = Mathf.Clamp(currentScrollSpeed, startScrollSpeed, maxScrollSpeed);

        // Bewege die Kamera nach oben
        transform.position += new Vector3(0, currentScrollSpeed * Time.deltaTime, 0);

        //Die Kamera folgt der Spielfigur, um sicherzustellen, dass der Spieler nicht zu tief fällt
        if (player.position.y > transform.position.y)
        {
            transform.position = new Vector3(transform.position.x, player.position.y, transform.position.z);
        }
    }

    void FixedUpdate()
    {
        if(transform.position.y >= bg2.position.y)
        {
            bg1.position = new Vector3(bg1.position.x, bg2.position.y + size, bg1.position.z);
            SwitchBg();
        }
    }

    private void SwitchBg()
    {
        Transform temp = bg1;
        bg1 = bg2;
        bg2 = temp;
    }

    public void StartGame()
    {
        gameActive = true;
    }

    public void StopGame()
    {
        gameActive = false;
    }
}
