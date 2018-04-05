using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerEffector : MonoBehaviour {

    GameObject player;
    private Vector3 offset = new Vector3(0, 30, 0);

    Color orange = new Color(255 / 255f, 119 / 255f, 0 / 255f, 255 / 255f);
    Color violet = new Color(128 / 255f, 36 / 255f, 86 / 255f, 255 / 255f);
    Color personalGreen = new Color(128 / 255f, 154 / 255f, 86 / 255f, 255 / 255f);

    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position - offset;
    }
	
	// Update is called once per frame
	void Update () {
        bool goUp = Input.GetKey(KeyCode.W);

        if (player.GetComponent<PlatformerCharacter2D>().characterColor == personalGreen)
        {
            this.GetComponent<BoxCollider2D>().enabled = true;
            this.GetComponent<AreaEffector2D>().enabled = true;

            if (goUp && this.GetComponent<AreaEffector2D>().forceMagnitude < 200)
            {
                this.GetComponent<AreaEffector2D>().forceMagnitude += 1f;
            }
            else if (goUp && this.GetComponent<AreaEffector2D>().forceMagnitude >= 200)
            {
                this.GetComponent<AreaEffector2D>().forceMagnitude += 0.000001f;
            }
            else if (!goUp)
            {
                this.GetComponent<BoxCollider2D>().enabled = false;
                this.GetComponent<AreaEffector2D>().enabled = false;
            }
        }
        else
        {
            this.GetComponent<BoxCollider2D>().enabled = false;
            this.GetComponent<AreaEffector2D>().enabled = false;
        }
        transform.position = player.transform.position - offset;
    }
}
