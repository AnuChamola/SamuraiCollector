using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;
using UnityEngine.SceneManagement;
using DG.Tweening;
public class UIManager : MonoBehaviour
{
    [SerializeField] TMP_Text scoreText;
    [SerializeField] TMP_Text timerText;
    [SerializeField] TMP_Text bestScoreText;
    [SerializeField] Transform gameEndPopup;

    public void UpdateText()
    {
        scoreText.text = GameManager.instance.score.ToString();
        float m = TimeSpan.FromSeconds(GameManager.instance.time).Minutes;
        float s = TimeSpan.FromSeconds(GameManager.instance.time).Seconds;
        timerText.text = (String.Format("{0}m:{1}s", m, s));
    }
    public void RestartScene()
    {
        AudioManager.instance.PlaySound("slash", true);
        SceneManager.LoadScene(1);
    }
    public void BackToHome()
    {
        AudioManager.instance.PlaySound("slash", true);
        SceneManager.LoadScene(0);
    }
    public void OpenGameEnd()
    {
        gameEndPopup.localScale = Vector3.zero;
        gameEndPopup.DOScale(Vector3.one, 0.5f).SetEase(Ease.OutElastic);
        gameEndPopup.transform.parent.gameObject.SetActive(true);
        GameManager.instance.bestScore = Mathf.Max(GameManager.instance.bestScore, GameManager.instance.score);
        bestScoreText.text = "BEST SCORE: " + GameManager.instance.bestScore;
        PlayerPrefs.SetInt("best_score", GameManager.instance.bestScore);
    }

    public void TimerFX(bool decrease = false)
    {
        timerText.GetComponent<RectTransform>().DOKill();
        timerText.DOKill();
        timerText.color = Color.white;
        timerText.GetComponent<RectTransform>().DOScale(Vector3.one * 1.2f, 0.2f).SetLoops(2, LoopType.Yoyo);
        Color col = decrease ? Color.red : Color.green;
        timerText.DOColor(col, 0.2f).SetLoops(2, LoopType.Yoyo);
    }
    
}
