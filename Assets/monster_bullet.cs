using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class monster_bullet : MonoBehaviour
{
    private float minX = -8.1f, maxX = 8.15f;
    private float monsterDirection;
    private GameObject monster;
    // Start is called before the first frame update
    void Start()
    {
        monster = GameObject.FindGameObjectWithTag("monster");
    }

    // Update is called once per frame
    void Update()
    {
        if(monster != null)
        {
            bulletMove();
            checkDestroy();
        }
    }
    public void SetMonsterDirection(float direction) // 為射擊的子彈方向做準備
    {
        monsterDirection = direction;
    }
    private void bulletMove()
    {
        this.gameObject.transform.position += new Vector3(0.01f*monsterDirection, 0 ,0);
    }
    private void checkDestroy()
    {
        if (transform.position.x >= maxX || transform.position.x <= minX )
        {
            Destroy(gameObject);
        }
    }
    void OnTriggerEnter2D(Collider2D coll)
    {
        if(coll.gameObject.tag == "Player")
        {
            Destroy(gameObject);
        }
    }
}
