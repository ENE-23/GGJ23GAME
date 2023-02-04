using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using FishNet;
using FishNet.Object;
using UnityEngine.InputSystem;

public class GlowHover : NetworkBehaviour
{
    public LayerMask playerLayer;
    public SkinnedMeshRenderer meshRenderer;
    public Material[] originalMaterial;
    public Shader glowShader;
    public override void OnStartClient()
    {
        base.OnStartClient();
        if (!base.IsOwner) this.enabled = false;
    }
    // Start is called before the first frame update
    void Start()
    {
        meshRenderer = transform.GetChild(4).transform.GetChild(1).GetComponent<SkinnedMeshRenderer>();
        originalMaterial = meshRenderer.materials;
    }

    // Update is called once per frame
    void Update()
    {
        GameObject gtarget = GetPlayerUnderMouse();
        if (gtarget == null) return;
        Renderer r = gtarget.GetComponent<GlowHover>().meshRenderer;
        foreach(Material m in r.materials)
        {
            m.shader = glowShader;
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
