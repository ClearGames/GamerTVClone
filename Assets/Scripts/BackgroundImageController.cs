using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Scripting.APIUpdating;

public class BackgroundImageController : MonoBehaviour
{
    float height;
    float speed;
    //BoxCollider2D collider2D;
    BoxCollider2D boxCollider2D;

    // Start is called before the first frame update
    void Start()
    {
        boxCollider2D = GetComponent<BoxCollider2D>();
        height = boxCollider2D.size.y;
        speed = 3.0f;
    }

    // Update is called once per frame
    void Update()
    {
        Move();
        if (transform.position.y <= -height) Reposition();
    }

    void Move()
    {
        transform.Translate(Vector3.down * speed * Time.deltaTime);
    }

    void Reposition()
    {
        Vector3 offset = new Vector3(0, height * 2, 0);
        transform.position = transform.position + offset;
    }
}
