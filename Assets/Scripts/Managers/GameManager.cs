using UnityEngine;

public class GameManager : MonoBehaviour
{
    [SerializeField] private int count = 5;

    private void Start()
    {
        Application.targetFrameRate = 60;
    }

    private void Update()
    {

    }
}
