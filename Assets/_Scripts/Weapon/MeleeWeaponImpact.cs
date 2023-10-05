using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class MeleeWeaponImpact : MonoBehaviour
{
    public float attackRange;
    public LayerMask enemyLayer;
    public Collider2D _collider2D;
    public float damage;
    void Attack()
    {
        List<Collider2D> hitEnemies = new();
        ContactFilter2D contactFilter = new();
        contactFilter.SetLayerMask(enemyLayer);
        contactFilter.useTriggers = true;
        Physics2D.OverlapCollider(_collider2D, contactFilter, hitEnemies);

        foreach (var enemy in hitEnemies)
        {
            if (!enemy.gameObject.CompareTag("DamageReceiver")) continue;
            KeyValuePair<float, bool> kvp = new(0.0f, false);
            kvp = PlayerCtrl.Instance.PlayerDamageSender.GetPlayerDamage(damage);
            float finalDamage = kvp.Key;

            if (PlayerCtrl.Instance.PlayerDamageSender.Send(enemy.transform, finalDamage))
            {
                GameObject textPopupGO;
                if (kvp.Value)
                {
                    textPopupGO = TextPopupSpawner.Instance.Spawn(EnumManager.TextPopupCode.CRIT_DAMAGE_TEXT_POPUP.ToString(), transform.position, Quaternion.identity);
                }
                else
                {
                    textPopupGO = TextPopupSpawner.Instance.Spawn(EnumManager.TextPopupCode.DAMAGE_TEXT_POPUP.ToString(), transform.position, Quaternion.identity);
                }
                if (textPopupGO != null) textPopupGO.GetComponentInChildren<TextMeshProUGUI>().text = finalDamage.ToString();
            }
        }
    }
}
