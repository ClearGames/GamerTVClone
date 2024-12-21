using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour
{
    Animator animator;
    bool onDead;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        onDead = false;
        time = 0.0f;
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
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("bullet"))
        {
            //Destroy(gameObject);
            animator.SetInteger("State", 1);
            OnDead();
        }
    }

    private void OnDead()
    {
        onDead = true;
    }
}
