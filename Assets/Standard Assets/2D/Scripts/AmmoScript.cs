using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets._2D;

public class AmmoScript : MonoBehaviour {

    GameObject player;
    Color color;
    int pointer;
    private bool isColliding;

    private void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        if (this.gameObject.tag == "BlueAmmo")
        {
            color = Color.blue;
        } else if (this.gameObject.tag == "WhiteAmmo")
        {
            color = Color.white;
        }
        else if (this.gameObject.tag == "YellowAmmo")
        {
            color = Color.yellow;
        }
        else if (this.gameObject.tag == "RedAmmo")
        {
            color = Color.red;
        }
    }

    private void Update()
    {
        isColliding = false;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (isColliding) return;
        isColliding = true;

        pointer = player.GetComponent<Platformer2DUserControl>().pointer;

        // atualize ammo after colecting ammo on the game
        if (color == Color.white)
        {
            player.GetComponent<Platformer2DUserControl>().ammo[0]++;
            GameObject.Find("WhiteAmmoText").GetComponent<Text>().text = "" + player.GetComponent<Platformer2DUserControl>().ammo[0];
        }
        else if (color == Color.red)
        {
            player.GetComponent<Platformer2DUserControl>().ammo[1]++;
            GameObject.Find("RedAmmoText").GetComponent<Text>().text = "" + player.GetComponent<Platformer2DUserControl>().ammo[1];
        }
        else if (color == Color.yellow)
        {
            player.GetComponent<Platformer2DUserControl>().ammo[2]++;
            GameObject.Find("YellowAmmoText").GetComponent<Text>().text = "" + player.GetComponent<Platformer2DUserControl>().ammo[2];
        }
        else if (color == Color.blue)
        {
            player.GetComponent<Platformer2DUserControl>().ammo[3]++;
            GameObject.Find("BlueAmmoText").GetComponent<Text>().text = "" + player.GetComponent<Platformer2DUserControl>().ammo[3];
        }
        Destroy(this.gameObject);
    }
}
