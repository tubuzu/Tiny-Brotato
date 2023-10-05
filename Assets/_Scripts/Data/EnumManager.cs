using System.Collections.Generic;
using UnityEngine;

public class EnumManager : MonoBehaviour
{
    # region Item
    public enum ItemCode
    {
        RED_POTION = 0,
        GREEN_POTION = 1,
        BLUE_POTION = 2,
        GREEN_VACCINE = 3,
        PURPLE_VACCINE = 4,
        GREEN_BULLET_BOX = 5,
        RED_BULLET_BOX = 6,
        MEDICAL_KIT = 7,
        WATERMELON = 8,
    }
    public enum ItemType
    {
        ITEM = 0,
    }
    public enum ItemDropType
    {
        GEM = 0,
    }
    public enum ItemDropCode
    {
        GEM = 0,
    }
    #endregion

    # region Weapon
    public enum WeaponCode
    {
        SHERIFF = 0,
        VANDAL = 1,
        KNIGHT_SWORD = 2,
    }
    public enum WeaponType
    {
        RANGED = 0,
        MELEE = 1,
    }

    public enum WeaponProps
    {
        DAMAGE = 0,
        FIRERATE = 1,
        RANGE = 2,
        KNOCKBACK = 3,
        PIERCE = 4,
    }
    #endregion

    # region Player
    public enum PlayerProps
    {
        MAXHEALTH = 0,
        HEALTHREGERATE = 1,
        DAMAGEMULTIPLIER = 2,
        ATTACKRANGEMULTIPLIER = 3,
        ARMOR = 4,
        CRITICALRATE = 5,
        CRITICALDAMAGE = 6,
        ATTACKSPEED = 7,
        DODGERATE = 8,
        MOVESPEEDMULTIPLIER = 9,
        PICKUPRANGEMULTIPLIER = 10,
    }
    #endregion

    # region Enemy
    public enum EnemyCode
    {
        SLIME = 0,
        SPIDER = 1,
        BEE = 2,
        MINOTAUR = 3,
        RINO = 4,
    }
    #endregion

    # region Bullet
    public enum BulletCode
    {
        SHERIFF_BULLET = 0,
        VANDAL_BULLET = 1,
        ENEMY_BULLET = 2,
        BOSS_LASER = 3,
        BOSS_BOMB = 4,
    }
    # endregion

    # region Others
    public enum FXCode
    {
        NONE = 0,
        CROSS_WARNING = 1,
        SMALL_EXPLODE = 2,
    }
    public enum FaceDirection
    {
        NONE = 0,
        RIGHT = 1,
        LEFT = 2,
    }
    public enum TextPopupCode
    {
        NONE = 0,
        DAMAGE_TEXT_POPUP = 1,
        CRIT_DAMAGE_TEXT_POPUP = 2,
        HP_REGEN_TEXT_POPUP = 3,
        DODGE_TEXT_POPUP = 4,
    }
    #endregion

    static public Dictionary<ItemCode, string> ItemCode2String;
    static public Dictionary<WeaponCode, string> WeaponCode2String;
    static public Dictionary<WeaponProps, string> WeaponProps2String;
    static public Dictionary<PlayerProps, string> PlayerProps2String;

    private void Awake()
    {
        ItemCode2String = new Dictionary<ItemCode, string> {
            {ItemCode.RED_POTION, "RedPotion"},
            {ItemCode.GREEN_POTION, "GreenPotion"},
            {ItemCode.BLUE_POTION, "BluePotion"},
            {ItemCode.GREEN_VACCINE, "GreenVaccine"},
            {ItemCode.PURPLE_VACCINE, "PurpleVaccine"},
            {ItemCode.GREEN_BULLET_BOX, "GreenBulletBox"},
            {ItemCode.RED_BULLET_BOX, "RedBulletBox"},
            {ItemCode.MEDICAL_KIT, "MedicalKit"},
            {ItemCode.WATERMELON, "Watermelon"},
        };

        WeaponCode2String = new Dictionary<WeaponCode, string> {
            {WeaponCode.SHERIFF, "Gun"},
        };

        WeaponProps2String = new Dictionary<WeaponProps, string> {
            {WeaponProps.DAMAGE, "Damage"},
            {WeaponProps.FIRERATE, "FireRate"},
            {WeaponProps.RANGE, "Range"},
            {WeaponProps.KNOCKBACK, "KnockBack"},
            {WeaponProps.PIERCE, "Pierce"},
        };

        PlayerProps2String = new Dictionary<PlayerProps, string> {
            {PlayerProps.MAXHEALTH, "MaxHealth"},
            {PlayerProps.HEALTHREGERATE, "HealthRegeneration"},
            {PlayerProps.DAMAGEMULTIPLIER, "Damage"},
            {PlayerProps.ATTACKRANGEMULTIPLIER, "AttackRange"},
            {PlayerProps.ARMOR, "Armor"},
            {PlayerProps.CRITICALRATE, "CriticalRate"},
            {PlayerProps.CRITICALDAMAGE, "CriticalDamage"},
            {PlayerProps.ATTACKSPEED, "AttackSpeed"},
            {PlayerProps.DODGERATE, "DodgeRate"},
            {PlayerProps.MOVESPEEDMULTIPLIER, "MoveSpeed"},
            {PlayerProps.PICKUPRANGEMULTIPLIER, "PickUpRange"},
        };
    }

    private void OnDisable()
    {
        ItemCode2String.Clear();
        WeaponCode2String.Clear();
        WeaponProps2String.Clear();
        PlayerProps2String.Clear();
    }

    public static string GetItemCode(ItemCode itemCode) => ItemCode2String[itemCode];
    public static string GetWeaponCode(WeaponCode weaponCode) => WeaponCode2String[weaponCode];
    public static string GetWeaponPropKey(WeaponProps weaponProps) => WeaponProps2String[weaponProps];
    public static string GetPlayerPropKey(PlayerProps playerProps) => PlayerProps2String[playerProps];
}
