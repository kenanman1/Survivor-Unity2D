using System;
using UnityEngine;

/// <summary>
/// The PlayerController class is responsible for managing the player's state.
/// </summary>
public class PlayerController : MonoBehaviour
{
    public static event Action OnPlayerDeathEvent;

    [Header("Player Settings")]
    private PlayerHealth playerHealth;

    private void Start()
    {
        playerHealth = GetComponent<PlayerHealth>();
    }

    public void TakeDamageFromPlayer(float damage)
    {
        playerHealth.TakeDamageFromPlayer(damage);
    }

    public void OnPlayerDeath()
    {
        foreach (Transform child in transform)
        {
            Destroy(child.gameObject);
        }
        OnPlayerDeathEvent?.Invoke();
    }

    public void Upgrade(UpgradeType upgradeType, int upgradeValue)
    {
        switch (upgradeType)
        {
            case UpgradeType.Health:
                GetComponentInChildren<PlayerHealth>().health += upgradeValue;
                break;
            case UpgradeType.CottonCandyDamage:
                GetComponentInChildren<WeaponWithGun>().attackDamage += upgradeValue;
                break;
            case UpgradeType.DeBoinkDamage:
                GetComponentInChildren<MeleeWeapon>().attackDamage += upgradeValue;
                break;
            case UpgradeType.Speed:
                GetComponent<PlayerMovenment>().speed += upgradeValue;
                break;
        }
    }
}
