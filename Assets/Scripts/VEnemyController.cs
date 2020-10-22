using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class VEnemyController : MonoBehaviour
{
    public bool Red = true;
    public int distance = 10;
    public int y;
    void Update()
    {
        float EnemyA = gameObject.transform.position.x;
        float EnemyB = gameObject.transform.position.y;
        if (Red == true)
        {
            transform.position = new Vector2(EnemyA, Mathf.PingPong(Time.time * 2, distance) - y); // -1 indicates where it starts on the y-coordinate, Time.time * 2 indicates speed of object
        }
        else 
        {
            transform.position = new Vector2(EnemyA, (2 - Mathf.PingPong(Time.time * 2, distance)) - y);
        }
    }
}
