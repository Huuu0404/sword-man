using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FoxMovement : MonoBehaviour
{
    public GameObject monster;
    public float minX = -8.1f, maxX = 8.15f; // 角色移動邊界
    public float moveSpeed = 0.04f;
    public float moveTime = 1;
    private int direction;
    private Animator animator;
    // Start is called before the first frame update
    void Start()
    {
        direction = Random.Range(-1,2); // -1, 0, 1
        animator = monster.GetComponent<Animator>();
    }

    // Update is called once per frame
    void Update()
    {
        if(moveTime >= 0)
        {
            //移動動畫
            if(Mathf.Abs(direction)>0){
                animator.SetBool("IsWalking", true);
            }
            else{
                animator.SetBool("IsWalking", false);
            }
            move(direction);
            moveTime -= Time.deltaTime;
        }
        else
        {
            direction = Random.Range(-1,2);
            moveTime = 1;
        }
    }
    void move(int direction)
    {
        //移動位置
        Vector3 newPosition = monster.transform.position + new Vector3(direction*moveSpeed, 0, 0);
        newPosition.x = Mathf.Clamp(newPosition.x, minX, maxX); //防止超出邊界
        monster.transform.position = newPosition;

        // 角色翻面
        if(direction > 0){
            monster.transform.localScale = new Vector3(5f,5f,5f);
        }
        else if(direction < 0){
            monster.transform.localScale = new Vector3(-5f,5f,5f);
        }
    }
}
