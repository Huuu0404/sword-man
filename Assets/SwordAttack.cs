using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class SwordAttack : MonoBehaviour
{
    public GameObject sword;
    public GameObject monster;
    public GameObject monsterHP;
    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    void OnCollisionEnter2D(Collision2D obj)
    {
        print(obj.gameObject.name);
        // if(obj.gameObject.tag == "sword")
        // {
        //     current_HP -= 10;
        //     //血條外觀扣血
        //     float x_scale = (float)current_HP/ (float)max_HP;
        //     healthBar.transform.localScale = new Vector3(
        //         x_scale,
        //         healthBar.transform.localScale.y,
        //         healthBar.transform.localScale.z
        //     );
        //     //判斷角色死亡
        //     if(current_HP <= 0)
        //     {
        //         Destroy(Player);
        //     }
        // }
    }
}
