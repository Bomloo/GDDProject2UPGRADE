using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy : MonoBehaviour
{
    // Start is called before the first frame update
    #region health_var
    [SerializeField]
    int maxhealth;
    int currhealth;
    #endregion

    #region attack_var
    [SerializeField]
    bool boss;
    float attacktimer;
    [SerializeField]
    float attackspeed;
    bool isattacking;
    [SerializeField]
    projectile Ogprojectile;


    #endregion

    #region targeting
    public Transform player;
    #endregion

    #region animation
    Animator anim;
    #endregion

    #region Unity_funcs

    // Update is called once per frame
    private void Start()
    {
        currhealth = maxhealth;
        anim = GetComponent<Animator>();
    }
    void Update()
    {
        
        if(player != null && boss && !isattacking && attacktimer <=0)
        {
            attack();
        }
        else
        {
            attacktimer -= Time.deltaTime;
        }
        
    }
    #endregion

    #region health_funcs
    public void takedamage(int dmg)
    {
        currhealth -= dmg;
        if(currhealth <= 0)
        {
            Destroy(this.gameObject);
            if (boss)
            {
                GameObject gm = GameObject.FindWithTag("GameController");
                gm.GetComponent<GameManager>().WinGame();
            }
        }
    }
    #endregion

    #region attack_funcs
    private void attack()
    {
        
        StartCoroutine(attackrout());
        attacktimer = attackspeed;
    }

    IEnumerator attackrout()
    {
        isattacking = true;
        Transform t = this.transform;
        yield return new WaitForSeconds(.1f);
        projectile p = Instantiate(Ogprojectile, this.transform.position, Quaternion.identity);
        p.setdir(player.position - this.transform.position);
        isattacking = false;
    }

    #endregion

}
