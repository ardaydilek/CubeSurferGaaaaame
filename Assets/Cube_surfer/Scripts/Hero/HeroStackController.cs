using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class HeroStackController : MonoBehaviour
{
    
    public List<GameObject> blockList = new List<GameObject>();
    private GameObject lastBlockObject;
    public GameObject _trail;
    private Vector3 constTransform = new Vector3(0,2.3f,0);

    public GameObject getCubeprefab;

    private void Start()
    {
        UpdateLastBlockObject();
    }
   
    public void IncreaseNewBlock(GameObject _gameObject)
    {
        transform.position = new Vector3(transform.position.x, transform.position.y + 2f, transform.position.z);
        _gameObject.transform.position = new Vector3(transform.position.x, lastBlockObject.transform.position.y - 2f, transform.position.z);
        _gameObject.transform.SetParent(transform);
        blockList.Add(_gameObject);
        UpdateLastBlockObject();
        StartCoroutine(CreateAndDestroyPrefab());
    }


    public void DecreaseBlock(GameObject _gameObject)
    {
        _gameObject.transform.parent = null;
        blockList.Remove(_gameObject);
        UpdateLastBlockObject();
        Destroy(_gameObject , 2f);
    }

    public void UpdateLastBlockObject()
    {
        lastBlockObject = blockList[blockList.Count - 1];
        //transformUp = lastBlockObject.transform.position;
    }
    private void Update()
    {
        constTransform.x = lastBlockObject.transform.position.x;
        constTransform.z = lastBlockObject.transform.position.z;
        _trail.transform.position = constTransform;
    }

    IEnumerator CreateAndDestroyPrefab()
    {
        GameObject newPrefab = Instantiate(getCubeprefab, this.transform);
        //newPrefab.transform.SetParent(this.transform); //Prefab'ýn parent'ýný belirler
        yield return new WaitForSeconds(2f);
        Destroy(newPrefab);
    }
}
