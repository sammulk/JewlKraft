using UnityEngine;
using UnityEngine.UI;

public class ESC_Panel : MonoBehaviour
{ 
    [SerializeField]
    private Button Resume;
    [SerializeField] 
    private Button Settings;
    [SerializeField]
    private Button MainMenu;

    [SerializeField]
    private GameObject ESCPanel;
    [SerializeField]
    private GameObject SettingsPanel;

    public bool isOpen = false;

    private void Start()
    {
        Resume.onClick.AddListener(ResumeGame);
        MainMenu.onClick.AddListener(GoToMainMenu);
        Settings.onClick.AddListener(OpenSettings);
        if (ESCPanel != null)
            ESCPanel.SetActive(false);
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (Workstation.LastEscapeHandledFrame == Time.frameCount)
                return;

            if (Workstation.AnyOpen)
            {
                Workstation.CloseAllOpen();
                return;
            }

            ESC();
        }
    }

    private void GoToMainMenu()
    {
        ClosePanel();
        float FD = FadeController.Instance.fadeDuration;
        FadeController.Instance.fadeDuration = 0.1f;
        FadeController.Instance.FadeToScene("Main_menu");
        FadeController.Instance.fadeDuration = FD;
    }

    void ResumeGame()
    {
        if (isOpen)
        {
            ClosePanel();
            return;
        }
    }

    void OpenSettings()
    {
        if (SettingsPanel != null)
        {
            // Open settings panel from ESC: keep game paused but ESC panel is not 'open'
            SettingsPanel.SetActive(true);
            if (ESCPanel != null)
                ESCPanel.SetActive(false);
            isOpen = false;
            Time.timeScale = 0f;
        }
    }

    void ESC()
    {
        if (isOpen)
        {
            ClosePanel();
            return;
        }
        else
        {
            OpenPanel();
        }
    }

    // Public API so other panels can open/close the ESC panel correctly.
    public void OpenPanel()
    {
        if (ESCPanel != null)
            ESCPanel.SetActive(true);
        isOpen = true;
        Time.timeScale = 0f;
    }

    public void ClosePanel()
    {
        if (ESCPanel != null)
            ESCPanel.SetActive(false);
        isOpen = false;
        Time.timeScale = 1f;
    }
}
