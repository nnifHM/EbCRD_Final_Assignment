using System.Collections;
using UnityEngine;

public class DeletePlatform : MonoBehaviour
{
    private PlatformManager platformManager;
    private bool playerHasJumpedOff = false;
    private Coroutine destroyCoroutine = null;

    void Start()
    {
        platformManager = FindObjectOfType<PlatformManager>();
    }

    void OnCollisionExit2D(Collision2D collision)
    {
        // Prüfe, ob der Spieler die Plattform verlassen hat
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player has left the platform.");
            playerHasJumpedOff = true;
            // Starte die Coroutine, um die Plattform nach einer Verzögerung zu zerstören
            destroyCoroutine = StartCoroutine(DestroyPlatformAfterDelay());
        }
    }

    void OnCollisionEnter2D(Collision2D collision)
    {
        // Wenn der Spieler die Plattform wieder betritt, breche die Zerstörungsroutine ab
        if (collision.gameObject.CompareTag("Player"))
        {
            Debug.Log("Player is back on the platform.");
            playerHasJumpedOff = false;

            if (destroyCoroutine != null)
            {
                StopCoroutine(destroyCoroutine);
                destroyCoroutine = null;
            }
        }
    }

    IEnumerator DestroyPlatformAfterDelay()
    {
        yield return new WaitForSeconds(1f);

        if (playerHasJumpedOff)
        {
            platformManager.platforms.Remove(gameObject);
            Destroy(gameObject);
        }
    }
}
