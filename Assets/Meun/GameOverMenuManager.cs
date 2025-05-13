using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class GameOverMenuManager : MonoBehaviour
{
    public Button retryButton;
    public Button quitButton;

    private void Start()
    {
        if (retryButton != null)
            retryButton.onClick.AddListener(RestartGame);

        if (quitButton != null)
            quitButton.onClick.AddListener(ReturnToMainScene);
    }

    void RestartGame()
    {
        SceneManager.LoadScene("GameScene"); // ✅ 다시 도전
    }

    void ReturnToMainScene()
    {
        SceneManager.LoadScene("MainScene"); // ✅ 메인화면으로
    }
}
