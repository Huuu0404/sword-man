using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class healthBar_green : MonoBehaviour
{
    public GameObject healthBar;
    public GameObject Player;
    private float max_HP = 100;
    private float current_HP;
    // Start is called before the first frame update
    void Start()
    {
        current_HP = max_HP;
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Z))
        {
            current_HP -= 10;
            //血條外觀扣血
            float x_scale = (float)current_HP/ (float)max_HP;
            healthBar.transform.localScale = new Vector3(
                x_scale,
                healthBar.transform.localScale.y,
                healthBar.transform.localScale.z
            );
            //判斷角色死亡
            if(current_HP <= 0)
            {
                Destroy(Player);
            }
        }
    }
}
