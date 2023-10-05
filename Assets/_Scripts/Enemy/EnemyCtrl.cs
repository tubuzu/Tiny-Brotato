// using System.Collections;
using UnityEngine;

public class EnemyCtrl : MyMonoBehaviour
{
    [SerializeField] protected Transform model;
    public Transform Model => model;
    [SerializeField] protected EnemyAnimation enemyAnimation;
    public EnemyAnimation EnemyAnimation => enemyAnimation;
    [SerializeField] protected EnemyStatus enemyStatus;
    public EnemyStatus EnemyStatus { get => enemyStatus; }
    [SerializeField] protected EnemyDamageSender enemyDamageSender;
    public EnemyDamageSender EnemyDamageSender { get => enemyDamageSender; }
    [SerializeField] protected EnemyDamageReceiver enemyDamageReceiver;
    public EnemyDamageReceiver EnemyDamageReceiver { get => enemyDamageReceiver; }
    [SerializeField] protected EnemyMovement enemyMovement;
    public EnemyMovement EnemyMovement => enemyMovement;
    [SerializeField] protected EnemyPerformAbility enemyPerformAbility;
    public EnemyPerformAbility EnemyPerformAbility => enemyPerformAbility;

    [SerializeField] protected Rigidbody2D _rb;
    public Rigidbody2D Rb => _rb;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        if (!_rb) _rb = GetComponent<Rigidbody2D>();
        LoadModel();
        LoadEnemyAnimation();
        LoadEnemyDamageSender();
        LoadEnemyDamageReceiver();
        LoadEnemyStatus();
        LoadEnemyMovement();
        LoadEnemyPerformAbility();
    }
    protected virtual void LoadModel()
    {
        if (model != null) return;
        model = transform.Find("Model").GetComponent<Transform>();
    }

    protected virtual void LoadEnemyAnimation()
    {
        if (enemyAnimation != null) return;
        enemyAnimation = transform.Find("Model").GetComponent<EnemyAnimation>();
    }

    protected virtual void LoadEnemyDamageSender()
    {
        if (enemyDamageSender != null) return;
        enemyDamageSender = transform.Find("DamageSender").GetComponent<EnemyDamageSender>();
    }
    protected virtual void LoadEnemyDamageReceiver()
    {
        if (enemyDamageReceiver != null) return;
        enemyDamageReceiver = transform.Find("DamageReceiver").GetComponent<EnemyDamageReceiver>();
    }
    protected virtual void LoadEnemyStatus()
    {
        if (enemyStatus != null) return;
        enemyStatus = transform.Find("Status").GetComponent<EnemyStatus>();
    }
    protected virtual void LoadEnemyMovement()
    {
        if (enemyMovement != null) return;
        enemyMovement = transform.Find("Movement").GetComponent<EnemyMovement>();
    }
    protected virtual void LoadEnemyPerformAbility()
    {
        if (enemyPerformAbility != null) return;
        enemyPerformAbility = transform.Find("Abilities").GetComponent<EnemyPerformAbility>();
    }
}
