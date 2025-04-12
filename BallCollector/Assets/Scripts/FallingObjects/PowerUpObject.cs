using UnityEngine;

public class PowerUpObject : FallingObject
{
    public PowerUpData data;
    public override void OnCollision()
    {
        GameManager.instance.player.SetPowerUpType(data.type);
        gameObject.SetActive(false);
        GameManager.instance.player.collectedFX.SetActive(true);
        AudioManager.instance.PlaySound("powerup", true);
    }
    protected override void OnEnable()
    {
        base.OnEnable();
        data = GameManager.instance.powerUpDataList[Random.Range(0, GameManager.instance.powerUpDataList.Length)];
        sr.sprite = data.sprite;
    }
}
[System.Serializable]
public class PowerUpData
{
    public PowerupType type;
    public Sprite sprite;
}