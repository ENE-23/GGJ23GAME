using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object.Synchronizing;
using FishNet.Object;
using StarterAssets;
public class HealthScript : NetworkBehaviour
{
    [SyncVar] public int lifes = 3;

    ThirdPersonController thirdPersonController;
    

    private void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
    }

    private void OnBeingHit() {
        if (!base.IsOwner) return;

        thirdPersonController.enabled = false;
    }

    public void BeingHit() {
        if (!base.IsOwner) return;
        StartCoroutine(HitCoroutine());
    }

    IEnumerator HitCoroutine() {
        yield return new WaitForEndOfFrame();
        thirdPersonController.enabled = false;
        yield return new WaitForSeconds(1f);
        thirdPersonController.enabled = true;
    }
}
