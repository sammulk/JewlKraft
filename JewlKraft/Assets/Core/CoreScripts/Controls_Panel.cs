using UnityEngine;
using UnityEngine.UI;

public class Controls_Panel : MonoBehaviour
{
    [SerializeField]
    private Button ExitButton;
    [SerializeField]
    private GameObject ESCPanel;
    [SerializeField]
    private Button SettingsButton;
    [SerializeField]
    private GameObject SettingsPanel;

    void Start()
    {
        ExitButton.onClick.AddListener(ClosePanel);
        SettingsButton.onClick.AddListener(OpenSettingsPanel);
        this.gameObject.SetActive(false);
    }

    private void Update()
    {
        if (this.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            ClosePanel();
        }
    }

    void ClosePanel()
    {
        this.gameObject.SetActive(false);
        if (ESCPanel != null)
        {
            ESCPanel.SetActive(true);
        }
    }

    void OpenSettingsPanel()
    {
        if (SettingsPanel != null)
        {
            SettingsPanel.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
