using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class PlayerController : MonoBehaviour
{
    
    #region Unity_physics
    Rigidbody2D PlayerRB;
    #endregion

    #region evolutoin_trackers
    int Jumpcounter;
    int Attackcounter;
    int longcouner;
    [SerializeField]
    int jumpThreshhold;
    [SerializeField]
    int attackThreshhold;
    [SerializeField]
    float jumpgrowth;
    [SerializeField]
    float jumpcap;
    [SerializeField]
    int attackcap;
    #endregion

    #region attack_variables
    [SerializeField]
    float attackTimer;
    [SerializeField]
    int dmgvar;
    Vector2 currdir;
    bool attack;
    [SerializeField]
    float attackspeed;
    #endregion

    #region animation_variables
    Animator anim;
    #endregion

    #region movment_variables
    float x_input;
    float y_input;
    [SerializeField]
    int movespeed;
    public bool isjumping;
    [SerializeField]
    float jumpheight;
    [SerializeField]
    float jumptimer;
    #endregion

    #region health_var
    [SerializeField]
    int maxHealth;
    int currhealth;
    [SerializeField]
    Slider hpslider;
    #endregion

    #region Unity_functions
    void Start()
    {
        PlayerRB = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        currhealth = maxHealth;
        hpslider.value = (currhealth / maxHealth)*10;
        
    }

    // Update is called once per frame
    void Update()
    {
        if (attack)
        {
            
            return;
        }

        x_input = Input.GetAxisRaw("Horizontal");
        y_input = Input.GetAxisRaw("Vertical");
        

        move();

        evolve();


        if (Input.GetKeyDown(KeyCode.L) && attackTimer <= 0)
        {
            attackfunc();
        }
        else
        {
            attackTimer -= Time.deltaTime;
        }
        hpslider.value = ((currhealth*10) / maxHealth);

    }
    #endregion


    #region movement_functions
    private void move()
    {
        PlayerRB.AddForce(new Vector2(x_input*movespeed, 0));
        if (x_input > 0)
        {
            PlayerRB.velocity = new Vector2 (movespeed*Time.deltaTime, PlayerRB.velocity.y);
            currdir = Vector2.right;
            anim.SetBool("Moving", true);
        }
        if(x_input< 0)
        {
            PlayerRB.velocity = new Vector2 (movespeed*Time.deltaTime*-1, PlayerRB.velocity.y);
            currdir = Vector2.left;
            anim.SetBool("Moving", true);
            
        }
        if(x_input == 0)
        {
            PlayerRB.velocity = new Vector2(0, PlayerRB.velocity.y );
            anim.SetBool("Moving", false);
        }
        if (!isjumping && Input.GetKeyDown(KeyCode.Space) && y_input == 0)
        {
            Jumpcounter += 1;
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, jumpheight);
            //PlayerRB.AddForce(new Vector2(0, jumpheight));
        }
        if(isjumping && y_input < 0)
        {
            PlayerRB.velocity = new Vector2(PlayerRB.velocity.x, -4);
            //PlayerRB.AddForce(new Vector2(0, -150));
        }
        anim.SetFloat("Dirx", currdir.x);

    }
    #endregion

    #region evo_funcs
    private void evolve()
    {
        if(Jumpcounter > jumpThreshhold)
        {
            Jumpcounter = 0;
            if (jumpheight + jumpgrowth > jumpcap)
            {
                jumpheight = jumpcap;
            }
            else
            {
                jumpheight += jumpgrowth;
            }
            jumpThreshhold += 2;
        }
        if(Attackcounter > attackThreshhold)
        {
            Attackcounter = 0;
            if(dmgvar + 2 > attackcap)
            {
                dmgvar = attackcap;
            }
            dmgvar += 2;
            attackThreshhold += 2;
        }
    }
    #endregion

    #region health_funct
    
    public void takedmg(int damage)
    {
        currhealth -= damage;
        if(currhealth <= 0)
        {
            Destroy(this.gameObject);
            GameObject gm = GameObject.FindWithTag("GameController");
            gm.GetComponent<GameManager>().LoseGame();
        }
    }

    #endregion

    #region attackc_funcs

    private void attackfunc()
    {
        StartCoroutine(attackRoutine());

        attackTimer = attackspeed;
    }

    IEnumerator attackRoutine()
    {
        attack = true;
        anim.SetTrigger("Attack");

        yield return new WaitForSeconds(1f);


        RaycastHit2D[] hits = Physics2D.BoxCastAll(PlayerRB.position + currdir*.1f, Vector2.one, 0f, Vector2.zero, 0);
        Debug.Log(hits);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Enemy"))
            {
                Debug.Log("Git a Hit");
                hit.transform.GetComponent<Enemy>().takedamage(dmgvar);
            }
        }

        attack = false;


    }

    #endregion
}
