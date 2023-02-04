using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using TMPro;

public class GameManager : NetworkBehaviour
{

    [SyncVar]public int FirstTeamScore = 0;
    [SyncVar]public int SecondTeamScore = 0;

    [SerializeField] private TMP_Text firstText;
    [SerializeField] private TMP_Text secondText;
    // Start is called before the first frame update

    
    [ServerRpc]
    public void AddPointToTeam(int team, int score)
    {
        if (team == 2) FirstTeamScore += score;
        else SecondTeamScore += score;
    }

    private void Update()
    {
        firstText.text = FirstTeamScore.ToString();
        secondText.text = SecondTeamScore.ToString();
    }
}
