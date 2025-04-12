using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using DG.Tweening;
using TMPro;
public class StartScene : MonoBehaviour
{
    public Image bgCover;
    public TMP_Text titleText;
    public TMP_Text bestScoreText;
    public Animator anim;
    public RectTransform playButton;
    private void Start()
    {
        StartCoroutine(StartScreenAnimCo());
    }
    public void PlayGame()
    {
        AudioManager.instance.PlaySound("slash", true);
        StartCoroutine(PlayGameCo());
    }
    IEnumerator PlayGameCo()
    {
        bgCover.DOFade(1, 0.5f);
        yield return new WaitForSeconds(1);
        SceneManager.LoadScene(1);

    }
    IEnumerator StartScreenAnimCo()
    {
        titleText.DOFade(0, 0);
        Vector2 objPos = titleText.GetComponent<RectTransform>().anchoredPosition;
        titleText.GetComponent<RectTransform>().anchoredPosition = new Vector2(objPos.x - 50, objPos.y);
        bestScoreText.text = "BEST SCORE: " + PlayerPrefs.GetInt("best_score", 0);
        playButton.gameObject.SetActive(false);
        yield return new WaitForSeconds(1);
        titleText.GetComponent<RectTransform>().DOAnchorPosX(objPos.x, 0.5f);
        titleText.DOFade(1, 0.5f);
        yield return new WaitForSeconds(1);
        anim.Play("Attack");
        yield return new WaitForSeconds(0.3f);
        AudioManager.instance.PlaySound("slash", true);
        objPos = playButton.anchoredPosition;
        playButton.anchoredPosition = new Vector2(objPos.x - 50, objPos.y);
        playButton.DOAnchorPosX(objPos.x, 0.5f);
        playButton.gameObject.SetActive(true);
    }
}
