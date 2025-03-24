using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Platform : MonoBehaviour
{
    public float moveSpeed = 2f;
    public float moveDistance = 3f;
    private Vector3 startPos;
    private bool movingRight = true;
    
    void Start() { startPos = transform.position; }
    
    void Update()
    {
        if (movingRight && transform.position.x > startPos.x + moveDistance ||
            !movingRight && transform.position.x < startPos.x - moveDistance)
        {
            movingRight = !movingRight;
        }
        transform.position += new Vector3((movingRight ? 1 : -1) * moveSpeed * Time.deltaTime, 0, 0);
    }
}
