using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DialogManager : MonoBehaviour
{
    private Dictionary<int, string[]> _dialogData;

    private void Awake()
    {
        _dialogData = new Dictionary<int, string[]>();
        GenerateData();
    }

    private void GenerateData()
    {
        // _dialogData.Add();
    }
}
