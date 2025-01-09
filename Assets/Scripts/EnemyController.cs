using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    public GameObject enemyBullet;
    GameObject player;
    PlayerController playerController;
    float fireDelay;

    Animator animator;
    bool onDead;
    float time;
    // 이동관련
    float moveSpeed;
    Rigidbody2D rg2D;
    // 아이템
    public GameObject[] items;
    // HP
    int hp;
    // 태그 임시 저장
    public string tagName;
    // 점수 저장
    int score;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
        
        onDead = false;
        time = 0.0f;
        
        // 이동관련
        rg2D = GetComponent<Rigidbody2D>();
        moveSpeed = Random.Range(5.0f, 7.0f);
        fireDelay = 2.5f;

        if (gameObject.CompareTag("itemDropEnemy"))
            hp = 3;
        else
            hp = 1;

        score = 10;
        tagName = gameObject.tag;
        Move();
    }

    public void FireBullet()
    {
        if (player == null) return;

        fireDelay += Time.deltaTime;
        if(fireDelay > 3f)
        {
            /*
             총알 생성
             transform.position : 현재 위치에서
             Quaternion.identity : 회전 없이
            */
            Instantiate(enemyBullet, transform.position, Quaternion.identity);
            fireDelay -= 3f;
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (onDead)
        {
            time += Time.deltaTime;
        }
        if (time > 0.6f)
        {
            Destroy(gameObject);
            //if (gameObject.CompareTag("itemDropEnemy"))
            if (tagName == "itemDropEnemy")
            {
                int temp = Random.Range(0, 2);
                Instantiate(items[temp], transform.position, Quaternion.identity);
            }
        }
        FireBullet();
        //Move();
    }

    private void Move()
    {
        if (player == null) return;
        Vector3 distance = player.transform.position - transform.position;
        Vector3 dir = distance.normalized;
        rg2D.velocity = dir * moveSpeed;
    }
    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            hp -= playerController.Damage;
        }
        if (collision.CompareTag("bombMissile"))
        {
            hp -= playerController.BombDamage;
        }
        if (hp <= 0)
        {
            //Destroy(gameObject);
            animator.SetInteger("State", 1);
            OnDead();
        }
        
        if (collision.CompareTag("blockCollider"))
        {
            OnDisappear();
        }
    }

    private void OnDead()
    {
        onDead = true;
        if(gameObject.tag != "Untagged")
        {
            // 스코어 증가 코드 작성
            UIManager.instance.ScoreAdd(score);
            SoundManager.instance.enemyDeadSound.Play();
        }
        gameObject.tag = "Untagged";
    }

    private void OnDisappear()
    {
        Destroy(gameObject);
    }
}
