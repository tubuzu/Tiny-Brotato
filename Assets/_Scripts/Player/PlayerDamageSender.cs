using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerDamageSender : DamageSender
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
        if (playerCtrl != null) return;
        Transform curTransform = transform;
        while (!curTransform.TryGetComponent(out playerCtrl)) curTransform = curTransform.parent;
    }

    public KeyValuePair<float, bool> GetPlayerDamage(float damage)
    {
        bool isCritical = Random.Range(0, 101) < playerCtrl.PlayerStatus.CriticalRate;
        float finalDamage = isCritical ? damage * (1 + (float)playerCtrl.PlayerStatus.CriticalDamage / 100) : damage;
        finalDamage *= 1 + (float)playerCtrl.PlayerStatus.DamageMultiplier / 100;
        return new KeyValuePair<float, bool>(finalDamage, isCritical);
    }
}
