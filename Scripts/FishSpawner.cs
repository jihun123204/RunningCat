using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FishSpawner : MonoBehaviour
{
    public GameObject fishPrefab;
    public Transform player;           // 플레이어 위치
    public float spawnInterval = 2f;
    public float offsetMin = 2f;       // 최소 거리
    public float offsetMax = 4f;       // 최대 거리
    public float spawnY = 0f;          // Y 위치 고정

    void Start()
    {
        InvokeRepeating(nameof(SpawnFish), 1f, spawnInterval);
    }

    void SpawnFish()
    {
        if (player == null) return;

        // 플레이어 기준으로 오른쪽 또는 왼쪽 방향 감지
        float playerDir = player.localScale.x >= 0 ? 1f : -1f;

        // 플레이어 앞쪽 X 위치 계산
        float offset = Random.Range(offsetMin, offsetMax);
        float spawnX = player.position.x + offset * playerDir;

        Vector3 spawnPos = new Vector3(spawnX, spawnY, 0f);
        Instantiate(fishPrefab, spawnPos, Quaternion.identity);
    }
}
