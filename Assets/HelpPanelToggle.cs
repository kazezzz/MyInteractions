using UnityEngine;

public class HelpPanelToggle : MonoBehaviour
{
    public GameObject helpPanel;

    void Start()
    {
        // 运行时先隐藏说明面板。
        helpPanel.SetActive(false);
    }

    public void TogglePanel()
    {
        // 每点击一次，切换显示或隐藏。
        helpPanel.SetActive(!helpPanel.activeSelf);
    }
}
