using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object.Synchronizing;
using FishNet.Object;
using StarterAssets;
public class HealthScript : NetworkBehaviour
{
    [SyncVar] public int lifes = 3;
    [SyncVar] public bool isDead = false;
    ThirdPersonController thirdPersonController;
    Animator _animator;
    private int animIDIsHit;

    private void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        _animator = GetComponent<Animator>();
        animIDIsHit = Animator.StringToHash("isHit");
    }

    private void Update()
    {
        if (!base.IsOwner) return;
        if (lifes <= 0) thirdPersonController.enabled = false;
    }

    private void OnBeingHit() {
        if (!base.IsOwner) return;

        thirdPersonController.enabled = false;
    }

    [Client]
    public void BeingHit() {
        //if (!base.IsOwner) return;
        if (lifes <= 0) {
            Debug.Log("isDead");
            _animator.SetBool("isDead",true);
        }
        else StartCoroutine(HitCoroutine());
    }

    IEnumerator HitCoroutine() {
        
        yield return new WaitForEndOfFrame();
        _animator.SetBool("isHit", true);
        thirdPersonController.enabled = false;
        yield return new WaitForSeconds(1f);
        thirdPersonController.enabled = true;
        _animator.SetBool("isHit", false);
    }
}
