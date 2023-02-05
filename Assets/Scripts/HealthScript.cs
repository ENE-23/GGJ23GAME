using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet.Object.Synchronizing;
using FishNet.Object;
using StarterAssets;
using FishNet.Transporting;
using UnityEngine.UI;

public class HealthScript : NetworkBehaviour
{
    [SyncVar(Channel = Channel.Unreliable, ReadPermissions = ReadPermission.Observers, SendRate = 0.1f, OnChange = nameof(OnHit))] public int lifes = 3;
    [SyncVar(Channel = Channel.Unreliable, ReadPermissions = ReadPermission.Observers, SendRate = 0.1f, OnChange = nameof(OnDead))] public bool isDead = false;
    ThirdPersonController thirdPersonController;
    Animator _animator;
    private int animIDIsHit;
    [SerializeField] GameObject rotedGO;
    [SerializeField] Slider slider;

    private void Start()
    {
        thirdPersonController = GetComponent<ThirdPersonController>();
        _animator = GetComponent<Animator>();
        animIDIsHit = Animator.StringToHash("isHit");
        slider.value = lifes;

    }

    private void Update()
    {
        slider.gameObject.transform.LookAt(Camera.main.transform);
        if (!base.IsOwner) return;
        thirdPersonController.enabled = !isDead;
    }

    private void OnHit(int prev, int next, bool asServer)
    {
        if (lifes <= 0)
        {
            rotedGO.SetActive(true);
            Debug.Log("isDead");
            isDead = true;
            _animator.SetBool("isDead", true);
            thirdPersonController.enabled = false;
        }
        else StartCoroutine(HitCoroutine());
    }

    private void OnDead(bool prev, bool next, bool asServer)
    {
        slider.value = lifes;

        if (!next)
        {
            rotedGO.SetActive(false);

            lifes = 3;
            _animator.SetBool("isDead", false);
            thirdPersonController.enabled = true;
        }
    }
    private void OnBeingHit() {
        if (!base.IsOwner) return;

        thirdPersonController.enabled = false;
    }

    //[ServerRpc]
    [Client]
    public void BeingHit() {
        if (lifes <= 0) {
            Debug.Log("isDead");
            isDead = true;
            _animator.SetBool("isDead",true);
            thirdPersonController.enabled = false;
        }
        else StartCoroutine(HitCoroutine());
    }

    //[ServerRpc]
    [Client]
    public void BeingRevived()
    {
        isDead = false;
        lifes = 3;
        _animator.SetBool("isDead", false);
        thirdPersonController.enabled = true;
    }

    IEnumerator HitCoroutine() {
        slider.value = lifes;

        yield return new WaitForEndOfFrame();
        _animator.SetBool("isHit", true);
        thirdPersonController.enabled = false;
        yield return new WaitForSeconds(.5f);
        thirdPersonController.enabled = true;
        _animator.SetBool("isHit", false);
    }
}
