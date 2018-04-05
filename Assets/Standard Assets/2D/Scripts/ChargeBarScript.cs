using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class ChargeBarScript : MonoBehaviour {

    GameObject player;
    private float fillAmount;
    private Vector3 offset = new Vector3(0, -50, 0);

    // Use this for initialization
	void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position - offset;
    }
	
	// Update is called once per frame
	void Update () {
        fillAmount = player.GetComponent<Platformer2DUserControl>().chargeBarFillAmount;
        this.GetComponent<Image>().fillAmount = fillAmount;
        transform.position = player.transform.position - offset;
    }
}
