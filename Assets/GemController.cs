using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GemController : MonoBehaviour
{
    // Start is called before the first frame update
    float rotationSpeed = 100f;
    ScoreManager scoreMan;
    public CoinsManager coinMan;
    void Start()
    {
        scoreMan = FindObjectOfType<ScoreManager>();
    }

    // Update is called once per frame
    void Update()
    {
        transform.Rotate(Vector3.up * Time.deltaTime * rotationSpeed);
    }

    private void OnTriggerEnter(Collider other)
    {
        if(other.gameObject.tag == "cube")
        {
            scoreMan.addScore();
            coinMan.AddCoins(this.gameObject.transform.position, 1);
            Destroy(this.gameObject);
        }
        
    }
}
