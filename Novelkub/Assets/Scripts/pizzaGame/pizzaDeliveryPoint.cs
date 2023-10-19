using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using TMPro;
using System;

public class pizzaDeliveryPoint : MonoBehaviour
{
    AudioSource audio;
    public pizzaGameManager manager;
    private float gameduration = 180.0f;
    public TMP_Text timeText;

    void Awake()
    {
        audio = GetComponent<AudioSource>();
    }

    private void Start()
    {
        UpdatetimeText();
    }

    private void UpdatetimeText()
    {
        throw new NotImplementedException();
    }

    private void Update()
    {
        if (gameduration > 0.0f)
        {
            gameduration -= Time.deltaTime;
            if (gameduration <= 0.0f)
            {
                //restartquest();
            }
            UpdatetimeText();
        }
    }

    private void OnTriggerEnter(Collider other)
    {
        if (other.tag == "point")
        {
            Player player = other.GetComponent<Player>();
            player.pointCount++;
            audio.Play();
            gameObject.SetActive(false);
        }
    }

    private void updatetimertext()
    {
        int minutes = Mathf.FloorToInt(gameduration / 60);
        int seconds = Mathf.FloorToInt(gameduration - minutes * 60);
        timeText.text = string.Format ("{0:00}:{1:00}", minutes, seconds);
    }
}
