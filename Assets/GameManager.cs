using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int Coins;
    private float countDownTimer;
    public bool IsGameOver;
    public bool IsCountDown;
    public int CountDownTimer { get { return (int)(this.countDownTimer + 0.5f); } }
    private bool hasStartedCountDown = false;

    private PlayerManager playerManager;
    private bool hasInitialized = false;

    private void Initialize()
    {
        this.Coins = 2;
        this.playerManager.Health = 3;
        this.IsCountDown = false;
        this.IsGameOver = false;
        this.hasStartedCountDown = false;
        this.hasInitialized = true;
    }

    public void IsContinue()
    {
        this.Coins--;
        this.playerManager.Health = 3;
        this.IsCountDown = false;
        this.IsGameOver = false;
    }

    private void Update()
    {
        if (Managers.TryGetPlayerManager(out this.playerManager))
        {
            if (!hasInitialized) this.Initialize();

            if (this.playerManager.Health != 0) return;
            if(this.Coins > 0)
            {
                this.countDownTimer -= Time.deltaTime;

                if (!this.hasStartedCountDown)
                {
                    this.countDownTimer = 9;
                    this.hasStartedCountDown = true;
                }
            }

            if(this.Coins == 0 && this.countDownTimer == 0)
            {
                this.IsGameOver = true;
            }
        }
    }
}
