using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class UIManager : MonoBehaviour
{
    #region PLAYER_UI
    [Header("Player UI")]
    [Header("sprites")]
    public Sprite playerHealthSpriteRef;
    public Sprite playerHealthDepletedSpriteRef;
    [Header("Display Info")]
    public Image[] HealthBar;
    public TMP_Text TMPCoins;
    public TMP_Text TMPBombs;
    public TMP_Text TMPScore;
    public GameObject Continue;
    public TMP_Text TMPContinueCount;
    public GameObject GameOver;
    public TMP_Text TMPFinalScore;
    #endregion PLAYER_UI

    #region AI_UI
    [Header("AI UI")]
    [Header("Display Info")]
    //public Image[] BuyIcons;
    public GameObject BuyTemplate;
    public Transform BuyPanel;
    public TMP_Text TMPCash;


    private bool aiBuyOptionsInitialized = false;
    #endregion AI_UI

    private PlayerManager playerManager;
    private AIManager aiManager;
    private GameManager gameManager;

    string finalScore = string.Empty;

    private void RenderPlayerUI()
    {
        if (this.playerManager == null) Managers.TryGetPlayerManager(out this.playerManager);
        else
        {
            this.TMPScore.text = this.playerManager.Score.ToString().PadLeft(6, '0');
            this.TMPBombs.text = this.playerManager.Bombs.ToString();
            HealthBar[0].sprite = (this.playerManager.Health > 0) ? this.playerHealthSpriteRef : this.playerHealthDepletedSpriteRef;
            HealthBar[1].sprite = (this.playerManager.Health > 1) ? this.playerHealthSpriteRef : this.playerHealthDepletedSpriteRef;
            HealthBar[2].sprite = (this.playerManager.Health > 2) ? this.playerHealthSpriteRef : this.playerHealthDepletedSpriteRef;
            
        }

        if (this.gameManager == null) Managers.TryGetGameManager(out this.gameManager);
        else
        {
            this.TMPCoins.text = this.gameManager.Coins.ToString();
            this.Continue.SetActive(this.gameManager.IsCountDown);
            this.TMPContinueCount.text = this.gameManager.CountDownTimer.ToString();
            this.GameOver.SetActive(this.gameManager.IsGameOver);

            if(this.playerManager != null)
            {
                this.TMPFinalScore.text = this.playerManager.Score.ToString().PadLeft(6, '0');
            }
        }
    }

    private void InitializeAIUI()
    {
        if (this.aiManager.AIAssets == null) return;
        if (this.aiManager.AIAssets.Length == 0) return;

        for (int index = 0; index < this.aiManager.AIAssets.Length; index++)
        {
            GameObject panel = GameObject.Instantiate(BuyTemplate, BuyPanel);
            
            panel.GetComponent<BuyTemplateInfo>().Configure(this.aiManager.AIAssets[index], index);
        }

        this.aiBuyOptionsInitialized = true;
    }

    private void RenderAIUI()
    {
        if (this.aiManager == null) Managers.TryGetAIManager(out this.aiManager);
        else
        {
            if (!aiBuyOptionsInitialized) InitializeAIUI();
        }
    }

    private void Update()
    {
        RenderPlayerUI();
        RenderAIUI();
    }
}
