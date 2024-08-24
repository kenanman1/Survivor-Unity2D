using UnityEngine;

public class Candy : Collectable
{
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.tag == "Player" && !isCollected)
        {
            isCollected = true;
            LeanTween.cancel(gameObject);
            isMoving = false;
            Player player = collision.GetComponent<Player>();
            player.AddCandy();
            if (gameObject.activeSelf)
            {
                gameObject.SetActive(false);
                CandyPool.Instance.candyPool.Release(this);
            }
        }
    }
}
