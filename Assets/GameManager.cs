using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class GameManager : MonoBehaviour
{
    [Header("Characters")]
    public GameObject[] heros;
    public Transform herosParent;
    public RuntimeAnimatorController herosAnim;

    [Header("Other")]
    public Animator planeAnim;
    public GameObject storeImg;
    public GameObject finalAnim;

    public static int lastSceneIdx;
    private void Start()
    {
        lastSceneIdx = SceneManager.GetActiveScene().buildIndex;
        GameObject newObj = Instantiate(heros[Charactermenu.currIndex],herosParent);
        newObj.AddComponent<Animator>();
        newObj.GetComponent<Animator>().runtimeAnimatorController = herosAnim;
    }

    public void loadStoreScene()
    {
        SceneManager.LoadScene(0);
    }
    public void nextLevel()
    {
        finalAnim.SetActive(true);
        planeAnim.SetTrigger("Fly");
        FindObjectOfType<CameraFollowController>().finishCameraMovement();
        Invoke("loadNextScene", 3f);
    }
    public void startGame()
    {
        storeImg.SetActive(false);
    }
    public void rePlay()
    {
        Invoke("resetGame", 1.99f);
    }

    void loadNextScene()
    {
        if (SceneManager.GetActiveScene().buildIndex == 2)
        {
            SceneManager.LoadScene(1);
        }
        else
        {
            SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex + 1);
        }
    }

    void resetGame()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().buildIndex);
    }


}
