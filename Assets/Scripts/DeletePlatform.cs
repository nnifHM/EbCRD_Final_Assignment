using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DeletePlatform : MonoBehaviour
{
    private bool playerHasJumpedOff = false;

    void OnCollisionExit2D(Collision2D collision)
    {
        // Prüfe, ob der Spieler die Plattform verlassen hat
        if (collision.gameObject.CompareTag("Player"))
        {
            playerHasJumpedOff = true;
        }
    }

    void Update()
    {
        // Wenn der Spieler abgesprungen ist, zerstöre die Plattform nach kurzer Zeit
        if (playerHasJumpedOff)
        {
            Destroy(gameObject, 1f); // Verzögerung von 1 Sekunde, bevor die Plattform zerstört wird
        }
    }
}
