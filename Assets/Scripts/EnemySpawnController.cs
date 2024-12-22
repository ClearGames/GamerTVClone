using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemySpawnController : MonoBehaviour
{
    // 생성 위치
    public Transform[] enemySpawns;
    //public Transform[] enemySpawns = new Transform[9];
    // 적 프리팹
    public GameObject[] enemyGameObjects;
    // 시간을 재는 변수
    float time;
    // 적 생성 시간
    float respawnTime;
    // 적 생성 숫자
    int enemyCount;
    // 랜덤 숫자 변수를 저장하는 배열
    int[] randomCounts;
    // 웨이브 >> 추후 사용
    int wave;
    // 플레이어 변수
    GameObject player;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        respawnTime = 4.0f;
        enemyCount = 5;
        randomCounts = new int[enemyCount];
        wave = 0;
        player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        Timer();
    }

    void Timer()
    {
        time += Time.deltaTime;
        if(time > respawnTime)
        {
            RandomPos();
            EnemyCreate();
            wave++;
            time -= respawnTime;
            //time = 0;
            //time -= time;
        }
    }

    void RandomPos()
    {
        // 랜덤 위치를 위한 숫자
        for (int i = 0; i < enemyCount; ++i)
        {
            randomCounts[i] = Random.Range(0, 9);
        }
    }

    void EnemyCreate()
    {
        if (player == null) return;
        for(int i=0; i<enemyCount; ++i)
        {
            // 랜덤 적 선택
            int tmpCnt = Random.Range(0, enemyGameObjects.Length);
            // 생성
            GameObject tmp = GameObject.Instantiate(enemyGameObjects[tmpCnt]);
            // 위치
            tmp.transform.position = enemySpawns[randomCounts[i]].position;
            // 동일 위치를 방지하기 위한 조금의 위치 값 수정
            float tmpX = tmp.transform.position.x;
            float result = Random.Range(tmpX - 2.0f, tmpX + 2.0f);
            tmp.transform.position = new Vector3(result, tmp.transform.position.y, tmp.transform.position.z);
        }
    }
}
