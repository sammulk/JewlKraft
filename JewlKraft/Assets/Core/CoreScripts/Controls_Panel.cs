using UnityEngine;
using UnityEngine.UI;

public class Controls_Panel : MonoBehaviour
{
    [SerializeField]
    private Button ExitButton;
    [SerializeField]
    private ESC_Panel ESCPanel;           // reference to the ESC_Panel component
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
            ESCPanel.OpenPanel();
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
