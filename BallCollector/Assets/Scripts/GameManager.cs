using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameManager : MonoBehaviour
{
    public int score;
    public float time;
    public int bestScore;
    public Bounds bounds;
    public static GameManager instance;
    public UIManager uiManager;
    public bool canPlay;
    public RuntimeAnimatorController[] fruitControllers;
    public Sprite[] trapSprites;
    public PowerUpData[] powerUpDataList;
    public Player player;
    public Transform bgTransform;
    public ObjectSpawner objSpawner;
    private void Awake()
    {
        instance = this;
        bestScore = PlayerPrefs.GetInt("best_score", 0);
    }
    // Start is called before the first frame update
    void Start()
    {
        bounds = OrthographicBounds(Camera.main);
    }

    // Update is called once per frame
    void Update()
    {
        if (!canPlay) return;
        time -= Time.deltaTime;
        uiManager.UpdateText();
        if(time <= 0)
        {
            canPlay = false;
            uiManager.OpenGameEnd();
        }
    }
    public Bounds OrthographicBounds(Camera camera)
    {
        float screenAspect = (float)Screen.width / (float)Screen.height;
        float cameraHeight = camera.orthographicSize * 2;
        Bounds bounds = new Bounds(
            camera.transform.position,
            new Vector3(cameraHeight * screenAspect, cameraHeight, 0));
        return bounds;
    }
}
