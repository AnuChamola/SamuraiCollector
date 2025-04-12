using System.Linq;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    private SpriteRenderer sr;
    private Animator anim;
    public Animator Anim { get { return anim; } }
    private Vector2 lastPos;
    public PowerUps currentPowerUp;
    public PowerUps[] powerUpsList;
    public GameObject collectedFX;
    // Start is called before the first frame update
    void Start()
    {
        sr = GetComponent<SpriteRenderer>();
        anim = GetComponent<Animator>();
    }
    private void OnMouseDrag()
    {
        if (!GameManager.instance.canPlay) return;
        Vector3 mousePos = Camera.main.ScreenToWorldPoint(Input.mousePosition);
        if(mousePos.x != lastPos.x)
        {
            if (mousePos.x < lastPos.x)
            {
                //dragging left
                sr.flipX = true;
            }
            else
            {
                //dragging right
                sr.flipX = false;
            }
        }
        
        //mousePos.y = -GameManager.instance.bounds.extents.y + (sr.bounds.size.y * 0.5f);
        mousePos.y = -2.6f;
        mousePos.z = 0;
        mousePos.x = Mathf.Clamp(mousePos.x, -GameManager.instance.bounds.extents.x + (sr.bounds.size.x * 0.5f), GameManager.instance.bounds.extents.x - (sr.bounds.size.x * 0.5f));
        transform.position = mousePos;
        GameManager.instance.bgTransform.localPosition = new Vector3((mousePos.x * 0.1f) * -1f, GameManager.instance.bgTransform.localPosition.y, GameManager.instance.bgTransform.localPosition.z);
        anim.SetBool("run", true);
        lastPos = mousePos;
    }
    private void OnMouseUp()
    {
        anim.SetBool("run", false);
    }
    private void OnTriggerEnter2D(Collider2D col)
    {
        FallingObject obj = col.GetComponent<FallingObject>();
        if (obj != null) obj.OnCollision();
    }
    public void SetPowerUpType(PowerupType _type)
    {
        if(currentPowerUp != null) currentPowerUp.DisablePowerup();
        currentPowerUp = powerUpsList.ToList().Find(x => x.type == _type);
        currentPowerUp.Init();
    }
}
