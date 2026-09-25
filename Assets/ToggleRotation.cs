using UnityEngine;
using UnityEngine.InputSystem;

public class ToggleRotation : MonoBehaviour
{
    public float rotationSpeed = 90f;
    private bool isRotating;

    void Update()
    {
        // 每按一次数字 2，切换旋转状态。
        if (Keyboard.current != null &&
            Keyboard.current.digit2Key.wasPressedThisFrame)
        {
            isRotating = !isRotating;
        }

        // 开启时，绕自身的竖直轴旋转。
        if (isRotating)
        {
            transform.Rotate(
                0f,
                rotationSpeed * Time.deltaTime,
                0f
            );
        }
    }
}
