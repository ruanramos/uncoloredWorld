using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class PlataformController : MonoBehaviour {

    private Color PlataformColor;
    private SpriteRenderer plataformSpriteRenderer;
    public Vector2 velocity = new Vector2(0, 0);
    [SerializeField] private float yTopLimit;
    [SerializeField] private float yBottomLimit;
    [SerializeField] private float xTopLimit;
    [SerializeField] private float xBottomLimit;

    [SerializeField] private Vector2 firstOrangePosition;
    [SerializeField] private Vector2 secondOrangePosition;
    [NonSerialized] public bool shouldTeleport;
    enum orangePosition {first, second };
    private orangePosition pos = orangePosition.first;

    private float platformVelocityValue = 40f;

    Color orange = new Color(255 / 255f, 119 / 255f, 0 / 255f, 255 / 255f);
    Color violet = new Color(128 / 255f, 36 / 255f, 86 / 255f, 255 / 255f);
    Color personalGreen = new Color(128 / 255f, 154 / 255f, 86 / 255f, 255 / 255f);

    GameObject player;
    private BoxCollider2D boxCollider;
    GameObject areaEffector;

    public bool isWall = false;

    public GameObject prefabClone;


    void Start () {
        player = GameObject.FindGameObjectWithTag("Player");
        plataformSpriteRenderer = this.GetComponent<SpriteRenderer>();
        if (this.tag == "Platform") plataformSpriteRenderer.color = Color.white;
        boxCollider = this.GetComponent<BoxCollider2D>();
        shouldTeleport = false;
        
    }
	
	void Update () {
        PlataformColor = plataformSpriteRenderer.color;
        Move();
        
        // Make character violet effect
        if ((this.tag == "Platform" && player.GetComponent<PlatformerCharacter2D>().characterColor == violet) || PlataformColor == violet)
        {
            boxCollider.isTrigger = true;
        }
        else
        {
            boxCollider.isTrigger = false;
        }
	}

    bool MovingUp()
    {
        if (velocity.y > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool MovingDown()
    {
        if (velocity.y < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool MovingLeft()
    {
        if (velocity.x < 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool MovingRight()
    {
        if (velocity.x > 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool notMovingVertically()
    {
        if (velocity.y == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    bool notMovingHorizontally()
    {
        if (velocity.x == 0)
        {
            return true;
        }
        else
        {
            return false;
        }
    }

    void Move()
    {
        if (PlataformColor == Color.blue)
        {
            // blue platform movement and blue wall movement
            if (yTopLimit == yBottomLimit)
            {
                velocity.y = 0;
            }
            else if (transform.position.y > yTopLimit && MovingUp())
            {
                velocity.y = -platformVelocityValue;
            }
            else if (transform.position.y <= yBottomLimit && MovingDown())
            {
                    velocity.y = platformVelocityValue;
            }
            else
            {
                if (MovingUp() || notMovingVertically()) velocity.y = platformVelocityValue;
                if (MovingDown()) velocity.y = -platformVelocityValue;        
            }
        }

        else if (PlataformColor == Color.yellow)
        {
            // yellow platform movement and yellow wall movement
            if (xTopLimit == xBottomLimit)
            {
                velocity.x = 0;
                player.GetComponent<PlatformerCharacter2D>().isInContactWithYellowPlatform = false;
            }
            else if (transform.position.x > xTopLimit && MovingRight())
            {
                velocity.x = -platformVelocityValue;
            }
            else if (transform.position.x <= xBottomLimit && MovingLeft())
            {
                velocity.x = platformVelocityValue;
            }
            else
            {
                if (MovingRight() || notMovingHorizontally()) velocity.x = platformVelocityValue;
                if (MovingLeft()) velocity.x = -platformVelocityValue;
            }
        }
        
        else if (PlataformColor == Color.white)
        {
            velocity.x = 0;
            velocity.y = 0;
        }
        else if (PlataformColor == violet)
        {
            velocity.x = 0;
            velocity.y = 0;
            this.GetComponent<BoxCollider2D>().isTrigger = true;
        }

        else if (PlataformColor == personalGreen)
        {
            velocity.x = 0;
            velocity.y = 0;
            // turn on effector
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<AreaEffector2D>().enabled = true;
            this.gameObject.transform.GetChild(0).gameObject.GetComponent<BoxCollider2D>().enabled = true;
        }
        else if (PlataformColor == orange)
        {
            velocity.x = 0;
            velocity.y = 0;
        }

        Vector3 movement = new Vector3(velocity.x, velocity.y, 0);
        transform.Translate(movement * Time.deltaTime);
    }

    private void OnCollisionStay2D(Collision2D collision)
    {
        if (collision.gameObject.tag == "Player" && this.PlataformColor == orange)
        {
            if (shouldTeleport && firstOrangePosition != secondOrangePosition)
            {
                if (pos == orangePosition.first)
                { 
                    transform.position = secondOrangePosition;
                    if (!isWall)
                    {
                        collision.transform.position = new Vector2(transform.position.x, transform.position.y + 70);
                    }
                    pos = orangePosition.second;
                    
                }
                else if (pos == orangePosition.second)
                {
                    transform.position = firstOrangePosition;
                    if (!isWall)
                    {
                        collision.transform.position = new Vector2(transform.position.x, transform.position.y + 70);
                    }
                    pos = orangePosition.first;
                }
                shouldTeleport = false;
            }
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        // reseting the platform to white
        if (collision.tag == "Projectile")
        {
            if (collision.GetComponent<SpriteRenderer>().color == Color.white)
            {
                this.GetComponent<SpriteRenderer>().color = Color.white;
                boxCollider.isTrigger = false;
                Destroy(collision.gameObject);
            }
        }
    }

    private void OnTriggerStay2D(Collider2D collision)
    {
        if (collision.tag == "Clone")
        {
            float randX = UnityEngine.Random.Range(player.transform.position.x - 500, player.transform.position.x + 500);
            float randY = UnityEngine.Random.Range(player.transform.position.y - 250, player.transform.position.y + 250);
            Vector3 randomPosition = new Vector3(randX, randY, 0);
            GameObject clone = Instantiate(prefabClone, randomPosition, transform.rotation) as GameObject;
            Destroy(collision.gameObject);
        }
    }
}
