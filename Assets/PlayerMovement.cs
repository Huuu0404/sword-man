using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerMovement : MonoBehaviour
{
    public float moveSpeed = 0.07f; //左右移動速度
    public float minX = -8.1f, maxX = 8.15f; // 角色移動邊界
    public float jumpForce = 5.5f; //跳躍力道
    private bool isJumping = false; //防止二段跳
    private Rigidbody2D rb;
    private SpriteRenderer spriteRenderer;
    private Animator animator;
    [SerializeField] private float attackCooldown = 1.0f; // 攻擊冷卻時間
    private float cooldownTimer = Mathf.Infinity;
    public GameObject healthBar;
    public GameObject Player;
    public GameObject lose_button;
    public GameObject bullet;
    private float max_HP = 100;
    private float current_HP;

    // Start is called before the first frame update
    void Start()
    {
        rb = GetComponent<Rigidbody2D>();
        spriteRenderer = GetComponent<SpriteRenderer>();
        animator = GetComponent<Animator>();
        current_HP = max_HP;
    }



    // Update is called once per frame
    void Update()
    {
        //判斷角色死亡
        if(current_HP <= 0)
        {
            Destroy(Player);
            Time.timeScale = 0.0f;
            lose_button.SetActive(true);
        }
        // 左右移動
        float horizontaInput = Input.GetAxis("Horizontal");
        if(Mathf.Abs(horizontaInput)>0){
            animator.SetTrigger("walk");
        }
        else{
            animator.SetBool("IsWalking", false);
        }
        // animator.SetBool("IsWalking", Mathf.Abs(horizontaInput)>0);

        Vector3 newPosition = transform.position + new Vector3(horizontaInput*moveSpeed, 0, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
        transform.position = newPosition;

        // // 角色翻面
        if(horizontaInput > 0){
            transform.localScale = new Vector3(-1.6f,1.6f,1.6f);
        }
        else if(horizontaInput < 0){
            transform.localScale = new Vector3(1.6f,1.6f,1.6f);
        }
        
        // 跳躍
        if(Input.GetKeyDown(KeyCode.LeftAlt)){
            if(!isJumping){
                rb.AddForce(new Vector2(0, jumpForce), ForceMode2D.Impulse);
                isJumping = true;
            }
        }

        // 攻擊
        cooldownTimer += Time.deltaTime;
        if(Input.GetKeyDown(KeyCode.LeftControl) && cooldownTimer>attackCooldown && horizontaInput==0)
        {
            animator.SetTrigger("attack");
            shootBullet();
            cooldownTimer = 0;
        }
    }

    private void shootBullet()
    {
        GameObject newBullet = Instantiate(bullet, this.transform.position, Quaternion.identity);
        
        float playerDirection = transform.localScale.x;
        newBullet.GetComponent<bullet>().SetPlayerDirection(playerDirection);

        newBullet.SetActive(true);
    }

    private void OnCollisionStay2D(Collision2D collision) 
    {
        if(collision.gameObject.CompareTag("Ground")){
            isJumping = false;
        }
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if(collision.gameObject.tag == "monster" || collision.gameObject.tag == "Fox")
        {
            //角色往後退
            float backwardDistance = 2.0f;
            Vector2 collisionNormal = collision.GetContact(0).normal;
            Vector3 newPosition = transform.position + new Vector3(collisionNormal.x * backwardDistance, 0, 0);
            newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX);
            transform.position = newPosition;
            //血條外觀扣血
            current_HP -= 5;
            float x_scale = (float)current_HP/ (float)max_HP;
            healthBar.transform.localScale = new Vector3(
                x_scale,
                healthBar.transform.localScale.y,
                healthBar.transform.localScale.z
            );
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "monster_bullet")
        {
            //血條外觀扣血
            current_HP -= 10;
            float x_scale = (float)current_HP/ (float)max_HP;
            healthBar.transform.localScale = new Vector3(
                x_scale,
                healthBar.transform.localScale.y,
                healthBar.transform.localScale.z
            );
        }
    }
    public float GetHorizontalInput()
    {
        return Input.GetAxis("Horizontal");
    }
}
