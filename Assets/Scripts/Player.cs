using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UIElements;

public class Player : MonoBehaviour
{
    //Transform tr;
    float speed;

    public Vector3 limitMax; // public 선언 -> unity inspector에서 보인다
    public Vector3 limitMin;
    Vector3 temp;

    // Start is called before the first frame update
    void Start()
    {
        //tr = GetComponent<Transform>();
    }

    // Update is called once per frame
    void Update()
    {
        //tr.position = new Vector3(tr.position.x + 0.01f, 0, 0);
        //transform.position = new Vector3(transform.position.x + 0.01f, 0, 0);

        speed = 0.1f;
        float x = Input.GetAxis("Horizontal") * speed;
        float y = Input.GetAxis("Vertical") * speed;
        transform.Translate(new Vector3(x, y, 0));
        //temp.x = x; temp.y = y;
        //temp.Set(x, y, 0);
        //transform.Translate(tmp);

        //if (transform.position.x > limitMax.x) transform.position.Set(limitMax.x, transform.position.y, 0);

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

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.red;
        Gizmos.DrawLine(limitMin, new Vector2(limitMax.x, limitMin.y));
        Gizmos.DrawLine(limitMin, new Vector2(limitMin.x, limitMax.y));
        Gizmos.DrawLine(limitMax, new Vector2(limitMax.x, limitMin.y));
        Gizmos.DrawLine(limitMax, new Vector2(limitMin.x, limitMax.y));
    }
}
