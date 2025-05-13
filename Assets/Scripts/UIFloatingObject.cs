using UnityEngine;

public class UIFloatingObject : MonoBehaviour
{
    public float floatSpeed = 1f;
    public float floatHeight = 20f;  // UI는 픽셀 단위로 더 큼

    private RectTransform rectTransform;
    private Vector2 startAnchoredPos;

    void Start()
    {
        rectTransform = GetComponent<RectTransform>();
        startAnchoredPos = rectTransform.anchoredPosition;
    }

    void Update()
    {
        float offsetY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        rectTransform.anchoredPosition = startAnchoredPos + new Vector2(0f, offsetY);
    }
}
