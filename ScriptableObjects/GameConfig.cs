using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[CreateAssetMenu(fileName = "GameConfig", menuName = "Config/New Game Config", order = 0)]
public class GameConfig : ScriptableObject
{
    public int StartMoney;
    public HealthUpgrades[] HealthUpgrades;
    public CountUpgrades[] CountUpgrades;
    public MergeUpgrades[] MergeUpgrades;
    public WeaponUpgrade[] Weapons;

}

[System.Serializable]
public class AudioConfig
{
    public AudioClip Audio;
    public float Volume;
}
[System.Serializable]
public struct HealthUpgrades
{
    public int Cost;
    public float Health;
}
[System.Serializable]
public struct CountUpgrades
{
    public int Cost;
}
[System.Serializable]
public struct MergeUpgrades
{
    public int Cost;
}
[System.Serializable]
public struct WeaponUpgrade
{
    public Sprite CardImage;
    public string WeaponText;
    public float Damage;
    public AnimatorOverrideController Controller;
    public float Range;

}


