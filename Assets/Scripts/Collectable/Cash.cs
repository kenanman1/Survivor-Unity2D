using UnityEngine;

public class Cash : Collectable
{
    public override void ReleaseCollectableToPool()
    {
        if (gameObject.activeSelf)
        {
            gameObject.SetActive(false);
            CashPool.Instance.cashPool.Release(this);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isCollected)
        {
            isCollected = true;
            LeanTween.cancel(gameObject);
            isMoving = false;
            Player player = collision.GetComponent<Player>();
            player.AddCash();
            ReleaseCollectableToPool();
        }
    }
}
