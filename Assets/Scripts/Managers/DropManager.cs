using UnityEngine;

public class DropManager : MonoBehaviour
{
    [SerializeField] private Candy candyPrefab;

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
        if (Random.Range(0, 100) < 50)
            DropCash(vector);
        else
            DropCandy(vector);
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
