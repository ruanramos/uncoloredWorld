using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;
using UnityEngine.UI;

public class TextPositionScript : MonoBehaviour {

    GameObject player;
    private Vector3 offset = new Vector3(-10, 50, 0);

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position - offset;
    }

    // Update is called once per frame
    void Update()
    {
        if (player.gameObject.GetComponent<Platformer2DUserControl>().paintable)
        {
            this.GetComponent<Text>().enabled = true;
        }
        else
        {
            this.GetComponent<Text>().enabled = false;
        }
        transform.position = player.transform.position - offset;
    }
}
