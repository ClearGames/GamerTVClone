using System.Collections;
using System.Collections.Generic;
using System.Security.Cryptography;
using UnityEngine;
using UnityEngine.UIElements;

public class PlayerController : MonoBehaviour
{
    float x;
    float y;

    public Vector3 limitMax; // public 선언 -> unity inspector에서 보인다
    public Vector3 limitMin;
    Vector3 temp;

    public GameObject[] prefabBullet;
    //public GameObject prefabBullet;

    float time;
    public float speed;

    float fireDelay;
    Animator animator;
    bool onDead;

    // 아이템
    public int Damage;
    public int Bomb;

    // 폭탄
    public GameObject BombMissile;
    public int BombPosY;
    public int BombDamage;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        fireDelay = 0;
        speed = 10.0f;

        animator = GetComponent<Animator>();
        onDead = false;

        Damage = 1;
        Bomb = 0;

        BombPosY = -30;
        BombDamage = 30;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FireBullet();
        OnDeadCheck();
        FireBomb();
    }

    public void Move()
    {
        float x = Input.GetAxis("Horizontal") * speed * Time.deltaTime;
        float y = Input.GetAxis("Vertical") * speed * Time.deltaTime;
        transform.Translate(new Vector3(x, y, 0));

        if (transform.position.x > limitMax.x)
        {
            temp.x = limitMax.x;
            temp.y = transform.position.y; // needed
            transform.position = temp;
        }
        if (transform.position.x < limitMin.x)
        {
            temp.x = limitMin.x;
            temp.y = transform.position.y;
            transform.position = temp;
        }
        if (transform.position.y > limitMax.y)
        {
            temp.x = transform.position.x;
            temp.y = limitMax.y;
            transform.position = temp;
        }
        if (transform.position.y < limitMin.y)
        {
            temp.x = transform.position.x;
            temp.y = limitMin.y;
            transform.position = temp;
        }
    }

    public void FireBullet()
    {
        fireDelay += Time.deltaTime;
        //Debug.Log("Fire" + fireDelay);
        if(fireDelay > 0.3f)
        {
            //Instantiate(prefabBullet, transform.position, Quaternion.identity); // 자기 위치에서 총알 생성
            Instantiate(prefabBullet[Damage - 1], transform.position, Quaternion.identity); // 자기 위치에서 총알 생성
            fireDelay -= 0.3f;
        }
    }
    public void FireBomb()
    {
        if (Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space!!");
            if(Bomb >= 1)
            {
                GameObject go = Instantiate(BombMissile, transform.position, Quaternion.identity);
                go.transform.position = new Vector3(transform.position.x, BombPosY, transform.position.z);
                //go.transform.position = new Vector2(transform.position.x, BombPosY);
                --Bomb;
                UIManager.instance.BombCheck(Bomb);
            }
        }    
    }
    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(limitMin, new Vector2(limitMax.x, limitMin.y));
        Gizmos.DrawLine(limitMin, new Vector2(limitMin.x, limitMax.y));
        Gizmos.DrawLine(limitMax, new Vector2(limitMax.x, limitMin.y));
        Gizmos.DrawLine(limitMax, new Vector2(limitMin.x, limitMax.y));
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("enemyBullet"))
        {
            animator.SetInteger("State", 1);
            onDead = true;
        }
    }

    private void OnDeadCheck()
    {
        if (onDead)
        {
            time += Time.deltaTime;
            //Debug.Log("time after destroyed" + time);
            if(SoundManager.instance.playerDeadSound.isPlaying == false)
                SoundManager.instance.playerDeadSound.Play();
        }
        if(time > 0.6f)
        {
            Destroy(gameObject);
            GameManager.instance.PlayerLifeRemove();
            GameManager.instance.CreatePlayer();
            UIManager.instance.LifeCheck(GameManager.instance.lifeCount);
        }
    }
}
