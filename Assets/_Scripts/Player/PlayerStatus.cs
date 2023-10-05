using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerStatus : PlayerAbstract
{
    [Header("Player Status")]
    [SerializeField] int maxHealth = 10;
    [SerializeField] int healthRegenRate = 0; // health regen amount each 10s, if healthRegenRate = 10, each 10s player will regen 10hp
    [SerializeField] int damageMultiplier = 0; // final damage send: damage * (1 + damageMultiplier / 100)
    [SerializeField] int attackRangeMultiplier = 0; // final attack range: weaponAttackRange * (1 + attackRangeMultiplier / 100)
    [SerializeField] int armor = 0; // final damage receive: damageReceive * (1 - armor / 100);
    [SerializeField] int criticalRate = 10; // 10%
    [SerializeField] int criticalDamage = 50; // crit damage: damage * (1 + criticalDamage / 100)
    [SerializeField] int attackSpeed = 0; // weaponAttackSpeed: weaponAttackSpeed * (1 - attackSpeed / 100)
    [SerializeField] int dodgeRate = 0; // %
    [SerializeField] int moveSpeedMultiplier = 0; // moveSpeed: moveSpeed * (1 + moveSpeedMultiplier / 100)
    [SerializeField] int pickUpRangeMultiplier = 0; // pickUpRange: pickUpRange * (1 + pickUpRangeMultiplier / 100)

    [Header("Other Status")]
    [SerializeField] int gemNum = 0;
    [SerializeField] int level = 1;
    [SerializeField] float currentEXP = 0;
    [SerializeField] float maxEXP = 10;

    public int MaxHealth { get => maxHealth; set => maxHealth = value; }
    public int HealthRegenRate { get => healthRegenRate; set => healthRegenRate = value; }
    public int DamageMultiplier { get => damageMultiplier; set => damageMultiplier = value; }
    public int AttackRangeMultiplier { get => attackRangeMultiplier; set => attackRangeMultiplier = value; }
    public int Armor { get => armor; set => armor = value; }
    public int CriticalRate { get => criticalRate; set => criticalRate = value; }
    public int CriticalDamage { get => criticalDamage; set => criticalDamage = value; }
    public int AttackSpeed { get => attackSpeed; set => attackSpeed = value; }
    public int DodgeRate { get => dodgeRate; set => dodgeRate = value; }
    public int MoveSpeedMultiplier { get => moveSpeedMultiplier; set => moveSpeedMultiplier = value; }
    public int PickUpRangeMultiplier { get => pickUpRangeMultiplier; set => pickUpRangeMultiplier = value; }

    public int GemNum { get => gemNum; set => gemNum = value; }
    public int Level { get => level; set => level = value; }
    public float CurrentEXP { get => currentEXP; set => currentEXP = value; }
    public float MaxEXP { get => maxEXP; set => maxEXP = value; }

    public void ChangeMaxHealth(int value) => maxHealth += value;
    public void ChangeHealthRegeRate(int value) => healthRegenRate += value;
    public void ChangeDamageMultiplier(int value) => damageMultiplier += value;
    public void ChangeAttackRangeMultiplier(int value) => attackRangeMultiplier += value;
    public void ChangeArmor(int value) => armor += value;
    public void ChangeCriticalRate(int value) => criticalRate += value;
    public void ChangeCriticalDamage(int value) => criticalDamage += value;
    public void ChangeAttackSpeed(int value) => attackSpeed += value;
    public void ChangeDodgeRate(int value) => dodgeRate += value;
    public void ChangeMoveSpeedMultiplier(int value) => moveSpeedMultiplier += value;
    public void ChangePickUpRangeMultiplier(int value) => pickUpRangeMultiplier += value;

    public int GetMaxHealth() => maxHealth;
    public int GetHealthRegenRate() => healthRegenRate;
    public int GetDamageMultiplier() => damageMultiplier;
    public int GetAttackRangeMultiplier() => attackRangeMultiplier;
    public int GetArmor() => armor;
    public int GetCriticalRate() => criticalRate;
    public int GetCriticalDamage() => criticalDamage;
    public int GetAttackSpeed() => attackSpeed;
    public int GetDodgeRate() => dodgeRate;
    public int GetMoveSpeedMultiplier() => moveSpeedMultiplier;
    public int GetPickUpRangeMultiplier() => pickUpRangeMultiplier;

    public void ChangeGemNum(int value, bool changeExp = false)
    {
        gemNum += value;
        UIManager.Instance.ChangeGemCount(gemNum);
        if (changeExp) ChangeCurrentEXP(value);
    }
    public void ChangeCurrentEXP(float value)
    {
        currentEXP += value;
        if (currentEXP >= maxEXP) LevelUp();
        UIManager.Instance.ChangeExpValue(currentEXP / maxEXP);
    }


    public delegate void ChangePlayerAttr(int value);
    public delegate int GetPlayerAttr();
    public static Dictionary<EnumManager.PlayerProps, ChangePlayerAttr> ChangePlayerAttrFuncDict;
    public static Dictionary<EnumManager.PlayerProps, GetPlayerAttr> GetPlayerAttrDict;

    protected override void Awake()
    {
        base.Awake();
        ChangePlayerAttrFuncDict = new Dictionary<EnumManager.PlayerProps, ChangePlayerAttr> {
            {EnumManager.PlayerProps.MAXHEALTH, ChangeMaxHealth},
            {EnumManager.PlayerProps.HEALTHREGERATE, ChangeHealthRegeRate},
            {EnumManager.PlayerProps.DAMAGEMULTIPLIER, ChangeDamageMultiplier},
            {EnumManager.PlayerProps.ATTACKRANGEMULTIPLIER, ChangeAttackRangeMultiplier},
            {EnumManager.PlayerProps.ARMOR, ChangeArmor},
            {EnumManager.PlayerProps.CRITICALRATE, ChangeCriticalRate},
            {EnumManager.PlayerProps.CRITICALDAMAGE, ChangeCriticalDamage},
            {EnumManager.PlayerProps.ATTACKSPEED, ChangeAttackSpeed},
            {EnumManager.PlayerProps.DODGERATE, ChangeDodgeRate},
            {EnumManager.PlayerProps.MOVESPEEDMULTIPLIER, ChangeMoveSpeedMultiplier},
            {EnumManager.PlayerProps.PICKUPRANGEMULTIPLIER, ChangePickUpRangeMultiplier},
        };
        GetPlayerAttrDict = new Dictionary<EnumManager.PlayerProps, GetPlayerAttr> {
            {EnumManager.PlayerProps.MAXHEALTH, GetMaxHealth},
            {EnumManager.PlayerProps.HEALTHREGERATE, GetHealthRegenRate},
            {EnumManager.PlayerProps.DAMAGEMULTIPLIER, GetDamageMultiplier},
            {EnumManager.PlayerProps.ATTACKRANGEMULTIPLIER, GetAttackRangeMultiplier},
            {EnumManager.PlayerProps.ARMOR, GetArmor},
            {EnumManager.PlayerProps.CRITICALRATE, GetCriticalRate},
            {EnumManager.PlayerProps.CRITICALDAMAGE, GetCriticalDamage},
            {EnumManager.PlayerProps.ATTACKSPEED, GetAttackSpeed},
            {EnumManager.PlayerProps.DODGERATE, GetDodgeRate},
            {EnumManager.PlayerProps.MOVESPEEDMULTIPLIER, GetMoveSpeedMultiplier},
            {EnumManager.PlayerProps.PICKUPRANGEMULTIPLIER, GetPickUpRangeMultiplier},
        };
    }

    protected override void OnEnable()
    {
        // GameEvents.LevelUp += LevelUp;
    }

    private void OnDisable()
    {
        ChangePlayerAttrFuncDict.Clear();
    }

    public static ChangePlayerAttr GetChangePlayerAttrFunc(EnumManager.PlayerProps playerAttr)
        => ChangePlayerAttrFuncDict[playerAttr];

    public void LevelUp()
    {
        currentEXP -= maxEXP;
        maxEXP *= 1.6f;
        level++;
        UIManager.Instance.ShowLevelUpText(level);
    }

    // void OnWaveStart()
    // {

    // }
    // void OnWaveStop()
    // {

    // }

}
