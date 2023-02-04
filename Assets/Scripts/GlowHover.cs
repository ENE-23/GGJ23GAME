using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;
using UnityEngine.InputSystem;

public class GlowHover : NetworkBehaviour
{
    public LayerMask playerLayer;

    public GameObject indicator;

    public bool isHover;
    [SerializeField] GameObject target;
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
        indicator.SetActive(isHover);
        indicator.transform.LookAt(Camera.main.transform);
        if (!base.IsOwner) return;
        GameObject gtarget = GetPlayerUnderMouse();
        if (gtarget == null)
        {
            if (target == null) return;
            target.GetComponent<GlowHover>().isHover = false;
        }
        else {
            gtarget.GetComponent<GlowHover>().isHover = true;
            target = gtarget;
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
            //Debug.Log(hit.transform.name);
        }
        if (g == gameObject) g = null;
        return g;
    }
}
