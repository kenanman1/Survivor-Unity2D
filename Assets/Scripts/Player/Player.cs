using System;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMovenment))]
[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private List<Weapon> weapons = new List<Weapon>();
    private float TotalCandyCount { get; set; }
    private float TotalCashCount { get; set; }
    public float CandyToLevelUp { get; set; } = 5;
    private float Level { get; set; } = 1;

    public static Action onLevelUp;

    private void Start()
    {
        foreach (Weapon weapon in GetComponentsInChildren<Weapon>())
        {
            weapons.Add(weapon);
        }
    }

    private void Update()
    {
        LevelUp();
    }

    public void AddCandy(float count = 1)
    {
        TotalCandyCount += count;
        UIManager.Instance.UpdateCandyText(TotalCandyCount);
        UIManager.Instance.UpdateLevelSlider(TotalCandyCount);
    }

    public void AddCash(float count = 1)
    {
        TotalCashCount += count;
        UIManager.Instance.UpdateCashText(TotalCashCount);
    }

    private void LevelUp()
    {
        if (TotalCandyCount >= CandyToLevelUp)
        {
            onLevelUp?.Invoke();
            Level++;
            CandyToLevelUp = Mathf.RoundToInt(CandyToLevelUp * 1.5f);
            UIManager.Instance.UpdateLevelText(Level);
            UIManager.Instance.RestoreLevelSlider(CandyToLevelUp);
        }
    }

    public bool HasGun(WeaponTypes weapon)
    {
        foreach (Weapon item in weapons)
        {
            if (item.weaponType == weapon)
                return true;
        }
        return false;
    }
}
