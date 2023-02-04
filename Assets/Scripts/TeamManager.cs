using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object.Synchronizing;
using FishNet.Object;
using TMPro;

public class TeamManager : NetworkBehaviour
{

    [SyncVar] public int teamID;
    [SerializeField]TMP_Text text;
    int nextTeam = 0;

    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner) return;
            
        UpdateTeam();
        text.text = teamID.ToString();
    }
    // Start is called before the first frame update
    void Start()
    {
         
    }

    // Update is called once per frame
    void Update()
    {
        text.transform.LookAt(Camera.main.transform);
        text.text = teamID.ToString();
    }

    [ServerRpc]
    public void UpdateTeam()
    {
        teamID = UnityEngine.Random.Range(0, 2);
    }
}
