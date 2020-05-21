using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerController : MonoBehaviour
{
    [SerializeField]
    private float movespeed;

    [SerializeField]
    private ControlManager cmn;

    [SerializeField]
    private bool lookingRight = true; 

    private float horizontal;

    private Rigidbody2D prb;

    private Animator pan;

    private bool afk = false;

    private bool crouched = false;

    [SerializeField]
    private LayerMask groundType;

    [SerializeField]
    private GameObject groundChecker;

    [SerializeField]
    private float radiusGroundCheck;

    [SerializeField]
    private float jumpHeight;

    private void Start()
    {
        prb = GetComponent<Rigidbody2D>();
        pan = GetComponent<Animator>();
    }

    private void Update()
    {
        Crouch();
        Jump();
        Run();
        AnimationManager();
    }

    private void AnimationManager()
    {
        pan.SetFloat("movespeed", Mathf.Abs(horizontal));
        pan.SetBool("afk", afk);
        pan.SetBool("grounded", Grounded());
        pan.SetFloat("jump idle", prb.velocity.y);
        pan.SetBool("crouched", crouched);
    }

    private void Run()
    {
        horizontal = cmn.GetAxis("Horizontal");
        if(!afk)
        {
            prb.velocity = new Vector2(movespeed * horizontal, prb.velocity.y);
        }
        else 
        {
            if(horizontal != 0)
            {
                pan.SetTrigger("btk");
            }
        }

        if((horizontal > 0 && !lookingRight) || (horizontal < 0 && lookingRight))
        {
            Flip();
        }
    }

    private void Jump()
    {
        if(Grounded())
        {
            if(cmn.GetButtonDown(2))
            {
                crouched = false;
                prb.velocity = new Vector2(prb.velocity.x, jumpHeight);
            }
        }
    }

    private void Crouch()
    {
        if(cmn.GetButtonDown(3) && horizontal == 0)
        {
            if(!crouched)
            {
                pan.SetTrigger("s2c");
            }
            else 
            {
                pan.SetTrigger("c2s");
            }
        }
    }

    private void SetAfk()
    {
        afk = !afk;
    }

    private void SetCrouch()
    {
        crouched = !crouched;
    }

    private void Flip()
    {
        Vector3 lscale = transform.localScale;
        lscale.x *= -1;
        transform.localScale = lscale;
        lookingRight = !lookingRight;
    }

    public bool Grounded()
    {
        return Physics2D.OverlapCircle(groundChecker.transform.position,
                                       radiusGroundCheck, groundType);
    }
}
