using System.Collections;
using System.Collections.Generic;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
public class DooDoo_Jumper : MonoBehaviour
{
    float jumpHeight;
    Rigidbody2D rb;
    public Camera mc;
    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetAxis("Horizontal") > 0)
        {
            rb.AddForce(new Vector2(5, 0));
        } 
        if(Input.GetAxis("Horizontal") < 0)
        {
            rb.AddForce(new Vector2(-5, 0));
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        
        switch(collision.gameObject.tag)
        {
            case "Platform":
                /// Player has hit a platform. grab the jumpheight and make em jump
                if(collision.relativeVelocity.magnitude <= 0) // TODO: test whether magnitude needs to be negative or positive
                {
                    jumpHeight = collision.gameObject.GetComponent<Platform>().OnLand();
                    rb.AddForce(new Vector2(0, jumpHeight));
                    mc.transform.position = new Vector3(this.transform.position.x, this.transform.position.y + 4, -4);
                }
                break;
            case "SideWall":
                /// Player has hit the side wall. Teleport to the other side wall
                if(collision.gameObject.name.Contains("RightWall")) 
                {
                    /// Teleport to the same position on the left wall. Use the x position of the wall + the offset of the scale of the x scale.
                }
                else if (collision.gameObject.name.Contains("LeftWall"))
                {
                    /// Teleport to the same position on the right wall. Use the x position of the wall + the offset of the scale of the x scale.
                }
                break;
            case "Death":
                /// Player has fallen off of the screen. Make em die. 
                break;
        }
    }
}
