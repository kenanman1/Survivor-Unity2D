using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovenment : MonoBehaviour
{
    [Header("Player Settings")]
    public float speed = 10f;

    private Rigidbody2D rb;
    private bool isDead = false;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        PlayerController.OnPlayerDeathEvent += OnDead;
    }

    public void OnMove(InputValue value)
    {
        if (isDead)
            return;

        Vector2 keyboardInput = value.Get<Vector2>();

        if (keyboardInput.x > 0.5)
            keyboardInput.x = 1;
        else if (keyboardInput.x < -0.5)
            keyboardInput.x = -1;

        if (keyboardInput.y > 0.5)
            keyboardInput.y = 1;
        else if (keyboardInput.y < -0.5)
            keyboardInput.y = -1;

        rb.velocity = new Vector2(keyboardInput.x * speed, keyboardInput.y * speed);
    }

    private void OnDead()
    {
        isDead = true;
    }
}
