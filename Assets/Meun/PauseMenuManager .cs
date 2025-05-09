using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class PauseMenuManager : MonoBehaviour
{
    public GameObject menuPanel;
    public Button continueButton;
    public Button settingsButton;
    public Button quitButton;
    public Button retryButton; // ✅ 다시하기 버튼

    private bool isMenuOpen = false;

    private void Start()
    {
        menuPanel.SetActive(false);

        continueButton.onClick.AddListener(ResumeGame);
        settingsButton.onClick.AddListener(OpenSettings);
        quitButton.onClick.AddListener(ReturnToMainScene); // ✅ 종료 → 메인씬으로
        retryButton.onClick.AddListener(RestartGame);       // ✅ 다시하기 추가
    }

    private void Update()
    {
        if (Input.GetKeyDown(KeyCode.Escape))
        {
            if (!isMenuOpen)
                OpenMenu();
            else
                ResumeGame();
        }
    }

    void OpenMenu()
    {
        Time.timeScale = 0f;
        menuPanel.SetActive(true);
        isMenuOpen = true;
    }

    public void ResumeGame()
    {
        Time.timeScale = 1f;
        menuPanel.SetActive(false);
        isMenuOpen = false;
    }

    void OpenSettings()
    {
        menuPanel.SetActive(false);
        isMenuOpen = false;
        Time.timeScale = 1f;

        SettingsMenuManager settings = FindObjectOfType<SettingsMenuManager>();
        if (settings != null)
            settings.OpenSettingsPanel();
    }

    void ReturnToMainScene()
    {
        Time.timeScale = 1f;
        SceneManager.LoadScene("MainScene"); // ✅ 메인 씬 이름으로 변경
    }

    void RestartGame()
    {
        Time.timeScale = 1f;
        Scene currentScene = SceneManager.GetActiveScene();
        SceneManager.LoadScene(currentScene.buildIndex); // ✅ 현재 씬 다시 로드
    }
}
