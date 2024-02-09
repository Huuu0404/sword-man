using System.Collections;
using System.Collections.Generic;
using System;
using UnityEngine;

public class healthBar_monster : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject Player;
    public GameObject fox;
    private float max_HP = 150;
    private float current_HP;
    private GameObject sword;
    private GameObject playerObject;
    private bool isAttacking = false;
    private float lastAttackTime = 0f;
    private float attackCooldown = 0.1f;
    // public event Action OnDeath_monster;
    // Start is called before the first frame update
    void Start()
    {
        current_HP = max_HP;
        sword = GameObject.FindGameObjectWithTag("sword");
        playerObject = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        //判斷角色死亡
        if(current_HP <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "sword" && !isAttacking && Time.time - lastAttackTime > attackCooldown)
        {
            isAttacking = true;
            lastAttackTime = Time.time;

            if(playerObject != null)
            {
                PlayerMovement playerController = playerObject.GetComponent<PlayerMovement>();
                if (playerController != null && Mathf.Approximately(playerController.GetHorizontalInput(), 0f))
                {
                    current_HP -= 10;
                    //血條外觀扣血
                    float x_scale = (float)current_HP/ (float)max_HP;
                    healthBar.transform.localScale = new Vector3(
                        x_scale,
                        healthBar.transform.localScale.y,
                        healthBar.transform.localScale.z
                    );
                }
            }
            isAttacking = false;
        }
        if(coll.gameObject.tag == "bullet")
        {
            current_HP -= 5;
            //血條外觀扣血
            float x_scale = (float)current_HP/ (float)max_HP;
            healthBar.transform.localScale = new Vector3(
                x_scale,
                healthBar.transform.localScale.y,
                healthBar.transform.localScale.z
            );
        }
    }
}
