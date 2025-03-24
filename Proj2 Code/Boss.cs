using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Boss : MonoBehaviour
{
    public float moveSpeed = 2f;
    private int hitCount = 0;
    private bool movingRight = true;
    
    void Update()
    {
        if (Random.value < 0.01f) movingRight = !movingRight;
        transform.position += new Vector3((movingRight ? 1 : -1) * moveSpeed * Time.deltaTime, 0, 0);
    }
    
    void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.CompareTag("PlayerJump"))
        {
            hitCount++;
            if (hitCount >= 3)
                GameManager.instance.WinGame();
        }
    }
}
