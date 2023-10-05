using System.Collections;
using System.Collections.Generic;
using System.Linq;
using TMPro;
using Unity.VisualScripting;
using UnityEngine;

public class EnemyPerformAbility : EnemyAbstract
{
    [SerializeField] protected float attackRate = 6f;
    protected float warningTime = 1f;
    protected float timer = 0f;

    List<EnemyAbility> enemyAbilities;

    int randomAbilityIndex;
    bool attackPerforming = false;
    public bool AttackPerforming => attackPerforming;
    bool attackReleasing = false;
    public bool AttackReleasing => attackReleasing;

    protected override void Awake()
    {
        base.Awake();
        enemyAbilities = transform.GetComponentsInChildren<EnemyAbility>().ToList();
    }

    protected override void OnEnable()
    {
        base.OnEnable();
        attackPerforming = false;
        attackReleasing = false;
    }

    private void FixedUpdate()
    {
        if (!GameManager.Instance.WaveInProgress || enemyCtrl.EnemyDamageReceiver.IsDead())
        {
            if (attackPerforming)
            {
                CancelAbility();
                enemyCtrl.EnemyAnimation.StopAllCoroutines();
                enemyAbilities[randomAbilityIndex].CancelAttack();
            }
            return;
        }

        if (timer < attackRate)
        {
            timer += Time.fixedDeltaTime;
        }
        else if (PerformAbility())
        {
            timer = 0;
        }
    }

    protected bool PerformAbility()
    {
        float distanceToPlayer = (PlayerCtrl.Instance.transform.position - enemyCtrl.transform.position).magnitude;
        EnemyAbility[] validAbilities = enemyAbilities.Where(x => x.Range >= distanceToPlayer).ToArray();
        if (validAbilities.Length == 0) return false;

        StartCoroutine(AttackPerformCoroutine(validAbilities));
        return true;
    }

    IEnumerator AttackPerformCoroutine(EnemyAbility[] validAbilities)
    {
        attackPerforming = true;
        enemyCtrl.EnemyAnimation.WarningBeforeAttack(warningTime);

        yield return new WaitForSeconds(warningTime);

        randomAbilityIndex = Random.Range(0, validAbilities.Length);
        EnemyAbility randomAbility = validAbilities[randomAbilityIndex];

        attackReleasing = true;
        yield return randomAbility.ReleaseAttack();
        attackReleasing = false;
        attackPerforming = false;
    }

    public void CancelAbility() => StopAllCoroutines();
}
