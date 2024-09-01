using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField] private Candy candyPrefab;
    [SerializeField] private float candyDropChance = 70f;

    private void Awake()
    {
        Enemy.onDie += Drop;
    }

    private void OnDestroy()
    {
        Enemy.onDie -= Drop;
    }

    private void Drop(Vector2 vector)
    {
        if (Random.Range(0, 100) <= candyDropChance)
            DropCandy(vector);
        else
            DropCash(vector);
    }

    private void DropCandy(Vector2 vector)
    {
        Candy candy = CandyPool.Instance.candyPool.Get();
        candy.transform.position = vector;
    }

    private void DropCash(Vector2 vector)
    {
        Cash cash = CashPool.Instance.cashPool.Get();
        cash.transform.position = vector;
    }
}
