using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UIManager : MonoBehaviour
{
    public static UIManager instance;
    public GameObject[] uiBooms;

    private void Awake()
    {
        if (instance == null) instance = this;
        else Destroy(gameObject);

        DontDestroyOnLoad(gameObject); // ?
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
}
