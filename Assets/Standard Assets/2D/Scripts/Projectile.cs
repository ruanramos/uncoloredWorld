using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityStandardAssets._2D;

public class Projectile : MonoBehaviour {

    public Vector2 velocity = new Vector2 (100, 100);
    Color orange = new Color(255 / 255f, 119 / 255f, 0 / 255f, 255 / 255f);
    Color violet = new Color(128 / 255f, 36 / 255f, 86 / 255f, 255 / 255f);
    Color personalGreen = new Color(128 / 255f, 154 / 255f, 86 / 255f, 255 / 255f);

    void OnCollisionEnter2D(Collision2D coll)
    {
        GameObject player = GameObject.FindGameObjectWithTag("Player");
        if (coll.gameObject.tag == "Platform")
        {
            Color colliderColor = coll.gameObject.GetComponent<SpriteRenderer>().color;
            if (colliderColor == Color.white)
            {
                coll.gameObject.GetComponent<SpriteRenderer>().color = this.GetComponent<SpriteRenderer>().color;
                player.GetComponent<PlatformerCharacter2D>().timePassedAfterGrounded = 0;
            }
            else if (colliderColor == Color.red || colliderColor == Color.yellow || colliderColor == Color.blue || colliderColor == violet || colliderColor == orange || colliderColor == personalGreen)
            {
                // if projectile white, color of the platform gets white
                if (this.GetComponent<SpriteRenderer>().color == Color.white)
                {
                    coll.gameObject.GetComponent<SpriteRenderer>().color = Color.white;
                    
                    player.GetComponent<PlatformerCharacter2D>().isInContactWithYellowPlatform = false;

                    if (colliderColor == personalGreen)
                    {
                        coll.gameObject.transform.GetChild(0).gameObject.GetComponent<AreaEffector2D>().enabled = false;
                    }
                }
                else if (this.GetComponent<SpriteRenderer>().color == orange)
                {
                    coll.gameObject.GetComponent<PlataformController>().shouldTeleport = false;
                }

                // if projectile red
                else if (this.GetComponent<SpriteRenderer>().color == Color.red)
                {
                    // red + blue = violet
                    if (coll.gameObject.GetComponent<SpriteRenderer>().color == Color.blue)
                    {
                        coll.gameObject.GetComponent<SpriteRenderer>().color = violet;
                    }

                    // red + yellow = orange
                    else if (coll.gameObject.GetComponent<SpriteRenderer>().color == Color.yellow)
                    {
                        coll.gameObject.GetComponent<SpriteRenderer>().color = orange;
                        coll.gameObject.GetComponent<PlataformController>().shouldTeleport = true;
                        player.GetComponent<PlatformerCharacter2D>().isInContactWithYellowPlatform = false;
                    }
                }

                // if projectile blue
                else if (this.GetComponent<SpriteRenderer>().color == Color.blue)
                {
                    // red + blue = violet
                    if (coll.gameObject.GetComponent<SpriteRenderer>().color == Color.red)
                    {
                        coll.gameObject.GetComponent<SpriteRenderer>().color = violet;
                    }

                    // blue + yellow = green
                    else if (coll.gameObject.GetComponent<SpriteRenderer>().color == Color.yellow)
                    {
                        coll.gameObject.GetComponent<SpriteRenderer>().color = personalGreen;
                        player.GetComponent<PlatformerCharacter2D>().isInContactWithYellowPlatform = false;
                    }
                }

                // if projectile yellow
                else if (this.GetComponent<SpriteRenderer>().color == Color.yellow)
                {
                    // red + yellow = orange
                    if (coll.gameObject.GetComponent<SpriteRenderer>().color == Color.red)
                    {
                        coll.gameObject.GetComponent<SpriteRenderer>().color = orange;
                        coll.gameObject.GetComponent<PlataformController>().shouldTeleport = true;
                        player.GetComponent<PlatformerCharacter2D>().isInContactWithYellowPlatform = false;
                    }

                    // blue + yellow = green
                    else if (coll.gameObject.GetComponent<SpriteRenderer>().color == Color.blue)
                    {
                        coll.gameObject.GetComponent<SpriteRenderer>().color = personalGreen;
                        player.GetComponent<PlatformerCharacter2D>().isInContactWithYellowPlatform = false;
                    }
                }
            }
        }
        if (coll.gameObject.tag != "Player" && coll.gameObject.tag != "Projectile")
        {
            Destroy(this.gameObject);
        }

        if (this.gameObject.tag == "ProjectileHitCharacter" && coll.gameObject.tag != "Player")
        {
            DestroyImmediate(this.gameObject);
        }
    }

    private void OnCollisionStay2D(Collision2D coll)
    {
        if (this.gameObject.tag == "ProjectileHitCharacter" && coll.gameObject.tag != "Player")
        {
            DestroyImmediate(this.gameObject);
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        Color projectileColor = this.GetComponent<SpriteRenderer>().color;

        // projectile painting the enemy on contact
        if (collision.tag == "Enemy")
        {
            // if projectile white, paints the enemy white
            if (projectileColor == Color.white)
            {
                collision.GetComponent<SpriteRenderer>().color = Color.white;
            }

            // if projectile red
            else if (projectileColor == Color.red)
            {
                // if enemy white, paints enemy red
                if (collision.GetComponent<SpriteRenderer>().color == Color.white)
                {
                    collision.GetComponent<SpriteRenderer>().color = Color.red;
                }
                // if enemy blue, paints enemy violet
                else if (collision.GetComponent<SpriteRenderer>().color == Color.blue)
                {
                    collision.GetComponent<SpriteRenderer>().color = violet;
                }
                // if enemy yellow, paints enemy orange
                else if (collision.GetComponent<SpriteRenderer>().color == Color.yellow)
                {
                    collision.GetComponent<SpriteRenderer>().color = orange;
                }
            }

            // if projectile blue
            else if (projectileColor == Color.blue)
            {
                // if enemy white, paints enemy blue
                if (collision.GetComponent<SpriteRenderer>().color == Color.white)
                {
                    collision.GetComponent<SpriteRenderer>().color = Color.blue;
                }
                // if enemy red, paints enemy violet
                else if (collision.GetComponent<SpriteRenderer>().color == Color.red)
                {
                    collision.GetComponent<SpriteRenderer>().color = violet;
                }
                // if enemy yellow, paints enemy green
                else if (collision.GetComponent<SpriteRenderer>().color == Color.yellow)
                {
                    collision.GetComponent<SpriteRenderer>().color = personalGreen;
                }
            }

            // if projectile yellow
            else if (projectileColor == Color.yellow)
            {
                // if enemy white, paints enemy yellow
                if (collision.GetComponent<SpriteRenderer>().color == Color.white)
                {
                    collision.GetComponent<SpriteRenderer>().color = Color.yellow;
                }
                // if enemy blue, paints enemy green
                else if (collision.GetComponent<SpriteRenderer>().color == Color.blue)
                {
                    collision.GetComponent<SpriteRenderer>().color = personalGreen;
                }
                // if enemy red, paints enemy orange
                else if (collision.GetComponent<SpriteRenderer>().color == Color.red)
                {
                    collision.GetComponent<SpriteRenderer>().color = orange;
                }
            }

            Destroy(this.gameObject);
        }
    }
}
