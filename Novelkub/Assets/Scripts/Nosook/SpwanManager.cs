using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SpwanManager : MonoBehaviour
{
    public GameObject[] noSooks = new GameObject[4];
    public Transform[] noSooksPositoin = new Transform[4];
    public List<int> RandomPosList = new List<int>() { 0, 1, 2, 3 };
    public Transform[] ItemPos = new Transform[8];
    public GameObject mayac;
    // Start is called before the first frame update
    public static SpwanManager Instance;
    void Awake()
    {
        if (Instance != null)
        {
            Destroy(gameObject);
            return;
        }
        Instance = this;


    }
    void Start()
    {
        Setting();
    }

    // Update is called once per frame
    void Update()
    {

    }

    public void OnMayac()
    {
        mayac.SetActive(true);
    }

    public void Setting()
    {
        for (int i = 0; i < noSooks.Length; i++)
        {
            int rand = Random.Range(0, RandomPosList.Count);
            noSooks[RandomPosList[rand]].transform.position = noSooksPositoin[i].position;
            RandomPosList.RemoveAt(rand);
        }
        mayac.transform.position = ItemPos[Random.Range(0, 8)].position;
        mayac.SetActive(false);
    }
}