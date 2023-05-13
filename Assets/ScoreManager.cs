using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class ScoreManager : MonoBehaviour
{
    // Start is called before the first frame update
    public TMP_Text text;
    int currentScore;
    public GameObject collectGem;

    public int CoinAmount
    {
        get { return currentScore; }
        set { currentScore = value;
            text.text = CoinAmount.ToString();
            PlayerPrefs.SetInt("Gem", CoinAmount);
        }
    }

    private void Awake()
    {
        CoinAmount = PlayerPrefs.GetInt("Gem");
        Debug.Log("Player Prefs: " + PlayerPrefs.GetInt("Gem"));
        Debug.Log("Coin Amount:" + CoinAmount);
    }
    private void Start()
    {
        text.text = PlayerPrefs.GetInt("Gem").ToString();
    }
    public void addScore()
    {
        CoinAmount++;
        if (!collectGem.activeSelf)
        {
            collectGem.SetActive(true);
        }
        if (collectGem.activeSelf)
        {
            Invoke("deactiveSound", 2f);
        }
        PlayerPrefs.SetInt("Gem", CoinAmount);
    }
    public void addScore(int amount)
    {
        CoinAmount +=amount;
        if (!collectGem.activeSelf)
        {
            collectGem.SetActive(true);
        }
        if (collectGem.activeSelf)
        {
            Invoke("deactiveSound", 2f);
        }
    }

    public string getCurrent()
    {
        return CoinAmount.ToString();
    }
    void deactiveSound()
    {
        collectGem.SetActive(false);
    }
}
