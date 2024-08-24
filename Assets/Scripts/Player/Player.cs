using UnityEngine;

[RequireComponent(typeof(PlayerHealth))]
[RequireComponent(typeof(PlayerMovenment))]
[RequireComponent(typeof(PlayerController))]
public class Player : MonoBehaviour
{
    private float CandyCount { get; set; }
    private float CashCount { get; set; }


    public void AddCandy(float count = 1)
    {
        CandyCount += count;
        UIManager.Instance.UpdateCandyText(CandyCount);
    }


    public void AddCash(float count = 1)
    {
        CashCount += count;
        UIManager.Instance.UpdateCashText(CashCount);
    }
}
