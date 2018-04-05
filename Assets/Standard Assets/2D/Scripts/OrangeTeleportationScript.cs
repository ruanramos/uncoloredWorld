using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class OrangeTeleportationScript : MonoBehaviour
{

    GameObject player;
    Color orange = new Color(255 / 255f, 119 / 255f, 0 / 255f, 255 / 255f);
    public GameObject prefabClone;
    public bool shouldActivateClones = false;
    public bool clonesOnScreen = false;
    [HideInInspector]public int numberOfClones = 7;

    // Use this for initialization
    void Start()
    {
        player = GameObject.FindGameObjectWithTag("Player");
        transform.position = player.transform.position;
    }

    // Update is called once per frame
    void Update()
    {
        transform.position = player.transform.position;
        if (player.GetComponent<PlatformerCharacter2D>().characterColor == orange)
        {
            shouldActivateClones = true;
        }
        if (shouldActivateClones && !clonesOnScreen)
        {
            for (int i = 0; i < numberOfClones; i++)
            {
                float randX = Random.Range(player.transform.position.x - 500, player.transform.position.x + 500);
                float randY = Random.Range(player.transform.position.y - 250, player.transform.position.y + 250);
                Vector3 randomPosition = new Vector3(randX, randY, 0);
                GameObject clone = Instantiate(prefabClone, randomPosition, transform.rotation) as GameObject;
            }
            clonesOnScreen = true;
            shouldActivateClones = false;
            player.GetComponent<PlatformerCharacter2D>().characterColor = Color.white;
        }
    }
}