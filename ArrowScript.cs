using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class ArrowScript : MonoBehaviour
{
    public static ArrowScript instance;

    private Rigidbody2D rb;
    private float force = 850f;
    private bool facingRight;
    private float damage;
    
    


    private void Awake() {
        if(instance == null)instance = this;
    }

    private void Update() { //Real Rotation Effect
        Vector2 velocity = rb.velocity;
        float angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        angle = Mathf.Atan2(velocity.y, velocity.x) * Mathf.Rad2Deg;
        transform.rotation = Quaternion.AngleAxis(angle, Vector3.forward);  
        if(!facingRight){
            this.gameObject.transform.localScale = new Vector3(-this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
        }
        
    }

    public void arrowScript(bool right, float dmg){
        facingRight = right;
        damage = dmg;
        this.gameObject.SetActive(true);
        rb = GetComponent<Rigidbody2D>();
        if(right){
            rb.AddForce(new Vector2(force, 0));
        }else{
            this.gameObject.transform.localScale = new Vector3(-this.gameObject.transform.localScale.x, this.gameObject.transform.localScale.y, this.gameObject.transform.localScale.z);
            rb.AddForce(new Vector2(-force, 0));
        }
    }

    private void OnTriggerEnter2D(Collider2D other) {
        
    }

    private void OnCollisionEnter2D(Collision2D other) {
        switch(other.gameObject.tag){
            case "Hero":
                other.gameObject.GetComponent<Player>().TakeDamage(damage);
                Destroy(this.gameObject);
            break;

            case "Ground":
                Destroy(this.gameObject);
            break;

            case "Delimitator":
                Destroy(this.gameObject);
            break;
        }
    }
}
