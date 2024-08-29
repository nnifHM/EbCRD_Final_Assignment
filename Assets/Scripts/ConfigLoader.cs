using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ConfigLoader : MonoBehaviour
{
    public GameConfig gameConfig;

    void Start()
    {
        LoadConfig();
    }

    void LoadConfig()
    {
        // Lade die .json-Datei aus den Resources
        TextAsset configText = Resources.Load<TextAsset>("gameConfig");
        if (configText != null)
        {
            gameConfig = JsonUtility.FromJson<GameConfig>(configText.text);
            ApplyConfig();
        }
        else
        {
            Debug.LogError("Konfigurationsdatei nicht gefunden!");
        }
    }

    void ApplyConfig()
    {
        // Wende die geladene Konfiguration an (Beispiel)
        PlayerMovement playerMovement = FindObjectOfType<PlayerMovement>();
        if (playerMovement != null)
        {
            playerMovement.jumpspeed = gameConfig.playerJumpSpeed;
        }

        CameraController cameraController = FindObjectOfType<CameraController>();
        if (cameraController != null)
        {
            cameraController.startScrollSpeed = gameConfig.cameraStartSpeed;
            cameraController.speedIncreaseRate = gameConfig.cameraSpeedIncreaseRate;
            cameraController.maxScrollSpeed = gameConfig.maxCameraSpeed;
        }

        PlatformGenerator platformGenerator = FindObjectOfType<PlatformGenerator>();
        if (platformGenerator != null)
        {
            platformGenerator.verticalOffset = gameConfig.platformVerticalOffset;
            platformGenerator.horizontalRange = gameConfig.platformHorizontalRange;
            platformGenerator.minHorizontalDistance = gameConfig.minHorizontalDistance;
        }
    }
}
