using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class PlatformManager : MonoBehaviour
{
    public List<GameObject> platforms;
    public Transform playerTransform;
    public float destroyThreshold = 2.0f;

    void Update()
    {
        DestroyPlatformsBelowPlayer();
    }

    void DestroyPlatformsBelowPlayer()
    {
        // Findet alle Plattformen, die sich mehr als destroyThreshold-Einheiten unterhalb des Spielers befinden
        var platformsToDestroy = platforms.Where(p => p != null && p.transform.position.y < playerTransform.position.y - destroyThreshold).ToList();

        foreach (var platform in platformsToDestroy)
        {
            platforms.Remove(platform);
            Destroy(platform);
        }
    }

    public void AddPlatform(GameObject platform)
    {
        if (!platforms.Contains(platform))
        {
            platforms.Add(platform);
        }
    }
}
