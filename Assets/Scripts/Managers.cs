using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(DestroyManager))]
[RequireComponent(typeof(PlayerManager))]
[RequireComponent(typeof(UIManager))]
[RequireComponent(typeof(GameManager))]
[RequireComponent(typeof(AIManager))]
public class Managers : MonoBehaviour
{
    private static DestroyManager destroyManager;
    private static PlayerManager playerManager;
    private static AIManager aiManager;
    private static UIManager uiManager;
    private static GameManager gameManager;
    public static bool IsReady = false;

    public static bool TryGetDestroyManager(out DestroyManager refDestroyManager)
    {
        refDestroyManager = null;
        if (!IsReady) return false;
        refDestroyManager = destroyManager;
        return true;
    }

    public static bool TryGetPlayerManager(out PlayerManager refPlayerManager)
    {
        refPlayerManager = null;
        if (!IsReady) return false;
        refPlayerManager = playerManager;
        return true;
    }

    public static bool TryGetAIManager(out AIManager refAIManager)
    {
        refAIManager = null;
        if (!IsReady) return false;
        refAIManager = aiManager;
        return true;
    }

    public static bool TryGetUIManager(out UIManager refUIManager)
    {
        refUIManager = null;
        if (!IsReady) return false;
        refUIManager = uiManager;
        return true;
    }
    public static bool TryGetGameManager(out GameManager refGameManager)
    {
        refGameManager = null;
        if (!IsReady) return false;
        refGameManager = gameManager;
        return true;
    }

    public void Start()
    {
        destroyManager = this.GetComponent<DestroyManager>();
        playerManager = this.GetComponent<PlayerManager>();
        aiManager = this.GetComponent<AIManager>();
        uiManager = this.GetComponent<UIManager>();
        gameManager = this.GetComponent<GameManager>();

        IsReady = true;
    }
}
