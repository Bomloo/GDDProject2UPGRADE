using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class projectile : MonoBehaviour
{
    #region physics
    Rigidbody2D project;
    [SerializeField]
    float deathtimmer;
    #endregion

    #region movment
    private Vector2 dir;
    [SerializeField]
    float movespeed;
    #endregion

    #region damage_var
    [SerializeField]
    int dmgvar;
    #endregion

    #region unity_functs
    // Start is called before the first frame update
    void Start()
    {
        project = GetComponent<Rigidbody2D>();
        //this.transform.position.x = this.transform.position.x - 950;
    }

    // Update is called once per frame
    void Update()
    {
        project.velocity = dir * movespeed;
        deathtimmer -= Time.deltaTime;
        if(deathtimmer <= 0)
        {
            Destroy(this.gameObject);
        }
    }
    #endregion

    #region movefunct
    public void setdir(Vector3 location)
    {
        
        dir = location;
    }
    #endregion

    #region damage 
    private void OnCollisionEnter2D(Collision2D collision)
    {
        Debug.Log("colliding");
        RaycastHit2D[] hits = Physics2D.BoxCastAll(project.position + dir * .1f, Vector2.one, 0f, Vector2.zero, 0);
        Debug.Log(hits);
        foreach (RaycastHit2D hit in hits)
        {
            if (hit.transform.CompareTag("Player"))
            {
                Debug.Log("Git a Hit");
                hit.transform.GetComponent<PlayerController>().takedmg(dmgvar);
                Destroy(this.gameObject);
            } else if (hit.transform.CompareTag("Ground"))
            {
                Destroy(this.gameObject);
            }
            
            
        }
    }
    #endregion

}
