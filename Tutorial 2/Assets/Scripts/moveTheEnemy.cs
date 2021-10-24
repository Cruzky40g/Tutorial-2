using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class moveTheEnemy : MonoBehaviour
{
    public Vector2 endPos;
    private Vector2 startPos;

    private void Start()
    {
        startPos = transform.position;
    }
    void Update()
    {
        transform.position = Vector3.Lerp(startPos, endPos, Mathf.PingPong(Time.time, 1));
    }
}
