using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using Random = UnityEngine.Random;

public class BallBehavior : MonoBehaviour
{
    public Vector3 direction;
    public float speed;
    public float angle;

    private SpriteRenderer _spriteRenderer;
    // Start is called before the first frame update
    void OnEnable()
    {
        _spriteRenderer = GetComponent<SpriteRenderer>();
        speed = Random.Range(0.1f, 2f);
    }

    // Update is called once per frame
    private void FixedUpdate()
    {
        
        transform.position = transform.position + direction * (Time.fixedDeltaTime * speed);

        if (transform.position.x is < -9f or > 9)
        { 
            direction.x *= -1;
        }

        if (transform.position.y is < -4 or > 4)
        {
            direction.y *= -1;
        }
    }

    public void ChangeColor(Color color)
    {
        _spriteRenderer.color = color;
    }
}
