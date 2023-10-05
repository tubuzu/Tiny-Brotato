using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class PlayerDamageReceiver : DamageReceiver
{
    [SerializeField] protected PlayerCtrl playerCtrl;
    public PlayerCtrl PlayerCtrl { get { return playerCtrl; } }

    protected override void LoadComponents()
    {
        base.LoadComponents();
        this.LoadPlayerCtrl();
    }
    protected virtual void LoadPlayerCtrl()
    {
        this.playerCtrl = transform.parent.GetComponent<PlayerCtrl>();
        if (this.playerCtrl == null) this.playerCtrl = transform.GetComponent<PlayerCtrl>();
    }

    protected override void OnEnable()
    {
        this.hpMax = playerCtrl.PlayerStatus.MaxHealth;
        base.OnEnable();
        GameManager.Instance.OnWaveStart += OnGameStart;
        GameManager.Instance.OnWaveStop += OnGameStop;
    }

    protected virtual void OnDisable()
    {
        GameManager.Instance.OnWaveStart -= OnGameStart;
        GameManager.Instance.OnWaveStop -= OnGameStop;
    }

    public KeyValuePair<float, bool> GetEnemyDamage(float damage)
    {
        if (Random.Range(0, 101) < playerCtrl.PlayerStatus.DodgeRate)
        {
            return new KeyValuePair<float, bool>(0, false);
        }
        return new KeyValuePair<float, bool>(damage * (1 - (float)playerCtrl.PlayerStatus.Armor / 100), true);
    }

    public override void Deduct(float deduct)
    {
        KeyValuePair<float, bool> enemyDamage = GetEnemyDamage(deduct);
        if (enemyDamage.Value)
        {
            base.Deduct(enemyDamage.Key);
        }
        else TextPopupSpawner.Instance.Spawn(EnumManager.TextPopupCode.DODGE_TEXT_POPUP.ToString(), new Vector3(transform.position.x, transform.position.y + 0.5f, 0f), Quaternion.identity);
    }

    protected override void OnDeduct(float deduct)
    {
        base.OnDeduct(deduct);
        UIManager.Instance.ChangeHpValue(hp / hpMax);
        CameraShaker.Invoke();
    }

    protected override void OnAdd(float add)
    {
        base.OnAdd(add);
        UIManager.Instance.ChangeHpValue(hp / hpMax);
        GameObject regenText = TextPopupSpawner.Instance.Spawn(EnumManager.TextPopupCode.HP_REGEN_TEXT_POPUP.ToString(), new Vector3(transform.position.x, transform.position.y + 0.5f, 0f), Quaternion.identity);
        regenText.GetComponentInChildren<TextMeshProUGUI>().text = "+" + ((int)add).ToString();
    }

    public override void Reborn()
    {
        base.Reborn();
        UIManager.Instance.ChangeHpValue(hp / hpMax);
    }

    void OnGameStart()
    {
        hpMax = playerCtrl.PlayerStatus.MaxHealth;
        Reborn();
        StartCoroutine(RegenHPCoroutine());
        isImmortal = false;
    }
    void OnGameStop()
    {
        StopAllCoroutines();
        isImmortal = true;
    }
    IEnumerator RegenHPCoroutine()
    {
        while (true)
        {
            yield return new WaitForSeconds(10f);
            Add(playerCtrl.PlayerStatus.HealthRegenRate);
        }
    }

    protected override void OnDead()
    {
        base.OnDead();
        playerCtrl.PlayerAnimation.Die();
        GameManager.Instance.GameOver();
    }
}
