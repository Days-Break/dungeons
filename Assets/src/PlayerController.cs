using System.Collections.Generic;
using System.Collections;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using ClearSky;
namespace ClearSky
{
    public class PlayerController : MonoBehaviour
    {
        public delegate void PlayerListener();
        public event PlayerListener PlayerAttack;
        public Enemy enemy;
        public float movePower = 10f;
        public float jumpPower = 15f; //Set Gravity Scale in Rigidbody2D Component to 5
        private int level;
        public float exp;
        public float expMax;
        public float attackrange;
        public float HP;
        public float HPMax;
        public float ATK;
        public float DEF;
        private Rigidbody2D rb;
        private Animator anim;
        private Transform Cntpos;
        Vector3 movement;
        private float direction = 1;
        //bool isJumping = false;
        public bool alive = true;
        private Slider hslider;
        private Text hptext;
        private Slider eslider;
        private Text eptext;


        // Start is called before the first frame update
        void Start()
        {
            enemy.EnemyAttack += Hurt;
            enemy.EnemyDied += AddEXP;
            UpdateHP(); UpdateEXP();
        }
        void Awake()
        {
            rb = GetComponent<Rigidbody2D>();
            anim = GetComponent<Animator>();
            HPMax = 75;
            HP = HPMax;
            level = 1;
            expMax = 20;
            exp = 0;
            ATK = 10;
            DEF = 5;
            hslider = GameObject.Find("HP").transform.GetComponent<Slider>();
            hptext = GameObject.Find("HP").transform.GetChild(2).GetComponent<Text>();
            eslider = GameObject.Find("EXP").transform.GetComponent<Slider>();
            eptext = GameObject.Find("EXP").transform.GetChild(2).GetComponent<Text>();
        }
        private void Update()
        {
            Restart();
            if (alive)
            {
                // Die();
                // Hurt();
                //Jump();
                Attack();
                Run();
                Upgrade();
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
        void Attack()
        {
            if (Input.GetKeyDown(KeyCode.J))
            {
                anim.SetTrigger("attack");
                PlayerAttack();
            }
        }
        public float Damage(float DEF)
        {
            return Random.Range(ATK * 0.7f, ATK * 1.3f) - Random.Range(DEF * 0.5f, DEF);
        }
        void Hurt()
        {
            // if (Input.GetKeyDown(KeyCode.Alpha2))
            // {
            anim.SetTrigger("hurt");
            if (direction == 1)
                rb.AddForce(new Vector2(-2f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(2f, 1f), ForceMode2D.Impulse);
            HP -= enemy.Damage(DEF);
            UpdateHP();
            if (HP <= 0)
            {
                Die();
            }
            // }
        }
        void Hurt(float x)
        {
            // if (Input.GetKeyDown(KeyCode.Alpha2))
            // {
            anim.SetTrigger("hurt");
            if (direction == 1)
                rb.AddForce(new Vector2(-2f, 1f), ForceMode2D.Impulse);
            else
                rb.AddForce(new Vector2(2f, 1f), ForceMode2D.Impulse);
            HP -= x;
            UpdateHP();
            if (HP <= 0)
            {
                Die();
            }
            // }
        }
        void Die()
        {
            // if (Input.GetKeyDown(KeyCode.Alpha3))
            // {
            anim.SetTrigger("die");
            alive = false;
            // }
        }
        void Restart()
        {
            if (Input.GetKeyDown(KeyCode.Alpha0))
            {
                anim.SetTrigger("idle");
                alive = true;
            }
        }
        void UpdateHP()
        {
            hslider.value = HP / HPMax;
            hptext.text = HP.ToString() + "/" + HPMax.ToString();
        }
        void UpdateEXP()
        {
            eslider.value = exp / expMax;
            eptext.text = exp.ToString() + "/" + expMax.ToString();
        }
        void AddEXP()
        {
            exp += enemy.Exp;
            UpdateEXP();
        }
        void AddEXP(float x)
        {
            exp += x;
            UpdateEXP();
        }
        void Upgrade()
        {
            if (exp > expMax)
            {
                level++;
                exp -= expMax;
                expMax = level * 100 * Mathf.Log(2, level);
                HPMax += 10 * level;
                HP = HPMax;
                ATK += level;
                DEF += 0.5f * level;
                UpdateHP();
                UpdateEXP();
            }
        }
        void Rotate()
        {
            //StartCoroutine(Jump());
        }
    }
}