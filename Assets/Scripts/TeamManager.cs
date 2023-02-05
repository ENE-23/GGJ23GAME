using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Authenticating;
using FishNet.Object.Synchronizing;
using FishNet.Object;
using TMPro;

public class TeamManager : NetworkBehaviour
{

    [SyncVar] public int teamID;
    int nextTeam = 0;
    [SerializeField] GameObject hat1;
    [SerializeField] GameObject hat2;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner) return;
            
        UpdateTeam(LocalConnection.ClientId);

        
        //text.text = teamID.ToString();
        if (teamID == 0)
        {
            hat1.SetActive(true);
            hat2.SetActive(false);

        }
        else
        {
            hat1.SetActive(false);
            hat2.SetActive(true);
        }
    }
    // Start is called before the first frame update
    void Start()
    {

        
    }

    // Update is called once per frame
    void Update()
    {
        if(teamID == 0)
        {
            hat1.SetActive(true);
            hat2.SetActive(false);

        }
        else
        {
            hat1.SetActive(false);
            hat2.SetActive(true);
        }
    }

    [ServerRpc]
    public void UpdateTeam(int conID)
    {
        teamID = ((conID+1) % 2);
    }


}
