using System.Collections.Generic;
using System;
using UnityEngine;
namespace ClearSky
{
    public class SimplePlayerController : MonoBehaviour
    {
        public float movePower = 10f;
        public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5

        private Rigidbody2D rb;
        private Animator anim;
        private Transform Cntpos;
        Vector3 movement;
        private float direction = 1;
        //bool isJumping = false;
        public bool alive = true;


        // Start is called before the first frame update
        void Start()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
        }

        private void Update()
        {
            Restart();
            if (alive)
            {
                Hurt();
                Die();
                Attack();
                //Jump();
                Run();
                Rotate();
            }
        }
        private void OnTriggerEnter2D(Collider2D other)
        {
            anim.SetBool("isJump", false);
        }


        void Run()
        {
            Vector3 moveVelocity = Vector3.zero;
            anim.SetBool("isRun", false);


            if (Input.GetAxisRaw("Horizontal") < 0)
            {
                direction = -0.5f;
                moveVelocity = Vector3.left;

                transform.localScale = new Vector3(direction, 0.5f, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
            if (Input.GetAxisRaw("Horizontal") > 0)
            {
                direction = 0.5f;
                moveVelocity = Vector3.right;

                transform.localScale = new Vector3(direction, 0.5f, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
            if (Input.GetAxisRaw("Vertical") < 0)
            {
                direction = 0.5f;
                moveVelocity = Vector3.down;

                transform.localScale = new Vector3(0.5f, direction, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
            if (Input.GetAxisRaw("Vertical") > 0)
            {
                direction = 0.5f;
                moveVelocity = Vector3.up;

                transform.localScale = new Vector3(0.5f, direction, 1);
                if (!anim.GetBool("isJump"))
                    anim.SetBool("isRun", true);

            }
            transform.position += moveVelocity * movePower * Time.deltaTime;
        }
        // IEnumerator<WaitForSeconds> Jump()
        // {
        //     if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
        //     && !anim.GetBool("isJump"))
        //     {
        //         isJumping = true;
        //         anim.SetBool("isJump", true);
        //     }
        //     if (!isJumping)
        //     {
        //         yield break;
        //     }

        //     rb.velocity = Vector2.zero;
        //     rb.gravityScale = 10f;
        //     Cntpos = transform;
        //     Vector2 jumpVelocity = new Vector2(0, jumpPower);
        //     rb.AddForce(jumpVelocity, ForceMode2D.Impulse);
        //     yield return new WaitForSeconds(1f);
        //     if (Cntpos.position == transform.position && rb.gravityScale != 0f)
        //     {
        //         rb.gravityScale = 0f;
        //     }
        //     isJumping = false;
        // }
        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.Alpha1))
            {
                anim.SetTrigger("attack");
            }
        }
        void Hurt()
        {
            if (Input.GetKeyDown(KeyCode.Alpha2))
            {
                anim.SetTrigger("hurt");
                if (direction == 1)
                    rb.AddForce(new Vector2(-5f, 1f), ForceMode2D.Impulse);
                else
                    rb.AddForce(new Vector2(5f, 1f), ForceMode2D.Impulse);
            }
        }
        void Die()
        {
            if (Input.GetKeyDown(KeyCode.Alpha3))
            {
                anim.SetTrigger("die");
                alive = false;
            }
        }
        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                anim.SetTrigger("idle");
                alive = true;
            }
        }
        void Rotate()
        {
            //StartCoroutine(Jump());
        }
    }
}