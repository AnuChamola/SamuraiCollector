using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class FallingObject : MonoBehaviour
{
    [SerializeField] float minSpeed;
    [SerializeField] float maxSpeed;
    protected float speed;
    protected Rigidbody2D rb;
    protected SpriteRenderer sr;

    protected virtual void Awake()
    {
        rb = GetComponent<Rigidbody2D>();
        sr = GetComponent<SpriteRenderer>();
    }
    
    protected virtual void OnEnable()
    {
        Vector3 spawnPos = new Vector3();
        spawnPos.x = Random.Range(-GameManager.instance.bounds.extents.x + (sr.bounds.size.y * 0.5f), GameManager.instance.bounds.extents.x - (sr.bounds.size.y * 0.5f));
        spawnPos.y = GameManager.instance.bounds.extents.y;
        transform.position = spawnPos;
        speed = Random.Range(minSpeed, maxSpeed);
    }
    // Update is called once per frame
    protected virtual void LateUpdate()
    {
        rb.linearVelocity = Vector2.down * speed;
        if(rb.transform.position.y <= -GameManager.instance.bounds.extents.y)
        {
            gameObject.SetActive(false);
        }
    }
    public abstract void OnCollision();
    
}
