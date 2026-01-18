using UnityEngine;
using UnityEngine.UI;

public class SettingsPanel : MonoBehaviour
{
    [SerializeField]
    private Button ExitSettingsButton;
    [SerializeField]
    private ESC_Panel ESCPanel;           // reference to the ESC_Panel component
    [SerializeField] 
    private Button ControlsButton;
    [SerializeField]
    private GameObject ControlsPanel;
    [SerializeField]
    private Button HelpButton;
    [SerializeField]
    private GameObject HelpPanel;

    void Start()
    {
        ExitSettingsButton.onClick.AddListener(CloseSettingsPanel);
        ControlsButton.onClick.AddListener(OpenControlsPanel);
        HelpButton.onClick.AddListener(OpenHelpPanel);
        this.gameObject.SetActive(false);
    }

    void Update()
    {
        // If settings panel is open and the user presses Escape, close settings and re-open ESC panel via API.
        if (this.gameObject.activeSelf && Input.GetKeyDown(KeyCode.Escape))
        {
            CloseSettingsPanel();
        }
    }

    void CloseSettingsPanel()
    {
        this.gameObject.SetActive(false);
        if (ESCPanel != null)
        {
            ESCPanel.OpenPanel();
        }
    }

    void OpenControlsPanel()
    {
        if (ControlsPanel != null)
        {
            ControlsPanel.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }

    void OpenHelpPanel()
    {
        if (HelpPanel != null)
        {
            HelpPanel.SetActive(true);
            this.gameObject.SetActive(false);
        }
    }
}
