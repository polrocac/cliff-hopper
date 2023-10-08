using System.Collections;
using System.Collections.Generic;
using TMPro;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    [SerializeField] TextMeshProUGUI coinText;
    [SerializeField] TextMeshProUGUI cornerText;
    [SerializeField] TextMeshProUGUI highscoreText;
    public static UIManager Instance { get; private set; }
    void Awake()
    {
        if (Instance != null && Instance != this)
        {
            Destroy(this);
        }
        else
        {
            Instance = this;
        }
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void UpdateCoinText()
    {
        coinText.text = GameManager.Instance.Coins.ToString();
    }

    public void UpdateCornerText()
    {
        if(GameManager.Instance.Corners == 10 || GameManager.Instance.Corners == 100 || GameManager.Instance.Corners == 1000)
        {
            //cornerText.rectTransform.anchoredPosition = (new Vector3(cornerText.rectTransform.anchoredPosition.x + 10f, cornerText.rectTransform.anchoredPosition.y, 0f));
        }
        cornerText.text = GameManager.Instance.Corners.ToString();
    }

    public void ActivarMenuMuerte(){
        transform.Find("MenuMuerte").gameObject.SetActive(true);
        transform.Find("Pause").gameObject.SetActive(false);
    }

    public void UpdateHighScore(){
        highscoreText.text = HighScore.Instance.Highscore.ToString();
    }
}
