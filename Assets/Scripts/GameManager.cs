using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using TMPro;
using System;

public class GameManager : NetworkBehaviour
{

    [SyncVar]public int FirstTeamScore = 0;
    [SyncVar]public int SecondTeamScore = 0;

    [SyncVar] public float timeLeft = 60f;

    [SyncVar] public bool overTime = false;

    [SyncVar] public bool gameStarted = false;



    [SerializeField] private TMP_Text firstText;
    [SerializeField] private TMP_Text secondText;
    [SerializeField] private TMP_Text timeText;

    [SerializeField] private GameObject hud;
    [SerializeField] private GameObject blueWon;
    [SerializeField] private GameObject redWon;
    [SerializeField] private GameObject adminPanel;
    // Start is called before the first frame update



    public override void OnStartClient()
    {
        base.OnStartClient();
        if(base.LocalConnection.ClientId == 0)
        {
            adminPanel.SetActive(true);
        }
    }
    [ServerRpc]
    public void AddPointToTeam(int team, int score)
    {
        if (team == 2) FirstTeamScore += score;
        else SecondTeamScore += score;
    }

    private void Update()
    {
        //if (!gameStarted) return;
        UpdateTime();
        firstText.text = FirstTeamScore.ToString();
        secondText.text = SecondTeamScore.ToString();
    }

    private void UpdateTime()
    {


        if(timeLeft <= 0)
        {

            //timeText.text = "END";
            hud.SetActive(false);
            if(FirstTeamScore > SecondTeamScore) {
                redWon.SetActive(true);

            } else if(SecondTeamScore > FirstTeamScore)
            {
                blueWon.SetActive(true);
            }else
            {
                timeLeft = 15f;
                hud.SetActive(true);

                overTime = true;

            }

        }
        else if(!overTime)
        {
            timeLeft -= Time.deltaTime;
            timeText.text = timeLeft.ToString("0");
        }
        else
        {
            timeLeft -= Time.deltaTime;
            timeText.text = "Overtime: " + timeLeft.ToString("0");
        }


    }

    [ServerRpc]
    public void StartGame()
    {

        gameStarted = true;
        adminPanel.SetActive(false);
    }
}
