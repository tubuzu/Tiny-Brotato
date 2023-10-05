// using System.Collections;
// using System.Collections.Generic;
using UnityEngine;

public class PlayerCtrl : MyMonoBehaviour
{
    protected static PlayerCtrl instance;
    public static PlayerCtrl Instance => instance;
    [SerializeField] protected Transform model;
    public Transform Model { get => model; }
    [SerializeField] protected PlayerAnimation playerAnimation;
    public PlayerAnimation PlayerAnimation { get => playerAnimation; }
    [SerializeField] protected PlayerMovement playerMovement;
    public PlayerMovement PlayerMovement { get => playerMovement; }
    [SerializeField] protected PlayerWeapon playerWeapon;
    public PlayerWeapon PlayerWeapon { get => playerWeapon; }
    [SerializeField] protected PlayerInventory playerInventory;
    public PlayerInventory PlayerInventory { get => playerInventory; }
    [SerializeField] protected PlayerDamageReceiver playerDamageReceiver;
    public PlayerDamageReceiver PlayerDamageReceiver { get => playerDamageReceiver; }
    [SerializeField] protected PlayerDamageSender playerDamageSender;
    public PlayerDamageSender PlayerDamageSender { get => playerDamageSender; }
    [SerializeField] protected PlayerStatus playerStatus;
    public PlayerStatus PlayerStatus { get => playerStatus; }
    [SerializeField] protected PlayerInput playerInput;
    public PlayerInput PlayerInput { get => playerInput; }

    protected override void Awake()
    {
        if (instance != null)
        {
            Debug.LogError("Only 1 PlayerCtrl");
            return;
        }
        instance = this;
    }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadModel();
        this.LoadPlayerAnimation();
        this.LoadPlayerWeapon();
        this.LoadPlayerInventory();
        this.LoadPlayerMovement();
        this.LoadPlayerDamageSender();
        this.LoadPlayerDamageReceiver();
        this.LoadPlayerStatus();
        this.LoadPlayerInput();
    }
    protected virtual void LoadModel()
    {
        if (this.model != null) return;
        this.model = transform.Find("Model").transform;
    }
    protected virtual void LoadPlayerMovement()
    {
        if (this.playerMovement != null) return;
        this.playerMovement = transform.Find("Movement").GetComponent<PlayerMovement>();
    }
    protected virtual void LoadPlayerAnimation()
    {
        if (this.playerAnimation != null) return;
        this.playerAnimation = transform.Find("Model").GetComponent<PlayerAnimation>();
    }
    protected virtual void LoadPlayerWeapon()
    {
        if (this.playerWeapon != null) return;
        this.playerWeapon = transform.Find("Weapon").GetComponent<PlayerWeapon>();
    }
    protected virtual void LoadPlayerInventory()
    {
        if (this.playerInventory != null) return;
        this.playerInventory = transform.Find("Inventory").GetComponent<PlayerInventory>();
    }
    protected virtual void LoadPlayerDamageSender()
    {
        if (this.playerDamageSender != null) return;
        this.playerDamageSender = transform.Find("Weapon").GetComponent<PlayerDamageSender>();
    }
    protected virtual void LoadPlayerDamageReceiver()
    {
        if (this.playerDamageReceiver != null) return;
        this.playerDamageReceiver = transform.Find("DamageReceiver").GetComponent<PlayerDamageReceiver>();
    }
    protected virtual void LoadPlayerStatus()
    {
        if (this.playerStatus != null) return;
        this.playerStatus = transform.Find("Status").GetComponent<PlayerStatus>();
    }

    protected virtual void LoadPlayerInput()
    {
        if (this.playerInput != null) return;
        this.playerInput = transform.Find("Input").GetComponent<PlayerInput>();
    }
}
