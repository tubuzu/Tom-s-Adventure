using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : Singleton<PlayerCtrl>
{
    [SerializeField] protected Transform model;
    public Transform Model => model;
    [SerializeField] protected PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement { get => playerMovement; }
    [SerializeField] protected ItemCollector itemCollector;
    public ItemCollector ItemCollector => itemCollector;

    protected override void Awake()
    {
        this.MakeSingleton(false);
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
        this.LoadPlayerMovement();
        this.LoadItemCollector();
    }
    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model").GetComponent<Transform>();
        // Debug.Log(transform.name + ": LoadModel", gameObject);
    }
    protected virtual void LoadPlayerMovement()
    {
        if (this.playerMovement != null) return;
        this.playerMovement = transform.Find("PlayerMovement").GetComponent<PlayerMovement>();
        // Debug.Log(transform.name + ": LoadPlayerMovement", gameObject);
    }
    protected virtual void LoadItemCollector() {
        if (this.itemCollector != null) return;
        this.itemCollector = transform.Find("ItemCollector").GetComponent<ItemCollector>();
    }
}
