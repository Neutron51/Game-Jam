using UnityEngine;
using UnityEngine.InputSystem;

public class PlayerJump : MonoBehaviour
{
    private Rigidbody rb;
    public float jumpSpd = 7f;

    private Keyboard keyboard;

    private void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }
}
