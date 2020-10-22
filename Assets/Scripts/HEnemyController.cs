using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HEnemyController : MonoBehaviour
{
    public bool Blue = true;
    public int distance = 10;
    public int x;
    void Update()
    {
        float EnemyA = gameObject.transform.position.x;
        float EnemyB = gameObject.transform.position.y;
        if (Blue == true)
        {
            transform.position = new Vector2(Mathf.PingPong(Time.time * 2, distance) + x, EnemyB); // -1 indicates where it starts on the y-coordinate, Time.time * 2 indicates speed of object
        }
        else 
        {
            transform.position = new Vector2(Mathf.PingPong(Time.time * 2, distance) - x, EnemyB); //moves in opposite direction previous line
        }
    }
}
