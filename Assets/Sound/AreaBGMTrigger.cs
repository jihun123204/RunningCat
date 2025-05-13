using UnityEngine;
using UnityEngine.SceneManagement;

public class AreaBGMTrigger : MonoBehaviour
{
    public AudioClip areaBGM;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (!other.CompareTag("Player") || areaBGM == null)
            return;

        // ✅ GameOver 씬일 경우 지역 BGM 무시
        if (SceneManager.GetActiveScene().name == "GameOver")
            return;

        SoundManager.Instance.PlayAreaBGM(areaBGM);
    }
}
