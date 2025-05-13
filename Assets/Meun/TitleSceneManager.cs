using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;
using System.Collections;


public class TitleSceneManager : MonoBehaviour
{
    public GameObject pressAnyKeyUI;      // "아무 키나 누르세요" 문구
    public float delayBeforeInput = 2f;   // 로딩 연출 시간
    private bool canAcceptInput = false;

    void Start()
    {
        pressAnyKeyUI.SetActive(false);
        StartCoroutine(ShowPromptAfterDelay());
    }

    IEnumerator ShowPromptAfterDelay()
    {
        yield return new WaitForSeconds(delayBeforeInput);
        pressAnyKeyUI.SetActive(true);
        canAcceptInput = true;
    }

    void Update()
    {
        if (canAcceptInput && Input.anyKeyDown)
        {
            LoadMainScene();
        }
    }

    void LoadMainScene()
    {
        SceneManager.LoadScene("MainScene"); // ✅ 메인 씬 이름에 맞게 조정
    }
}
