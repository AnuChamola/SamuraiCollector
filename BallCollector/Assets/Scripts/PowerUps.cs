using UnityEngine;
using UnityEngine.UI;

public enum PowerupType
{
    Shield,
    Magnet
}
public class PowerUps : MonoBehaviour
{
    private float time;
    public float maxTime;
    public PowerupType type;
    private Animator anim;
    public bool timer;
    public Image timerBarFill;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        anim = GetComponent<Animator>();
        time = maxTime;
    }
    private void Update()
    {
        if (!timer) return;
        time -= Time.deltaTime;
        timerBarFill.fillAmount = time / maxTime;
        if (time <= 0)
        {
            time = 0;
            DisablePowerup();
        }
    }
    public void Init()
    {
        time = maxTime;
        timerBarFill.fillAmount = 1;
        gameObject.SetActive(true);
        timer = true;
    }
    public void DisablePowerup()
    {
        GameManager.instance.player.currentPowerUp = null;
        timer = false;
        time = maxTime;
        gameObject.SetActive(false);
    }
    
}


