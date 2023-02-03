using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object.Synchronizing;
using FishNet.Object;
public class HealthScript : NetworkBehaviour
{
    [SyncVar] public int lifes = 3;

    private void Update()
    {
        if (!base.IsOwner) return;


    }
}
