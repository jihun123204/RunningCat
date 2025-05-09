using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuruSpawner : MonoBehaviour
{
    public GameObject churuPrefab;      // �� ������
    public float spawnInterval = 3f;    // �� ���� ����
    public float spawnRangeX = 8f;     // X�� ���� ���� (����, ������)
    public float churuSpawnChance = 0.1f;  // �򸣰� ������ Ȯ�� (0.1f = 10%)

    void Start()
    {
        // ���� �ð����� �� ���� (Ȯ���� �ݿ��Ͽ�)
        InvokeRepeating(nameof(SpawnChuru), 2f, spawnInterval);
    }

    void SpawnChuru()
    {
        // Ȯ�������� ���� ���� ����
        if (Random.value <= churuSpawnChance)
        {
            // �򸣰� ������ ��ġ�� ����
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 spawnPos = new Vector3(randomX, 0f, 0f);

            // �� ������Ʈ ����
            Instantiate(churuPrefab, spawnPos, Quaternion.identity);
        }
    }
}
