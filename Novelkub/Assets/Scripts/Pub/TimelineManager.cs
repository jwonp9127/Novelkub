using System;
using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.Playables;
using UnityEngine.Serialization;

public class TimelineManager : MonoBehaviour
{
    public PlayableDirector take1Director;
    public PlayableDirector take2Director;
    public GameObject take1StartArea;
    public GameObject take2StartArea;
    public bool take1IsEnd;
    public bool take2IsEnd;

    private void Start()
    {
        take1IsEnd = false;
        take2IsEnd = false;
        take1StartArea.SetActive(false);
        take2StartArea.SetActive(false);
    }

    public void Take1()
    {
        take1Director.gameObject.SetActive(true);
        take1Director.Play();
        take1StartArea.SetActive(false);
    }
    
    public void Take2()
    {
        take2Director.gameObject.SetActive(true);
        take2Director.Play();
        take2StartArea.SetActive(false);
    }

    public void Take1End()
    {
        take1Director.gameObject.SetActive(false);
        take2StartArea.SetActive(true);
        take1IsEnd = true;
        Debug.Log("take1End");
    }
    
    public void Take2End()
    {
        take2Director.gameObject.SetActive(false);
        take2IsEnd = true;
        Debug.Log("take2End");
    }
}
