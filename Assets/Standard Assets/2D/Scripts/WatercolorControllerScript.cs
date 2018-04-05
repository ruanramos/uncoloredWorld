using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityEngine.UI;

public class WatercolorControllerScript : MonoBehaviour {

    GameObject player;
    private Color selectedColor;

// Use this for initialization
void Start ()
    {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update () {
        selectedColor = player.GetComponent<Platformer2DUserControl>().selectedColor;
        this.GetComponent<Text>().color = selectedColor;
    }
}
