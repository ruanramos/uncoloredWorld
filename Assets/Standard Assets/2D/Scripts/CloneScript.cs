using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class CloneScript : MonoBehaviour {

    GameObject player;
    GameObject teleportationObject;

    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        teleportationObject = GameObject.Find("OrangeTeletransportArea");
    }

    private void OnMouseDown()
    {
        player.transform.position = transform.position;
        GameObject[] clones = GameObject.FindGameObjectsWithTag("Clone");
        foreach (GameObject clone in clones)
        {
            Destroy(clone);
        }
        player.GetComponent<Platformer2DUserControl>().shootButtonPressTime = -1;
        teleportationObject.GetComponent<OrangeTeleportationScript>().clonesOnScreen = false;
    }
}
