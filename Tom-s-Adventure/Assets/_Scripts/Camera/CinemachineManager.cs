using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Cinemachine;

public class CinemachineManager : MyMonoBehaviour
{
    [SerializeField] protected CinemachineVirtualCamera virCam;
    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadVirtualCam();
    }
    protected virtual void LoadVirtualCam()
    {
        if (this.virCam != null) return;
        this.virCam = transform.GetComponent<CinemachineVirtualCamera>();
    }
    protected override void Start()
    {
        base.Start();
        virCam.Follow = GameObject.Find("Player").transform;
    }
}
