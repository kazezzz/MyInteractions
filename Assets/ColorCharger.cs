using UnityEngine;
using UnityEngine.InputSystem;

public class ColorChanger : MonoBehaviour
{
    private Material cubeMaterial;
    private bool isBlue;

    void Start()
    {
        cubeMaterial = GetComponent<Renderer>().material;
        cubeMaterial.color = Color.red;
    }

    void Update()
    {
        if (Keyboard.current != null &&
            Keyboard.current.digit1Key.wasPressedThisFrame)
        {
            isBlue = !isBlue;
            cubeMaterial.color = isBlue ? Color.blue : Color.red;
        }
    }
}