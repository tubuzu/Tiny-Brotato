using System;
// using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Runtime.Serialization.Formatters.Binary;
using UnityEngine;

[Serializable]
public class SaveWeapon
{
    public EnumManager.WeaponCode weaponCode;
    public int level;

    public SaveWeapon(EnumManager.WeaponCode weaponCode, int level)
    {
        this.weaponCode = weaponCode;
        this.level = level;
    }
}

[Serializable]
public class SaveItem
{
    public EnumManager.ItemCode itemCode;
    public int count;

    public SaveItem(EnumManager.ItemCode itemCode, int count)
    {
        this.itemCode = itemCode;
        this.count = count;
    }
}

[Serializable]
public class SaveData
{
    public bool progressSaved;

    public int currentWave;
    public List<SaveItem> items;
    public List<SaveWeapon> weapons;

    public int curLevel;
    public float curExp;
    public int curGem;

    public SaveData()
    {
        progressSaved = false;
        items = new();
        weapons = new();
    }
}

public static class SaveSystem
{
    public static void Save(SaveData data)
    {
        BinaryFormatter formatter = new();
        FileStream fs = new FileStream(GetPath(), FileMode.Create);
        formatter.Serialize(fs, data);
        fs.Close();
    }

    public static SaveData Load()
    {
        if (!File.Exists(GetPath()))
        {
            SaveData emptyData = new SaveData();
            return emptyData;
        }

        BinaryFormatter formatter = new BinaryFormatter();
        FileStream fs = new FileStream(GetPath(), FileMode.Open);
        SaveData data = formatter.Deserialize(fs) as SaveData;
        fs.Close();

        return data;
    }

    private static string GetPath()
    {
        return Path.Combine(Application.persistentDataPath, "data.qnd");
    }
}
