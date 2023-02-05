using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;
using UnityEngine.InputSystem;
using System;

public class GlowHover : NetworkBehaviour
{
    public LayerMask playerLayer;

    public GameObject indicator;
    public GameObject indicator2;

    public bool isHover;
    public bool isHover2;
    [SerializeField] GameObject target;
    [SerializeField] GameObject target2;
    public override void OnStartClient()
    {
        base.OnStartClient();
        //if (!base.IsOwner) this.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        isHover = false;
    }

    // Update is called once per frame
    void Update()
    {

        AttackIndicatorUpdate();
        HelpIndicatorUpdate();

       
    }

    private void AttackIndicatorUpdate()
    {
        indicator.SetActive(isHover);
        indicator.transform.LookAt(Camera.main.transform);
        if (!base.IsOwner) return;
        GameObject gtarget = GetPlayerUnderMouse();
        if (gtarget == null)
        {
            if (target == null) return;
            target.GetComponent<GlowHover>().isHover = false;
        }
        else
        {
            gtarget.GetComponent<GlowHover>().isHover = true;
            target = gtarget;
        }

    }

    private void HelpIndicatorUpdate()
    {
        indicator2.SetActive(isHover2);
        indicator2.transform.LookAt(Camera.main.transform);
        if (!base.IsOwner) return;
        GameObject gtarget2 = GetAlayUnderMouse();
        if (gtarget2 == null)
        {
            if (target2 == null) return;
            target2.GetComponent<GlowHover>().isHover2 = false;
        }
        else
        {
            gtarget2.GetComponent<GlowHover>().isHover2 = true;
            target2 = gtarget2;
        }

    }

    private GameObject GetPlayerUnderMouse()
    {

        GameObject g = null;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, playerLayer))
        {
            g = hit.transform.gameObject;
            if (g.GetComponent<TeamManager>().teamID == gameObject.GetComponent<TeamManager>().teamID) g = null;
            //Debug.Log(hit.transform.name);
        }
        if (g == gameObject) g = null;
        return g;
    }

    private GameObject GetAlayUnderMouse()
    {

        GameObject g = null;
        RaycastHit hit;
        Ray ray = Camera.main.ScreenPointToRay(Mouse.current.position.ReadValue());
        if (Physics.Raycast(ray, out hit, Mathf.Infinity, playerLayer))
        {
            g = hit.transform.gameObject;
            if (g.GetComponent<TeamManager>().teamID != gameObject.GetComponent<TeamManager>().teamID || !g.GetComponent<HealthScript>().isDead) g = null;
            //Debug.Log(hit.transform.name);
        }
        if (g == gameObject) g = null;
        return g;
    }
}
