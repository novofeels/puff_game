using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class Health : MonoBehaviour
{
[SerializeField] GameObject DeadPuff;

    void Update() 
    {
        
    }
    public int health = 50;

   void OnTriggerEnter2D(Collider2D other) {
    DamageDealer damageDealer = other.gameObject.GetComponent<DamageDealer>();  

    if(damageDealer != null) {
        TakeDamage(damageDealer.GetDamage());
        damageDealer.Hit();
        
    }
}

void TakeDamage(int damage) {
    health -= damage;
    Debug.Log("Health: " + health);
    if(health <= 0) {
       
            Destroy(gameObject);
            Instantiate(DeadPuff, transform.position, Quaternion.identity);
            Rigidbody2D rb = DeadPuff.GetComponent<Rigidbody2D>();
            rb.AddForce(new Vector2(-1, 0));
            FindObjectOfType<LevelManager>().LoadDeathMenu();
        
    }
}

public int GetHealth() {
    return health;
}   
}