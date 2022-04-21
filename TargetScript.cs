using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TargetScript : MonoBehaviour
{
    public float maxHealth;
    public float currentHealth;

    public GameObject limbs;



    public void TakeDamage(float amount)
    {

        if (limbs != null)
        {
            if (currentHealth - amount <= 0f)
            {
                transform.root.gameObject.GetComponent<EnemyController>().health -= currentHealth;
                DestroyLimb();
            }
            else
            {
                currentHealth -= amount;
                transform.root.gameObject.GetComponent<EnemyController>().health -= amount;
            }
        }

        else
        {
            if (amount < maxHealth)
            {
                transform.root.gameObject.GetComponent<EnemyController>().health -= amount;
            }
            else
            {
                transform.root.gameObject.GetComponent<EnemyController>().health -= maxHealth;
            }
        }
        Debug.Log(gameObject.transform.root.name + " hit for " + amount + "damage.");
    }



    void DestroyLimb()
    {
        GetComponent<Collider>().enabled = false;
        Destroy(limbs);
    }
}
