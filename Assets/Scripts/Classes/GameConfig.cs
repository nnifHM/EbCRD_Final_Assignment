using UnityEngine;

//Das Attribut [System.Serializable] ermöglicht es, die Klasse in der Unity-Umgebung und für das JSON-Parsing zu verwenden.
[System.Serializable]
public class GameConfig
{
    public float playerJumpSpeed;
    public float cameraStartSpeed;
    public float cameraSpeedIncreaseRate;
    public float maxCameraSpeed;
    public float platformVerticalOffset;
    public float platformHorizontalRange;
    public float minHorizontalDistance;
}
