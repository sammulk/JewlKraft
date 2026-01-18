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
        ESCPanel.SetActive(false);
        UnityEngine.SceneManagement.SceneManager.LoadScene("Main_menu");
    }

    void ResumeGame()
    {
        if (isOpen)
        {
            if (ESCPanel != null)
                ESCPanel.SetActive(false);
            isOpen = false;
            Time.timeScale = 1f;
            return;
        }
    }

    void OpenSettings()
    {
        if (SettingsPanel != null)
        {
            SettingsPanel.SetActive(true);
            ESCPanel.SetActive(false);
        }
    }

    void ESC()
    {
        if (isOpen)
        {
            if (ESCPanel != null)
                ESCPanel.SetActive(false);
            isOpen = false;
            Time.timeScale = 1f;
            return;
        }
        else
        {
            if (ESCPanel != null)
                ESCPanel.SetActive(true);
            isOpen = true;
            Time.timeScale = 0f;
        }
    }
}
