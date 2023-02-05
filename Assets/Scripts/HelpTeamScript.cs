using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;
using FishNet.Object.Synchronizing;
using UnityEngine.InputSystem;
using StarterAssets;

public class HelpTeamScript : NetworkBehaviour
{
    [SerializeField] GameObject target;

    StarterAssetsInputs _input;
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner) this.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        _input = GetComponent<StarterAssetsInputs>();
    }

    // Update is called once per frame
    void Update()
    {
        if (_input.revive)
        {
            _input.revive = false;
            target = GetClosestPlayer();
            if (target == null) return;

            if (target.GetComponent<TeamManager>().teamID == gameObject.GetComponent<TeamManager>().teamID)
            {
                Debug.Log("Same team");
                if (target.GetComponent<HealthScript>().isDead)
                {
                    Debug.Log("Reviving");

                    ReviveTeammate(target);
                    //target.GetComponent<HealthScript>().lifes = 3;
                    //target.GetComponent<HealthScript>().isDead = false;
                }
            }
        }
        
        
       
    }


    [ServerRpc]
    private void ReviveTeammate(GameObject target)
    {
        target.GetComponent<HealthScript>().isDead = false;
    }
    private GameObject GetClosestPlayer() {

        GameObject[] enemies = GameObject.FindGameObjectsWithTag("Player");
        GameObject tMin = null;
        float minDist = Mathf.Infinity;
        Vector3 currentPos = transform.position;
        foreach (GameObject t in enemies)
        {
            if(t != this.gameObject)
            {
                float dist = Vector3.Distance(t.transform.position, currentPos);
                if (dist < minDist)
                {
                    tMin = t;
                    minDist = dist;
                }
            }
            
        }
        return tMin;
    }
}
