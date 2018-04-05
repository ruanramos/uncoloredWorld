using System;
using UnityEngine;
using UnityEngine.UI;
using UnityStandardAssets.CrossPlatformInput;
using UnityEngine.SceneManagement;

namespace UnityStandardAssets._2D
{
    [RequireComponent(typeof (PlatformerCharacter2D))]
    public class Platformer2DUserControl : MonoBehaviour
    {
        private PlatformerCharacter2D m_Character;
        private bool m_Jump;
        [NonSerialized] public Color selectedColor;
        [SerializeField] private float initialShootForce = 400f;
        private float finalShootForce;
        public float shootButtonPressTime = 0;
        private float timePressed;
        private bool shouldShoot;
        private Vector3 target;
        private float maxChargeTime = 5f;
        [NonSerialized]public float chargeBarFillAmount;
        private Color[] colors = { Color.white, Color.red, Color.yellow, Color.blue };
        [NonSerialized] public int pointer;
        [NonSerialized] public int[] ammo;

        [NonSerialized] public bool paintable = false;

        public bool shouldDash;
        public float dashDistance = 100f;

        Color orange = new Color(255 / 255f, 119 / 255f, 0 / 255f, 255 / 255f);
        Color violet = new Color(128 / 255f, 36 / 255f, 86 / 255f, 255 / 255f);
        Color personalGreen = new Color(128 / 255f, 154 / 255f, 86 / 255f, 255 / 255f);

        private void Awake()
        {
            m_Character = GetComponent<PlatformerCharacter2D>();
        }

        private void Start()
        {
            selectedColor = Color.white;
            pointer = 0;
            ammo = new int[] { 0, 0, 0, 0 };
        }


        private void Update()
        {
            if (!m_Jump)
            {
                // Read the jump input in Update so button presses aren't missed.
                if (m_Character.GetComponent<PlatformerCharacter2D>().characterColor != personalGreen)
                {
                    m_Jump = CrossPlatformInputManager.GetButtonDown("Jump");
                }
            }

            // little cheat to make testing easier
            if (Input.GetKeyDown(KeyCode.T))
            {
                ammo[0] += 10;
                ammo[1] += 10;
                ammo[2] += 10;
                ammo[3] += 10;

                GameObject.Find("WhiteAmmoText").GetComponent<Text>().text = "" + ammo[pointer];
                GameObject.Find("RedAmmoText").GetComponent<Text>().text = "" + ammo[pointer];
                GameObject.Find("YellowAmmoText").GetComponent<Text>().text = "" + ammo[pointer];
                GameObject.Find("BlueAmmoText").GetComponent<Text>().text = "" + ammo[pointer];
            }
            
            // reseting the level
            if (Input.GetKeyDown(KeyCode.R))
            {
                SceneManager.LoadScene("LoseScene");   
            }

            // charging the shoot
            if (Input.GetMouseButton(0))
            {
                // reset charge bar
                if (Input.GetMouseButtonDown(1))
                {
                    shootButtonPressTime = -1f;
                }

                shouldShoot = false;
                if (shootButtonPressTime <= maxChargeTime)
                {
                    shootButtonPressTime += Time.deltaTime * 2;
                }
                else
                {
                    shootButtonPressTime = maxChargeTime;
                }
            }
            else if (shootButtonPressTime > 0)
            {
                shouldShoot = true;
                timePressed = shootButtonPressTime;
                finalShootForce = initialShootForce * timePressed;

                // getting mouse position
                target = Camera.main.ScreenToWorldPoint(Input.mousePosition);
                target.z = transform.position.z;

                // shooting the projectile
                if (ammo[pointer] > 0)
                {
                    m_Character.Shoot(selectedColor, finalShootForce, target);
                    ammo[pointer]--;

                    // atualize ammo at screen
                    switch (pointer)
                    {
                        case 0:

                            GameObject.Find("WhiteAmmoText").GetComponent<Text>().text = "" + ammo[pointer];
                            break;
                        case 1:
                            GameObject.Find("RedAmmoText").GetComponent<Text>().text = "" + ammo[pointer];
                            break;
                        case 2:
                            GameObject.Find("YellowAmmoText").GetComponent<Text>().text = "" + ammo[pointer];
                            break;
                        case 3:
                            GameObject.Find("BlueAmmoText").GetComponent<Text>().text = "" + ammo[pointer];
                            break;
                    }

                }
                shouldShoot = false;
                shootButtonPressTime = 0;
            }

            if (Input.GetKeyDown(KeyCode.LeftShift)){
                shouldDash = true;
            }

            // making the change of colors easy to test while making the game
            /*
            if (Input.GetKey(KeyCode.B))
            {
                selectedColor = Color.blue;
            }
            if (Input.GetKey(KeyCode.Y))
            {
                selectedColor = Color.yellow;
            }
            if (Input.GetKey(KeyCode.R))
            {
                selectedColor = Color.red;
            }
            if (Input.GetKey(KeyCode.W))
            {
                selectedColor = Color.white;
            }
            */

            // making projectile color change ingame
            if (Input.GetAxis("Mouse ScrollWheel") < 0f)
            {
                if(pointer == 0)
                {
                    pointer = 3;
                }
                else if (pointer == 1 || pointer == 2 || pointer == 3)
                {
                    pointer--;
                }
                
            }

            else if (Input.GetAxis("Mouse ScrollWheel") > 0f)
            {
                if (pointer == 3)
                {
                    pointer = 0;
                }
                else if (pointer == 0 || pointer == 1 || pointer == 2)
                {
                    pointer++;
                }
                
            }

            if (Input.GetKeyDown(KeyCode.P))
            {
                paintable = !paintable;
            }

            

            selectedColor = colors[pointer];
        }

        private void FixedUpdate()
        {
            
            // Read the inputs.
            float h = CrossPlatformInputManager.GetAxis("Horizontal");


            // Pass all parameters to the character control script.
            m_Character.Move(h, m_Jump);
            if (shouldDash && m_Character.characterColor == Color.yellow)
            {
                if (this.GetComponent<PlatformerCharacter2D>().m_FacingRight)
                {
                    this.GetComponent<Rigidbody2D>().AddForce(Vector2.right * 1000, ForceMode2D.Impulse);
                    //transform.position = new Vector2(transform.position.x + dashDistance, transform.position.y);
                }
                else
                {
                    this.GetComponent<Rigidbody2D>().AddForce(-Vector2.right * 1000, ForceMode2D.Impulse);
                    //transform.position = new Vector2(transform.position.x - dashDistance, transform.position.y);
                }
            }
            m_Jump = false;
            shouldDash = false;
            chargeBarFillAmount = shootButtonPressTime / maxChargeTime;

            
        }
    }
}
