using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlayerEffectorParticleScript : MonoBehaviour {
    GameObject player;

    Color orange = new Color(255 / 255f, 119 / 255f, 0 / 255f, 255 / 255f);
    Color violet = new Color(128 / 255f, 36 / 255f, 86 / 255f, 255 / 255f);
    Color personalGreen = new Color(128 / 255f, 154 / 255f, 86 / 255f, 255 / 255f);


    // Use this for initialization
    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void FixedUpdate () {
        if (player.GetComponent<PlatformerCharacter2D>().characterColor != personalGreen && this.GetComponent<ParticleSystem>().isPlaying)
        {
            this.GetComponent<ParticleSystem>().Stop();
        }
        else if (player.GetComponent<PlatformerCharacter2D>().characterColor == personalGreen && !this.GetComponent<ParticleSystem>().isPlaying)
        {
            this.GetComponent<ParticleSystem>().Play();
        }
    }
}
