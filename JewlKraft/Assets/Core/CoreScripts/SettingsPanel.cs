using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField]
    private Button ExitSettingsButton;
    [SerializeField]
    private GameObject ESCPanel;

    void Start()
    {
        ExitSettingsButton.onClick.AddListener(CloseSettingsPanel);
        this.gameObject.SetActive(false);
    }

    void CloseSettingsPanel()
    {
        this.gameObject.SetActive(false);
        if (ESCPanel != null)
        {
            ESCPanel.SetActive(true);
        }
    }
}
