using UnityEngine;
using UnityEngine.InputSystem;

[RequireComponent(typeof(Rigidbody))]
public class BallJump : MonoBehaviour
{
    public float jumpForce = 5f;
    private Rigidbody rb;

    void Awake()
    {
        rb = GetComponent<Rigidbody>();
    }

    void Update()
    {
        if (Keyboard.current == null)
            return;

        // 检查球下方是否有地面。
        bool nearGround = Physics.Raycast(
            transform.position,
            Vector3.down,
            0.6f
        );

        // 等球落稳后才能再次跳跃。
        bool isStill = Mathf.Abs(rb.linearVelocity.y) < 0.1f;

        if (Keyboard.current.spaceKey.wasPressedThisFrame &&
            nearGround && isStill)
        {
            rb.AddForce(Vector3.up * jumpForce, ForceMode.Impulse);
        }
    }
}
