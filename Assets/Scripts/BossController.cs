using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class BossController : MonoBehaviour
{
    // 
    GameObject player;
    PlayerController playerController;
    // 체력바
    public float hpGreen; //초록색
    public float hpRed; //초록색
    //
    Animator animator;
    //
    bool onDead;
    bool isSpawn;
    // 점수
    int score;
    
    float time;
    float speed;
    Transform spawnMovePos;

    // 총알 쏘는 위치
    public Transform LAttackPos;
    public Transform RAttackPos;
    // 총알
    public GameObject bossBullet;
    // 총알 딜레이
    float fireDelay;

    // 애니메이션 상태 확인용
    // -1 : 대기, 이동 반복
    // 0 : 대기, 이동
    // 1 : L공격
    // 2 : R공격
    // 3 : Die
    int animNumber;

    // 피격관련
    public SpriteRenderer spriteRenderer;
    Color currentColor;

    private void Awake()
    {
        hpGreen = 150.0f;
        hpRed = 150.0f;
    }

    // Start is called before the first frame update
    void Start()
    {
        spawnMovePos = GameObject.Find("BossSpawn").GetComponent<Transform>();
        animator = GetComponent<Animator>();

        onDead = false;
        isSpawn = true;

        score = 1000;
        speed = 10;

        animNumber = 0;
        currentColor = spriteRenderer.color;
    }

    // Update is called once per frame
    void Update()
    {
        if (isSpawn)
        {
            BossSpawn();
        }
        if (onDead) time += Time.deltaTime;
        if (time > 0.6f) Destroy(gameObject);

        if(player == null && GameManager.instance.lifeCount >= 0)
        {
            PlayerFind();
        }
        FireBullet();
        AnimationSystem();
    }

    public void PlayerFind()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        playerController = player.GetComponent<PlayerController>();
    }

    // 애니메이션 상태 확인용
    // -1 : 대기, 이동 반복
    // 0 : 대기, 이동
    // 1 : L공격
    // 2 : R공격
    // 3 : Die
    void FireBullet()
    {
        // 총알 발사 애니메이션
        if (hpGreen > 0 && isSpawn == false)
        {
            fireDelay += Time.deltaTime;
            // 공격딜레이가 1.0초 지나고 L공격 상태가 아니면
            if (fireDelay > 1.0f && animNumber != 1)
            {
                // L공격
                animNumber = 1;
                fireDelay = 0; //fireDelay -= fireDelay;
            }
        }
        if(hpGreen >= 0)
        {
            fireDelay += Time.deltaTime;
            if(fireDelay > 1.0f && animNumber != 2)
            {
                // R공격
                animNumber = 2;
                fireDelay = 0;
            }
        }
    }

    //  애니메이션은 따로 관리
    void AnimationSystem()
    {
        if (animNumber == 0)
        {
            StartCoroutine(CoIdle());
            //StartCoroutine("CoIdle");
        }
        if (animNumber == 1)
        {
            StartCoroutine(CoLAttack());
        }
        if(animNumber == 2)
        {
            StartCoroutine(CoRAttack());
        }
    }

    //public IEnumerator CoIdle()
    IEnumerator CoIdle()
    {
        animNumber = -1;
        animator.SetTrigger("Idle");
        yield return new WaitForSeconds(0.6f);
    }
    IEnumerator CoLAttack()
    {
        animNumber = -1;
        animator.SetTrigger("LAttack");
        yield return new WaitForSeconds(0.6f);
    }
    IEnumerator CoRAttack()
    {
        animNumber = -1;
        animator.SetTrigger("RAttack");
        yield return new WaitForSeconds(0.6f);
        animator.SetTrigger("RAttack");
        yield return new WaitForSeconds(0.6f);
        animator.SetTrigger("RAttack");
        yield return new WaitForSeconds(0.6f);
        animNumber = 0;
    }

    void LAttack()
    {
        if (player == null) return;
        Instantiate(bossBullet, LAttackPos.position, Quaternion.identity);
        fireDelay -= 1f;
    }
    void RAttack()
    {
        if (player == null) return;
        Instantiate(bossBullet, RAttackPos.position, Quaternion.identity);
        fireDelay -= 1f;
    }

    void OnDead()
    {
        onDead = true;
        if(gameObject.tag != "untagged")
        {
            // 스코어 증가 코드 작성
            UIManager.instance.ScoreAdd(score);
            SoundManager.instance.enemyDeadSound.Play();
        }
        gameObject.tag = "Untagged";
    }

    void BossSpawn()
    {
        transform.position = Vector3.MoveTowards(transform.position, spawnMovePos.position, Time.deltaTime * speed);
        if(transform.position == spawnMovePos.position)
        {
            isSpawn = false;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            if(hpGreen > 0) hpGreen -= playerController.Damage;
            else            hpRed -= playerController.Damage;
            StartCoroutine(OnDamagedEffect());
        }
        if (collision.CompareTag("bombMissile"))
        {
            if (hpGreen > 0) hpGreen -= playerController.BombDamage;
            else             hpRed -= playerController.Damage;
            StartCoroutine(OnDamagedEffect());
        }
        if (hpRed <= 0)
        {
            animator.SetTrigger("Die");
            OnDead();
        }
    }

    IEnumerator OnDamagedEffect()
    {
        spriteRenderer.color = Color.red;
        yield return new WaitForSeconds(0.2f);
        spriteRenderer.color = currentColor;
    }
}
