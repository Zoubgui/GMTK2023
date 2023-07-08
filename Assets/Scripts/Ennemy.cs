using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Ennemy : MonoBehaviour
{

    private float maxLife = 3;
    public float currentLife;

    public SpriteRenderer spriteRenderer;

    public GameObject fx_hit;

    public Animator animator;


    private void Start()
    {
        currentLife = maxLife;
    }

    public void TakeDamage(float d)
    {
        currentLife -= d;

        if (currentLife <= 0)
            Die();
        else
            animator.SetTrigger("hurt");
    }

    public void Die()
    {
        animator.SetTrigger("die");
        //Destroy(this.gameObject);
    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Balle"))
        {
            TakeDamage(1);
            StartCoroutine(DamageFx());
        }
    }
    

    
    public IEnumerator DamageFx()
    {
        Instantiate(fx_hit, transform.position, Quaternion.identity);

        float strob = 0.1f;
        yield return new WaitForSeconds(strob);
        spriteRenderer.color = new Color(255, 255, 0);
        yield return new WaitForSeconds(strob);
        spriteRenderer.color = new Color(255, 0, 255);
        yield return new WaitForSeconds(strob);
        spriteRenderer.color = new Color(0, 255, 255);
        yield return new WaitForSeconds(strob);
        spriteRenderer.color = new Color(255, 255, 255);
    }
}
