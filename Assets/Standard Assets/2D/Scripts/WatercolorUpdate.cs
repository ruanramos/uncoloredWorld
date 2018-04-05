using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class WatercolorUpdate : MonoBehaviour {

    [SerializeField] public Image[] images;
    GameObject player;
    private int pointer;

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        images = gameObject.GetComponentsInChildren<Image>();
	}
	
	// Update is called once per frame
	void Update ()
    {
        pointer = player.GetComponent<Platformer2DUserControl>().pointer;
        if (pointer == 0)
        {
            foreach(Image image in images)
            {
                if (image.name != "White")
                {
                    image.GetComponent<Image>().enabled = false;
                }
                else
                {
                    image.GetComponent<Image>().enabled = true;
                }
            }
        }
        else if (pointer == 1)
        {
            foreach (Image image in images)
            {
                if (image.name != "Red")
                {
                    image.GetComponent<Image>().enabled = false;
                }
                else
                {
                    image.GetComponent<Image>().enabled = true;
                }
            }
        }
        else if (pointer == 2)
        {
            foreach (Image image in images)
            {
                if (image.name != "Yellow")
                {
                    image.GetComponent<Image>().enabled = false;
                }
                else
                {
                    image.GetComponent<Image>().enabled = true;
                }
            }
        }
        else if (pointer == 3)
        {
            foreach (Image image in images)
            {
                if (image.name != "Blue")
                {
                    image.GetComponent<Image>().enabled = false;
                }
                else
                {
                    image.GetComponent<Image>().enabled = true;
                }
            }
        }

    }
}
