using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BulletController : MonoBehaviour
{
    public float speed;
    float time;
    // Start is called before the first frame update
    private void Start()
    {
        speed = 30.0f;
        time = 0;
    }

    // Update is called once per frame
    private void Update()
    {
        FireBullet();
        DestroyBulltet();
    }

    private void FireBullet()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void DestroyBulltet()
    {
        time += Time.deltaTime;
        if (time > 5.0f)
        {
            Destroy(gameObject); // gameObject : 자기 자신
        }
    }

    // 게임오브젝트의 콜라이더가 isTrigger이고 다른 콜라이더와 충돌하는 순간 호출
    private void OnTriggerEnter2D(Collider2D collision)
    {
        // 적과 collision 이 발생했을 때만 실행되게 하기 위한 if문
        if (collision.CompareTag("enemy"))
        {
            Destroy(gameObject);
        }
        if (collision.CompareTag("itemDropEnemy"))
        {
            Destroy(gameObject);
        }
    }
}
