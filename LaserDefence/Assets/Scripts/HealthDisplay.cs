using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthDisplay : MonoBehaviour {

    [SerializeField] Text health;
    [SerializeField] GameSession gameSession;

	// Use this for initialization
	void Start () {
        health = GetComponent<Text>();
        gameSession = FindObjectOfType<GameSession>();
	}
	
	// Update is called once per frame
	void Update () {
        health.text = gameSession.GetHealth().ToString();
	}
}
