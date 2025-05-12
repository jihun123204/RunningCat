using UnityEngine;

public class AreaBGMTrigger : MonoBehaviour
{
    public AudioClip areaBGM;

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (other.CompareTag("Player") && areaBGM != null)
        {
            SoundManager.Instance.PlayAreaBGM(areaBGM);
        }
    }
}
