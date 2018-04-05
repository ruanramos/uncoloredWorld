using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemyController : MonoBehaviour {
    Color enemyColor;
    GameObject player;

    Color orange = new Color(255 / 255f, 119 / 255f, 0 / 255f, 255 / 255f);
    Color violet = new Color(128 / 255f, 36 / 255f, 86 / 255f, 255 / 255f);
    Color personalGreen = new Color(128 / 255f, 154 / 255f, 86 / 255f, 255 / 255f);

    // Use this for initialization
    void Start ()
    {
        enemyColor = Color.white;
        
        player = GameObject.FindGameObjectWithTag("Player");
    }
	
	// Update is called once per frame
	void Update ()
    {
        updateEnemyColor(enemyColor);
	}

    // updates enemy color every update
    void updateEnemyColor(Color enemyColor)
    {
        Color enemySpriteRendererColor = this.GetComponent<SpriteRenderer>().color;
        if (enemyColor == Color.red)
        {
            enemySpriteRendererColor = Color.red;
        }
        else if (enemyColor == Color.blue)
        {
            enemySpriteRendererColor = Color.blue;
        }
        else if (enemyColor == Color.yellow)
        {
            enemySpriteRendererColor = Color.yellow;
        }
        else if (enemyColor == orange)
        {
            enemySpriteRendererColor = orange;
        }
        else if (enemyColor == personalGreen)
        {
            enemySpriteRendererColor = personalGreen;
        }
        else if (enemyColor == violet)
        {
            enemySpriteRendererColor = violet;
        }
    }


}
