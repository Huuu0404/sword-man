using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MonsterMovement : MonoBehaviour
{
    public GameObject monster;
    public float minX = -8.1f, maxX = 8.15f; // 角色移動邊界
    public float moveSpeed = 0.015f;
    public float moveTime = 1;
    private int direction;
    private Animator animator;
    public GameObject monster_bullet;
    public GameObject foxPrefab;
    private float spawnFoxCooldown = 3.0f;
    private float spawnFoxTimer = 0f;
    private float shootCooldown = 2.0f;
    private float shootTimer = 0f;
    private List<GameObject> foxCount = new List<GameObject>();

    // Start is called before the first frame update
    void Start()
    {
        direction = Random.Range(-1,2); // -1, 0, 1
        animator = GetComponent<Animator>();
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

        //使用技能
        spawnFoxTimer -= Time.deltaTime;
        shootTimer -= Time.deltaTime;

        removeFox();
        if (spawnFoxTimer <= 0)
        {
            float randomValue = Random.value; // 0~1

            if (randomValue > 0.5f && foxCount.Count <2)
            {
                skillSpawnFox();
            }

            spawnFoxTimer = spawnFoxCooldown;
        }

        if (shootTimer <= 0)
        {
            float randomValue = Random.value; // 0~1

            if (randomValue < 0.65f)
            {
                skillShoot();
            }

            shootTimer = shootCooldown;
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

    private void skillShoot()
    {
        GameObject newBullet = Instantiate(monster_bullet, this.transform.position, Quaternion.identity);
        
        float monsterDirection = transform.localScale.x;
        newBullet.GetComponent<monster_bullet>().SetMonsterDirection(monsterDirection);

        newBullet.SetActive(true);
    }
    private void skillSpawnFox()
    {
        float randomX = Random.Range(-8.1f, 8.15f);
        GameObject newFox = Instantiate(foxPrefab, new Vector3(randomX, 0, 0), Quaternion.identity);
        newFox.SetActive(true);
        foxCount.Add(newFox);
    }
    private void removeFox()
    {
        for(int i = foxCount.Count - 1; i >= 0; i--)
        {
            if (foxCount[i] == null)
            {
                foxCount.RemoveAt(i);
            }
        }
    }
}
