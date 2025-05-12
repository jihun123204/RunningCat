using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public Transform player;           // �÷��̾� ��ġ
    public float spawnInterval = 2f;
    public float offsetMin = 2f;       // �ּ� �Ÿ�
    public float offsetMax = 4f;       // �ִ� �Ÿ�
    public float spawnY = 0f;          // Y ��ġ ����

    void Start()
    {
        InvokeRepeating(nameof(SpawnFish), 1f, spawnInterval);
    }

    void SpawnFish()
    {
        if (player == null) return;

        // �÷��̾� �������� ������ �Ǵ� ���� ���� ����
        float playerDir = player.localScale.x >= 0 ? 1f : -1f;

        // �÷��̾� ���� X ��ġ ���
        float offset = Random.Range(offsetMin, offsetMax);
        float spawnX = player.position.x + offset * playerDir;

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);
        Instantiate(fishPrefab, spawnPos, Quaternion.identity);
    }
}
