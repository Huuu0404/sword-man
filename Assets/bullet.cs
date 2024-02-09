using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class bullet : MonoBehaviour
{
    private float minX = -8.1f, maxX = 8.15f;
    private float playerDirection;
    private GameObject Player;
    // Start is called before the first frame update
    void Start()
    {
        Player = GameObject.FindGameObjectWithTag("Player");
    }

    // Update is called once per frame
    void Update()
    {
        if(Player != null)
        {
            bulletMove();
            checkDestroy();
        }
    }

    public void SetPlayerDirection(float direction) // 為射擊的子彈方向做準備
    {
        playerDirection = direction;
    }
    private void bulletMove()
    {
        this.gameObject.transform.position += new Vector3(-0.15f*playerDirection, 0 ,0);
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
        if(coll.gameObject.tag == "Fox" || coll.gameObject.tag =="monster")
        {
            Destroy(gameObject);
        }
    }
}
