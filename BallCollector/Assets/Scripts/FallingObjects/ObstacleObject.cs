using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ObstacleObject : FallingObject
{
    protected override void OnEnable()
    {
        base.OnEnable();
        sr.sprite = GameManager.instance.trapSprites[Random.Range(0, GameManager.instance.trapSprites.Length)];
    }
    public override void OnCollision()
    {
        
        if (GameManager.instance.player.currentPowerUp != null && GameManager.instance.player.currentPowerUp.type == PowerupType.Shield)
        {
            SpawnBlast();
            AudioManager.instance.PlaySound("explosion", true);
            return;
        } 
        GameManager.instance.time -= 10;
        if (GameManager.instance.time <= 0) GameManager.instance.time = 0;
        GameManager.instance.player.Anim.Play("Hurt");
        AudioManager.instance.PlaySound("explosion", true);
        GameManager.instance.uiManager.TimerFX(true);
        SpawnBlast();

    }
    private void SpawnBlast()
    {
        gameObject.SetActive(false);
        GameObject obj = PoolManager.instance.GetObject("blast");
        obj.SetActive(true);
        obj.transform.position = transform.position;
    }
}
