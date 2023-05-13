using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class Charactermenu : MonoBehaviour
{
    // Start is called before the first frame update
    public GameObject[] characterList;
    [Space]
    public GameObject[] Header;
    public GameObject lockImg;
    public GameObject okImg;
    [Space]
    public Button priceImg;
    public static int currIndex = 0;
    public CoinsManager coins;
    void Awake()
    {
        updateCharacter(currIndex);
        if (PlayerPrefs.GetInt("AvailableCharacterIndex") == null)
        {
            PlayerPrefs.SetInt("AvailableCharacterIndex", 0);
        }
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up, 30 * Time.deltaTime, Space.Self);
    }

    public void nextCharacter()
    {
        if (++currIndex == 3)
        {
            currIndex = 0;
        }
        updateCharacter(currIndex);
    }

    public void previousChaacter()
    {
        if (--currIndex == -1)
        {
            currIndex = 2;
        }
        updateCharacter(currIndex);
    }

    private void updateCharacter(int index)
    {
        checkAvailable(currIndex);
        for (int i = 0; i < characterList.Length;i++)
        {
            if(i == currIndex)
            {
                characterList[i].SetActive(true);
                Header[i].SetActive(true);
            }
            else
            {
                characterList[i].SetActive(false);
                Header[i].SetActive(false);
            }
        }
    }

    public void buyCharacter()
    {
        int temp = PlayerPrefs.GetInt("AvailableCharacterIndex");
        if (currIndex > temp +1)
        {
            return;
        }
        else
        {
            if (PlayerPrefs.GetInt("Gem") >= 2500)
            {
                coins.scoreMan.addScore(-2500);
                PlayerPrefs.SetInt("AvailableCharacterIndex", temp + 1);
            }
            else
            {
                return;
            }
        }
        updateCharacter(currIndex);
    }

    void checkAvailable(int idx)
    {
        ColorBlock colors = priceImg.colors;
        Color normalColor = colors.normalColor;
        if (idx <= PlayerPrefs.GetInt("AvailableCharacterIndex"))
        {
            okImg.SetActive(true);
            lockImg.SetActive(false);
            Debug.Log(PlayerPrefs.GetInt("AvailableCharacterIndex"));
            priceImg.interactable = false;

            normalColor.a = 0.6f;
        }
        else
        {
            okImg.SetActive(false);
            lockImg.SetActive(true);
            priceImg.interactable = true;
            normalColor.a = 1f;
        }
        colors.normalColor = normalColor;
        priceImg.colors = colors;
    }

    public void loadGameScene()
    {
        SceneManager.LoadScene(GameManager.lastSceneIdx);
    }
}
