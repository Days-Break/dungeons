using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using ClearSky;
namespace ClearSky
{
    public class Jump : MonoBehaviour
    {
        public float jumpPower = 5f; //Set Gravity Scale in Rigidbody2D Component to 5
        private GameObject obj;
        private Rigidbody rb;
        private Animator anim;
        bool isJumping = false;
        public bool alive = true;
        // Start is called before the first frame update
        void Start()
        {
            obj = GameObject.Find("Wizard Variant");
            rb = GetComponent<Rigidbody>();
            anim = obj.GetComponent<Animator>();
        }

        // Update is called once per frame
        void Update()
        {
            if (alive)
            {
                jump();
            }
        }

        void jump()
        {
            if ((Input.GetButtonDown("Jump") || Input.GetKeyDown(KeyCode.Space))
            && !anim.GetBool("isJump"))
            {
                isJumping = true;
                anim.SetBool("isJump", true);
            }
            if (!isJumping)
            {
                return;
            }
            rb.velocity = Vector3.zero;
            Vector3 jumpVelocity = new Vector3(0, 0, -jumpPower);
            rb.AddForce(jumpVelocity, ForceMode.Impulse);
            isJumping = false;
        }
    }
}

