using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CollectibleObject : FallingObject
{
    protected Animator anim;
    protected override void Awake()
    {
        base.Awake();
        anim = GetComponent<Animator>();
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        anim.runtimeAnimatorController = GameManager.instance.fruitControllers[Random.Range(0, GameManager.instance.fruitControllers.Length)];
    }
    public override void OnCollision()
    {
        GameManager.instance.score++;
        if (GameManager.instance.score != 0 && GameManager.instance.score % 10 == 0)
        {
            GameManager.instance.time += 10;
            GameManager.instance.objSpawner.IncreaseDifficulty();
            GameManager.instance.uiManager.TimerFX();
        }
            
        gameObject.SetActive(false);
        GameObject obj = PoolManager.instance.GetObject("collectedFX");
        GameManager.instance.player.collectedFX.SetActive(true);
        obj.SetActive(true);
        obj.transform.position = transform.position;
        AudioManager.instance.PlaySound("powerup", true);
        
    }
    protected override void LateUpdate()
    {
        if (GameManager.instance.player.currentPowerUp != null && GameManager.instance.player.currentPowerUp.type == PowerupType.Magnet)
        {
            Vector2 dir = (GameManager.instance.player.transform.position - transform.position).normalized;
            rb.linearVelocity = dir * speed;
            return;
        } 
        base.LateUpdate();
    }
}
