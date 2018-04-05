using System;
using UnityEngine;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    public class PlatformerCharacter2D : MonoBehaviour
    {
        [Header("Character Variables")]
        [Space]
        [SerializeField] private float m_MaxSpeed = 10f;                    // The fastest the player can travel in the x axis.
        [SerializeField] private float m_JumpForce = 400f;                  // Amount of force added when the player jumps.
        [SerializeField] private bool m_AirControl = false;                 // Whether or not a player can steer while jumping;
        [SerializeField] private LayerMask m_WhatIsGround;                  // A mask determining what is ground to the character

        private int mask = ~((1 << 9) | (1 << 8));

        private Transform m_GroundCheck;    // A position marking where to check if the player is grounded.
        const float k_GroundedRadius = .2f; // Radius of the overlap circle to determine if grounded
        private bool m_Grounded;            // Whether or not the player is grounded.
        private Transform m_CeilingCheck;   // A position marking where to check for ceilings
        const float k_CeilingRadius = .01f; // Radius of the overlap circle to determine if the player can stand up
        private Animator m_Anim;            // Reference to the player's animator component.
        private Rigidbody2D m_Rigidbody2D;
        public bool m_FacingRight = true;  // For determining which way the player is currently facing.

        public GameObject prefabProjectile;
        [NonSerialized] public float timePassedAfterGrounded;
        [NonSerialized] public bool isInContactWithYellowPlatform;
        [NonSerialized] public bool isInContactWithBluePlatform;
        private bool onTheFloor;

        private int jumpCount = 0;

        private float diesOfFalling = -600f;
        private float redPlatformForce = 5500f;

        private PlataformController platform;

        //GameObject playerEffector;

        Color orange = new Color(255 / 255f, 119 / 255f, 0 / 255f, 255 / 255f);
        Color violet = new Color(128 / 255f, 36 / 255f, 86 / 255f, 255 / 255f);
        Color personalGreen = new Color(128 / 255f, 154 / 255f, 86 / 255f, 255 / 255f);
        [Space]
        [Header("Character Sprites")]
        [Space]
        public Sprite redSprite;
        public Sprite blueSprite;
        public Sprite yellowSprite;
        public Sprite greenSprite;
        public Sprite violetSprite;
        public Sprite orangeSprite;
        public Sprite whiteSprite;

        [NonSerialized] public Color characterColor = Color.white;

        private void Awake()
        {
            // Setting up references.
            m_GroundCheck = transform.Find("GroundCheck");
            m_CeilingCheck = transform.Find("CeilingCheck");
            m_Anim = GetComponent<Animator>();
            m_Rigidbody2D = GetComponent<Rigidbody2D>();
        }

        private void Start()
        {
            timePassedAfterGrounded = 0;
            onTheFloor = true;
            //playerEffector = GameObject.Find("PlayerEffector");
        }

        // updates character sprite to his actual color
        private void ChangeCharacterSprite(Color characterColor)
        {
            if (characterColor == Color.white)
            {
                m_Rigidbody2D.gameObject.GetComponent<SpriteRenderer>().sprite = whiteSprite;
            }
            else if (characterColor == Color.red)
            {
                m_Rigidbody2D.gameObject.GetComponent<SpriteRenderer>().sprite = redSprite;
            }
            else if (characterColor == Color.blue)
            {
                m_Rigidbody2D.gameObject.GetComponent<SpriteRenderer>().sprite = blueSprite;
            }
            else if (characterColor == Color.yellow)
            {
                m_Rigidbody2D.gameObject.GetComponent<SpriteRenderer>().sprite = yellowSprite;
            }
            else if (characterColor == orange)
            {
                m_Rigidbody2D.gameObject.GetComponent<SpriteRenderer>().sprite = orangeSprite;
            }
            else if (characterColor == violet)
            {
                m_Rigidbody2D.gameObject.GetComponent<SpriteRenderer>().sprite = violetSprite;
            }
            else if (characterColor == personalGreen)
            {
                m_Rigidbody2D.gameObject.GetComponent<SpriteRenderer>().sprite = greenSprite;
            }
        }

        private void FixedUpdate()
        {
            m_Grounded = false;
            

            if (characterColor == Color.blue && jumpCount <= 1)
            {
                m_Grounded = true;
            }
            else
            {
                m_Grounded = false;
            }
            
            // The player is grounded if a circlecast to the groundcheck position hits anything designated as ground
            // This can be done using layers instead but Sample Assets will not overwrite your project settings.
            Collider2D[] colliders = Physics2D.OverlapCircleAll(m_GroundCheck.position, k_GroundedRadius, m_WhatIsGround);
            for (int i = 0; i < colliders.Length; i++)
            {
                if (colliders[i].gameObject != gameObject)
                {
                    m_Grounded = true;
                }
            }

            if (m_Grounded)
            {
                timePassedAfterGrounded += Time.deltaTime;                
            }
            else
            {
                timePassedAfterGrounded = 0;
            }

            

            m_Anim.SetBool("Ground", m_Grounded);

            // Set the vertical animation
            m_Anim.SetFloat("vSpeed", m_Rigidbody2D.velocity.y);

            // reaload scene if dies

            if (transform.position.y <= diesOfFalling)
            {
                SceneManager.LoadScene("LoseScene");
            }

            // Sprite changing of player by his color

            ChangeCharacterSprite(characterColor);
        }


        // Shooting the projectile method

        public void Shoot(Color color, float shootForce, Vector3 target)
        {
            Vector2 target2D = new Vector2(target.x, target.y);
            GameObject projectile = Instantiate(prefabProjectile, transform.position, transform.rotation) as GameObject;
            if (this.GetComponent<Platformer2DUserControl>().paintable)
            {
                projectile.gameObject.layer = 16;
                projectile.gameObject.tag = "ProjectileHitCharacter";
                projectile.gameObject.GetComponent<CircleCollider2D>().isTrigger = enabled;
            }
            projectile.GetComponent<SpriteRenderer>().color = color;
            projectile.GetComponent<Rigidbody2D>().AddForce((target2D + -1 * new Vector2(transform.position.x, transform.position.y)).normalized * shootForce);
        }


        public void Move(float move, bool jump)
        {
            //only control the player if grounded or airControl is turned on
            if (m_Grounded || m_AirControl)
            {
                // The Speed animator parameter is set to the absolute value of the horizontal input.
                m_Anim.SetFloat("Speed", Mathf.Abs(move));

                // Move the character
                m_Rigidbody2D.velocity = new Vector2(move*m_MaxSpeed, m_Rigidbody2D.velocity.y);
                if (isInContactWithYellowPlatform)
                {
                    m_Rigidbody2D.velocity += platform.velocity;
                }
                else if (isInContactWithBluePlatform)
                {
                    if(m_Rigidbody2D.velocity.x != 0)
                    {
                        transform.SetParent(null);
                    }
                }

                // If the input is moving the player right and the player is facing left...
                if (move > 0 && !m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
                    // Otherwise if the input is moving the player left and the player is facing right...
                else if (move < 0 && m_FacingRight)
                {
                    // ... flip the player.
                    Flip();
                }
            }
            // If the player should jump...
            if (m_Grounded && jump && m_Anim.GetBool("Ground"))
            {
                // Add a vertical force to the player.

                m_Rigidbody2D.AddForce(new Vector2(0f, m_JumpForce));
                m_Grounded = false;
                m_Anim.SetBool("Ground", false);
                jumpCount++;
            }
        }

        // fliping the sprite of the player method

        private void Flip()
        {
            // Switch the way the player is labelled as facing.
            m_FacingRight = !m_FacingRight;

            // Multiply the player's x local scale by -1.
            Vector3 theScale = transform.localScale;
            theScale.x *= -1;
            transform.localScale = theScale;
        }


        void OnCollisionStay2D(Collision2D coll)     
        {
            if (coll.gameObject.tag == "Platform")
            {
                if (coll.gameObject.GetComponent<SpriteRenderer>().color == Color.red)
                {
                    if (timePassedAfterGrounded >= 3 && !coll.gameObject.GetComponent<PlataformController>().isWall)
                    {
                        m_Rigidbody2D.AddForce(redPlatformForce * Vector2.up);
                    }

                    else if (coll.gameObject.GetComponent<PlataformController>().isWall)
                    {
                        if (transform.position.x < coll.transform.position.x)
                        {
                            m_Rigidbody2D.AddForce(redPlatformForce * new Vector2 (-10000,1));
                        }
                        else
                        {
                            m_Rigidbody2D.AddForce(redPlatformForce * new Vector2(10000, 1));
                        }
                    }
                }
                else if (coll.gameObject.GetComponent<SpriteRenderer>().color == Color.yellow)
                {
                    isInContactWithYellowPlatform = true;
                    platform = coll.gameObject.GetComponent<PlataformController>();
                }
                else if (coll.gameObject.GetComponent<SpriteRenderer>().color == Color.blue)
                {
                    platform = coll.gameObject.GetComponent<PlataformController>();
                    isInContactWithBluePlatform = true;
                    m_Rigidbody2D.gameObject.transform.SetParent(platform.transform);

                }
            }
        }

        private void OnCollisionEnter2D(Collision2D coll)
        {
            if (coll.gameObject.tag == "Platform" && coll.gameObject.GetComponent<SpriteRenderer>().color == Color.yellow)
            {
                platform = coll.gameObject.GetComponent<PlataformController>();
                isInContactWithYellowPlatform = true;
            }
            else if (coll.gameObject.tag == "Platform" && coll.gameObject.GetComponent<SpriteRenderer>().color == Color.blue)
            {
                platform = coll.gameObject.GetComponent<PlataformController>();
                isInContactWithBluePlatform = true;
                m_Rigidbody2D.gameObject.transform.SetParent(platform.transform);
            }

            if (coll.gameObject.tag == "Platform" || coll.gameObject.tag == "Ground")
            {
                jumpCount = 0;
            }
        }

        private void OnCollisionExit2D(Collision2D coll)
        {
            if (coll.gameObject.tag == "Platform")
            {
                if (coll.gameObject.GetComponent<SpriteRenderer>().color == Color.yellow)
                {
                    isInContactWithYellowPlatform = false;
                }
                else if (coll.gameObject.GetComponent<SpriteRenderer>().color == Color.blue)
                {
                    isInContactWithBluePlatform = false;
                    m_Rigidbody2D.gameObject.transform.SetParent(null);
                }
            }
        }

        private void OnTriggerEnter2D(Collider2D collision)
        {
            // painting the character himself
            if (collision.gameObject.tag == "ProjectileHitCharacter")
            {
                Color projectileColor = collision.gameObject.GetComponent<SpriteRenderer>().color;

                if (projectileColor == Color.white)
                {
                    characterColor = Color.white;
                }

                if (projectileColor == Color.red)
                {
                    if (characterColor == Color.white)
                    {
                        characterColor = Color.red;
                    }
                    else if (characterColor == Color.blue)
                    {
                        characterColor = violet;
                    }
                    else if (characterColor == Color.yellow)
                    {
                        characterColor = orange;
                    }
                }
                else if (projectileColor == Color.yellow)
                {
                    if (characterColor == Color.white)
                    {
                        characterColor = Color.yellow;
                    }
                    else if (characterColor == Color.blue)
                    {
                        characterColor = personalGreen;
                    }
                    else if (characterColor == Color.red)
                    {
                        characterColor = orange;
                    }
                }
                else if (projectileColor == Color.blue)
                {
                    if (characterColor == Color.white)
                    {
                        characterColor = Color.blue;
                    }
                    else if (characterColor == Color.yellow)
                    {
                        characterColor = personalGreen;
                    }
                    else if (characterColor == Color.red)
                    {
                        characterColor = violet;
                    }
                }
                Destroy(collision.gameObject);
            }

            // checks if player touched an enemy
            if (collision.gameObject.tag == "Enemy")
            {

            }
        }
    }
}
