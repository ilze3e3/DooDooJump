using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DooDoo_Jumper : MonoBehaviour
{
    float jumpHeight;
    Rigidbody2D rb2D;
    public Camera mc;

    public GameObject rW;
    public GameObject lW;
    public GameObject deathBlock;
    [SerializeField] private float cameraOffset = 2;
    BoxCollider2D dB;
    GameObject currPlatform;

    public bool dooDooGoJump = false;

    public SpawnThyPlatforms spawner;
   
    // Start is called before the first frame update
    void Start()
    {
        rb2D = this.GetComponent<Rigidbody2D>();
        dB = deathBlock.GetComponent<BoxCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetAxis("Horizontal") > 0)
        {
            rb2D.AddForce(new Vector2(2, 0));
        }
        if (Input.GetAxis("Horizontal") < 0)
        {
            rb2D.AddForce(new Vector2(-2, 0));
        }

        if (dooDooGoJump)
        {
            rb2D.velocity += Vector2.up * jumpHeight;
            dooDooGoJump = false;
        }

        if (this.transform.position.y > mc.transform.position.y - cameraOffset) 
        {
            mc.transform.position = new Vector3(mc.transform.position.x, this.transform.position.y + cameraOffset, -4);
            lW.transform.position = new Vector2(lW.transform.position.x, mc.transform.position.y);
            rW.transform.position = new Vector2(rW.transform.position.x, mc.transform.position.y);
            deathBlock.transform.position = new Vector2(deathBlock.transform.position.x, mc.transform.position.y - 5.3f);
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        switch(collision.gameObject.tag)
        {
            case "Platform":
                /// Player has hit a platform. grab the jumpheight and make em jump
                /// 
                if(collision.relativeVelocity.y >= 0 ) // TODO: test whether magnitude needs to be negative or positive
                {
                    jumpHeight = collision.gameObject.GetComponent<Platform>().OnLand();
                    dooDooGoJump = true;
                    spawner.SpawnPlatforms();
                }
                
                break;
            case "StartPlatform":
                if (collision.relativeVelocity.magnitude >= 0) // TODO: test whether magnitude needs to be negative or positive
                {
                    jumpHeight = collision.gameObject.GetComponent<Platform>().OnLand();
                    dooDooGoJump = true;
                }
                break;
            case "SideWall":
                /// Player has hit the side wall. Teleport to the other side wall
                if(collision.gameObject.name.Contains("RightWall")) 
                {
                    /// Teleport to the same position on the left wall. Use the x position of the wall + the offset of the scale of the x scale.
                    this.transform.position = new Vector2(lW.transform.position.x + lW.transform.localScale.x / 2 + this.transform.localScale.x, this.transform.position.y); 

                }
                else if (collision.gameObject.name.Contains("LeftWall"))
                {
                    /// Teleport to the same position on the right wall. Use the x position of the wall + the offset of the scale of the x scale.
                    this.transform.position = new Vector2(rW.transform.position.x - rW.transform.localScale.x / 2 - this.transform.localScale.x, this.transform.position.y); 
                }
                break;
            case "Death":
                /// Player has fallen off of the screen. Make em die. 
                break;
        }
    }
}
