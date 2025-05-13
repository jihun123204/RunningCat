using UnityEngine;

public class FloatingObject : MonoBehaviour
{
    public float floatSpeed = 1f;        // 진동 속도
    public float floatHeight = 0.5f;     // 진폭 (얼마나 위아래로 움직일지)

    private Vector3 startPosition;

    void Start()
    {
        startPosition = transform.position; // ✅ 월드 기준 위치
    }

    void Update()
    {
        float offsetY = Mathf.Sin(Time.time * floatSpeed) * floatHeight;
        Vector3 newPos = new Vector3(startPosition.x, startPosition.y + offsetY, startPosition.z);
        transform.position = newPos;
    }
}
