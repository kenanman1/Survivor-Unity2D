using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerMovenment : MonoBehaviour
{
    [Header("Player Settings")]
    [SerializeField] private float speed = 10f;

    private Rigidbody2D rb;

    private void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    public void OnMove(InputValue value)
    {
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
}
