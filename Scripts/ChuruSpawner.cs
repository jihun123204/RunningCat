using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ChuruSpawner : MonoBehaviour
{
    public GameObject churuPrefab;      // 츄르 프리팹
    public float spawnInterval = 3f;    // 츄르 생성 간격
    public float spawnRangeX = 8f;     // X축 랜덤 범위 (왼쪽, 오른쪽)
    public float churuSpawnChance = 0.1f;  // 츄르가 생성될 확률 (0.1f = 10%)

    void Start()
    {
        // 일정 시간마다 츄르 생성 (확률을 반영하여)
        InvokeRepeating(nameof(SpawnChuru), 2f, spawnInterval);
    }

    void SpawnChuru()
    {
        // 확률적으로 생성 여부 결정
        if (Random.value <= churuSpawnChance)
        {
            // 츄르가 랜덤한 위치에 생성
            float randomX = Random.Range(-spawnRangeX, spawnRangeX);
            Vector3 spawnPos = new Vector3(randomX, 0f, 0f);

            // 츄르 오브젝트 생성
            Instantiate(churuPrefab, spawnPos, Quaternion.identity);
        }
    }
}
