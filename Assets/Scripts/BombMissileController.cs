using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BombMissleController : MonoBehaviour
{
    public float speed;
    float time;

    // Start is called before the first frame update
    void Start()
    {
        speed = 35.0f;
        time = 0;
    }

    // Update is called once per frame
    void Update()
    {
        MoveBomb();
        DestroyBomb();
    }

    private void MoveBomb()
    {
        transform.Translate(Vector3.up * speed * Time.deltaTime);
    }

    private void DestroyBomb()
    {
        time += Time.deltaTime;
        if(time > 3.0f)
        {
            Destroy(gameObject);
        }
    }
}
