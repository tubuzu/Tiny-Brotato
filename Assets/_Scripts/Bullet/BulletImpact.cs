// using System.Collections;
// using System.Collections.Generic;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

[RequireComponent(typeof(Collider2D))]
public class BulletImpact : BulletAbstract
{
    [Header("Bullet Impact")]
    [SerializeField] protected Collider2D _collider;

    [SerializeField] protected int enemiesCanDamage = 0;

    [SerializeField] protected LayerMask shooterLayer;
    [SerializeField] protected LayerMask enemyLayer;

    [SerializeField] protected bool isLaser = false;

    protected override void LoadComponents()
    {
        base.LoadComponents();
        LoadCollider();
    }
    protected virtual void LoadCollider()
    {
        if (_collider != null) return;
        if (!TryGetComponent(out _collider)) return;
        _collider.isTrigger = true;
    }

    protected virtual void OnTriggerEnter2D(Collider2D other)
    {
        if (other.gameObject.CompareTag("DamageReceiver"))
        {
            if (!Utils.IsLayerInMask(other.gameObject.layer, enemyLayer) || enemiesCanDamage == 0) return;

            float damage = bulletCtrl.BulletDamageSender.Damage;

            bool isPlayerBullet = Utils.IsLayerInMask(LayerMask.NameToLayer("Player"), shooterLayer);
            KeyValuePair<float, bool> kvp = new KeyValuePair<float, bool>(0.0f, false); ;
            if (isPlayerBullet)
            {
                kvp = PlayerCtrl.Instance.PlayerDamageSender.GetPlayerDamage(damage);
                damage = kvp.Key;
            }

            if (bulletCtrl.BulletDamageSender.Send(other.transform, damage))
            {
                if (isPlayerBullet)
                {
                    GameObject textPopupGO;
                    if (kvp.Value)
                    {
                        textPopupGO = TextPopupSpawner.Instance.Spawn(EnumManager.TextPopupCode.CRIT_DAMAGE_TEXT_POPUP.ToString(), transform.position, Quaternion.identity);
                    }
                    else {
                        textPopupGO = TextPopupSpawner.Instance.Spawn(EnumManager.TextPopupCode.DAMAGE_TEXT_POPUP.ToString(), transform.position, Quaternion.identity);
                    }
                    if (textPopupGO != null) textPopupGO.GetComponentInChildren<TextMeshProUGUI>().text = ((int)damage).ToString();
                }
                if (enemiesCanDamage > 0)
                    enemiesCanDamage--;
                if (enemiesCanDamage == 0) DestroyBullet();
            }
            else if (!isLaser) DestroyBullet();
        }
    }

    protected virtual void DestroyBullet()
    {
        BulletSpawner.Instance.Despawn(bulletCtrl.gameObject);
    }

    public void Setup(LayerMask shooterLayer, LayerMask enemyLayer, float damage, int enemiesCanDamage)
    {
        this.shooterLayer = shooterLayer;
        this.enemyLayer = enemyLayer;
        bulletCtrl.BulletDamageSender.Damage = damage;
        this.enemiesCanDamage = enemiesCanDamage;
    }
}
