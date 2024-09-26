using System.Collections.Generic;
using TMPro;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public enum UpgradeType
{
    Health,
    CottonCandyDamage,
    DeBoinkDamage,
    Speed
}

public class StatsUpgradeManager : MonoBehaviour
{
    [SerializeField] private GameObject statsUpgrade;

    private List<Button> buttons;
    private Player player;

    private void Start()
    {
        player = FindObjectOfType<Player>();
        Player.onLevelUp += ConfigureUpgrade;
        buttons = new List<Button>(statsUpgrade.GetComponentsInChildren<Button>());
    }

    private void ConfigureUpgrade()
    {
        List<UpgradeType> upgradeTypes = new List<UpgradeType>();
        int upgradeTypeCount = System.Enum.GetValues(typeof(UpgradeType)).Length;

        for (int i = 0; i < buttons.Count; i++)
        {
            TextMeshProUGUI buttonText = buttons[i].GetComponentInChildren<TextMeshProUGUI>();
            int randomValue = Random.Range(1, 6);
            int randomUpgrade = Random.Range(0, upgradeTypeCount);

            UpgradeType upgradeType = (UpgradeType)randomUpgrade;
            if (!IsUpgradeAvailable(upgradeType) || upgradeTypes.Contains(upgradeType))
            {
                i--;
                continue;
            }

            upgradeTypes.Add(upgradeType);
            buttonText.text = $"{upgradeType} +{randomValue}";
        }
    }

    private bool IsUpgradeAvailable(UpgradeType upgradeType)
    {
        switch (upgradeType)
        {
            case UpgradeType.CottonCandyDamage:
                return player.HasGun(WeaponTypes.Ranged);
            case UpgradeType.DeBoinkDamage:
                return player.HasGun(WeaponTypes.Melee);
            case UpgradeType.Health:
                return true;
            case UpgradeType.Speed:
                return true;
            default:
                return false;
        }
    }

    public void OnClickUpgradeButton()
    {
        GameObject clickedObject = EventSystem.current.currentSelectedGameObject;
        TextMeshProUGUI buttonText = clickedObject.GetComponentInChildren<TextMeshProUGUI>();
        string[] buttonTextArray = buttonText.text.Split(' ');
        UpgradeType upgradeType = (UpgradeType)System.Enum.Parse(typeof(UpgradeType), buttonTextArray[0]);
        int upgradeValue = int.Parse(buttonTextArray[1].Substring(1));

        FindObjectOfType<PlayerController>().Upgrade(upgradeType, upgradeValue);
        GameManager.Instance.ContinueGame();
    }
}
