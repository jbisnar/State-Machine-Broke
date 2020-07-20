using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Movement_Sidescroll_Simple : MonoBehaviour
{
    float gravNormal = 24f;
    float gravJump = 24f;
    float gravDown = 24f;
    float gravWall = 0f;
    float gravGlide = 2f;
    float velWalk = 10f;
    float accelWalk = 50f;
    float accelSwitch = 100f;
    float accelSlow = 50f;
    float velAir = 7.5f;
    float accelAir = 30f;
    float accelAirSwitch = 45f;
    float accelAirSlow = 15f;
    float velSlide = 3f;
    float accelSlide = 10f;
    float accelSlideSwitch = 15f;
    float accelSlideSlow = 5f;
    float velGlide = 1f;
    float velJumpGround = 15f;
    float velJumpWallH = 4f;
    float velJumpWallV = 4f;
    float velWallSlideDown = 3f;

    public bool slidepants = false;
    public bool wallshoes = false;
    public bool glidesuit = false;
    public bool swingtie = false;

    float savedVel = 0;
    float savedVelAir = 0;
    float savedVelWallKick = 0;
    public bool gliding = false;
    public bool sliding = false;
    public GameObject standCollide;
    public GameObject slideCollide;

    public bool grounded = false;
    public bool jumping = false;
    public bool walledL = false;
    public bool wallslideL = false;
    public bool walledR = false;
    public bool wallslideR = false;
    float hitboxHeight = 1.8f;
    float hitboxWidth = .9f;

    bool cantslide = false;
    bool cantstand = false;
    float walljumpgrace = .05f;
    float walljumpcontrol = .5f;
    float perfectkickgrace = .1f;
    float walljumpgracetime;
    float walljumpcontroltime;
    float perfectkicktime;
    public LayerMask layerGround;
    public Vector2 temp;

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        temp = transform.GetComponent<Rigidbody2D>().velocity;
        grounded = Physics2D.OverlapArea(new Vector2(transform.position.x - (hitboxWidth/2 -.02f), transform.position.y - (hitboxHeight/2 -.02f)),
            new Vector2(transform.position.x + (hitboxWidth/2 - .02f), transform.position.y - (hitboxHeight/2 + .02f)), layerGround);
        walledL = Physics2D.OverlapArea(new Vector2(transform.position.x - (hitboxWidth/2 + .02f), transform.position.y + (hitboxHeight/2 - .02f)),
            new Vector2(transform.position.x - (hitboxWidth/2 - .02f), transform.position.y - (hitboxHeight/2 - .02f)), layerGround);
        walledR = Physics2D.OverlapArea(new Vector2(transform.position.x + (hitboxWidth/2 - .02f), transform.position.y + (hitboxHeight/2 - .02f)),
            new Vector2(transform.position.x + (hitboxWidth/2 + .02f), transform.position.y - (hitboxHeight/2 - .02f)), layerGround);
        if (walledL || walledR)
        {
            walljumpgracetime = Time.time + walljumpgrace;
        }

        //HORIZONTAL CONTROL
        if (Input.GetAxisRaw("Horizontal") == 0)
        { //Slow down
            if (Mathf.Abs(temp.x) < accelSlow * Time.deltaTime)
            {
                temp.x = 0;
            }
            else if (temp.x > 0)
            {
                if (grounded)
                {
                    temp.x -= accelSlow * Time.deltaTime;
                }
                else if (Time.time > walljumpcontroltime)
                {
                    temp.x -= accelAirSlow * Time.deltaTime;
                }
            }
            else
            {
                if (grounded)
                {
                    temp.x += accelSlow * Time.deltaTime;
                }
                else if (Time.time > walljumpcontroltime)
                {
                    temp.x += accelAirSlow * Time.deltaTime;
                }
            }
        }
        else if (Input.GetAxisRaw("Horizontal") > 0)
        { //Right
            if (walledR)
            {
                temp.x = 0;
            }
            else if (grounded)
            {
                if (temp.x > velWalk)
                {
                    temp.x = velWalk;
                }
                else if (temp.x < 0)
                {
                    temp.x += accelSwitch * Time.deltaTime;
                }
                else
                {
                    temp.x += accelWalk * Time.deltaTime;
                }
                savedVelAir = temp.x;
            }
            else
            {
                if (Time.time < walljumpcontroltime)
                {
                    temp.x = temp.x;
                }
                else if (temp.x > velAir)
                {
                    temp.x = temp.x;
                }
                else if (temp.x < 0)
                {
                    temp.x += accelAirSwitch * Time.deltaTime;
                }
                else
                {
                    temp.x += accelAir * Time.deltaTime;
                }
            }
        }
        else
        { //Left
            if (walledL)
            {
                temp.x = 0;
            }
            else if (grounded)
            {
                if (temp.x < -velWalk)
                {
                    temp.x = -velWalk;
                }
                else if (temp.x > 0)
                {
                    temp.x -= accelSwitch * Time.deltaTime;
                }
                else
                {
                    temp.x -= accelWalk * Time.deltaTime;
                }
            }
            else
            {
                if (Time.time < walljumpcontroltime)
                {
                    temp.x = temp.x;
                }
                else if (temp.x < -velAir)
                {
                    temp.x = temp.x;
                }
                else if (temp.x > 0)
                {
                    temp.x -= accelAirSwitch * Time.deltaTime;
                }
                else
                {
                    temp.x -= accelAir * Time.deltaTime;
                }
            }
        }
        if (temp.x != 0)
        {
            savedVel = temp.x;
        }

        //GRAVITY
        if (grounded)
        {
            jumping = false;
            temp.y = 0;
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                temp.y = velJumpGround;
                jumping = true;
            }
        }
        else if (walledL)
        {
            jumping = false;
            if (Input.GetKeyDown("w") && wallshoes)
            {
                if (savedVel < -velJumpWallH && Time.time < perfectkicktime)
                {
                    temp.x = -savedVel;
                }
                else
                {
                    temp.x = velJumpWallH;
                }
                temp.y = velJumpWallV;
                jumping = true;
                walljumpcontroltime = Time.time + walljumpcontrol;
            }
            if (wallslideL && temp.y < -velWallSlideDown)
            {
                temp.y = -velWallSlideDown;
            }
            else if (Input.GetAxisRaw("Vertical") > 0 && temp.y > 0)
            {
                temp.y -= gravJump * Time.deltaTime;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                jumping = false;
                temp.y -= gravDown * Time.deltaTime;
            }
            else
            {
                jumping = false;
                temp.y -= gravNormal * Time.deltaTime;
            }
        }
        else if (walledR)
        {
            jumping = false;
            if (Input.GetKeyDown("w") && wallshoes)
            {
                if (savedVel > velJumpWallH && Time.time < perfectkicktime)
                {
                    temp.x = -savedVel;
                }
                else
                {
                    temp.x = -velJumpWallH;
                }
                temp.y = velJumpWallV;
                jumping = true;
                walljumpcontroltime = Time.time + walljumpcontrol;
            }
            if (wallslideR && temp.y < -velWallSlideDown)
            {
                temp.y = -velWallSlideDown;
            }
            else if (Input.GetKey("w") && temp.y > 0)
            {
                temp.y -= gravJump * Time.deltaTime;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                jumping = false;
                temp.y -= gravDown * Time.deltaTime;
            }
            else
            {
                jumping = false;
                temp.y -= gravNormal * Time.deltaTime;
            }
        }
        else if (Time.time < walljumpgracetime)
        {
            jumping = false;
            if (Input.GetKeyDown("w") && wallshoes)
            {
                //temp.x = -velJumpWallH;
                temp.y = velJumpWallV;
                jumping = true;
                walljumpcontroltime = Time.time + walljumpcontrol;
            }
        }
        else
        {
            if (Input.GetKey("w") && temp.y > 0 && jumping)
            {
                temp.y -= gravJump * Time.deltaTime;
            }
            else if (Input.GetAxisRaw("Vertical") < 0)
            {
                jumping = false;
                temp.y -= gravDown * Time.deltaTime;
            }
            else
            {
                jumping = false;
                temp.y -= gravNormal * Time.deltaTime;
            }
        }
        transform.GetComponent<Rigidbody2D>().velocity = temp;
    }
}
