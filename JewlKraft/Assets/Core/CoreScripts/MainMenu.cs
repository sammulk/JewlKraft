using UnityEditor.SearchService;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MainMenu : MonoBehaviour
{
    [SerializeField]
    private Button PlayButton;
    [SerializeField]
    private Button ExitButton;
    [SerializeField]
    private Button SettingsButton;

    [SerializeField]
    private GameObject SettingsPanel;

    private void Start()
    {
        if (PlayButton != null)
        {
            PlayButton.onClick.AddListener(OnPlayButtonClicked);
        }
        if (ExitButton != null)
        {
            ExitButton.onClick.AddListener(OnExitButtonClicked);
        }
        if (SettingsButton != null)
        {
            SettingsButton.onClick.AddListener(OnSettingsButtonClicked);
        }
    }

    private void OnPlayButtonClicked()
    {
        SceneManager.LoadScene("Shop_scene");
        Time.timeScale = 1f;
    }

    private void OnExitButtonClicked()
    {
        Application.Quit();
    }

    private void OnSettingsButtonClicked()
    {
        if (SettingsPanel != null)
        {
            SettingsPanel.SetActive(true);
        }
    }
}
