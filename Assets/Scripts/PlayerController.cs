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

    public GameObject prefabBullet;
    float time;
    public float speed;

    // Start is called before the first frame update
    void Start()
    {
        time = 0;
        speed = 10.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        FireBullet();
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
        time += Time.deltaTime;
        Debug.Log("Fire" + time);
        if(time > 0.3f)
        {
            Instantiate(prefabBullet, transform.position, Quaternion.identity); // 자기 위치에서 총알 생성
            time -= 0.3f;
            //time = 0; // 위 코드가 권장되는 형태
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
}
