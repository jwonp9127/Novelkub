using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    public GameObject[] noSooks = new GameObject[4];
    public Transform [] noSooksPositoin = new Transform[4];
    public List<int> RandomPosList = new List<int>() { 0, 1, 2, 3 };
    // Start is called before the first frame update
    void Start()
    {
        for(int i = 0; i < noSooks.Length; i++) 
        {
            int rand = Random.Range(0, RandomPosList.Count);
            noSooks[RandomPosList[rand]].transform.position = noSooksPositoin[i].position;
            RandomPosList.RemoveAt(rand);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
