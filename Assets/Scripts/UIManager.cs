using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject[] uiBooms;
    public GameObject[] uiLifes;

    public Text scoreText;
    public int score;

    // 암막
    public Image blackOutCurtain;
    float blackOutCurtainValue;
    float blackOutCurtainSpeed;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // ?
    }

    private void Start()
    {
        score = 0;
        blackOutCurtainValue = 1.0f;
        blackOutCurtainSpeed = 0.5f;
    }

    private void Update()
    {
        if(blackOutCurtainValue > 0)
        {
            HideBlackOutCurtain();
        }
    }

    // 폭탄 아이템을 체크하는 함수
    public void BombCheck(int bombCount)
    {
        for(int i=0; i<uiBooms.Length; ++i)
        {
            if ((i + 1) <= bombCount)   uiBooms[i].SetActive(true);
            else                        uiBooms[i].SetActive(false);
        }
    }

    public void ScoreAdd(int _score)
    {
        score += _score;
        scoreText.text = score.ToString();
    }

    // 폭탄 아이템을 체크하는 함수
    public void LifeCheck(int lifeCount)
    {
        for (int i = 0; i < uiLifes.Length; ++i)
        {
            if ((i + 1) <= lifeCount) uiLifes[i].SetActive(true);
            else uiLifes[i].SetActive(false);
        }
    }

    public void HideBlackOutCurtain()
    {
        blackOutCurtainValue -= Time.deltaTime * blackOutCurtainSpeed;
        blackOutCurtain.color = new Color(0.0f, 0.0f, 0.0f, blackOutCurtainValue);
    }
}