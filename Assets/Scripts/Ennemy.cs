using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class Ennemy : MonoBehaviour
{
  
    private float maxLife = 3;
    public float currentLife;

    public SpriteRenderer spriteRenderer;

    public GameObject fx_hit;

    public Animator animator;

    int sceneActuelleIndex;

    [SerializeField] SpriteRenderer greySquare;

    [SerializeField] int speedTranslation;

    public float startValue = 0f;
    public float endValue = 1f;
    public float duration = 0.5f;

    



    public bool startscene = false;


    private void Start()
    {
        currentLife = maxLife;
        sceneActuelleIndex = SceneManager.GetActiveScene().buildIndex;
        startscene = true;

      



    }

    private void Update()
    {
        
        
    }

    public void TakeDamage(float d, GameObject collision)
    {
        currentLife -= d;

        if (currentLife <= 0)
        {
            collision.GetComponent<Movement>().damageTaken = true;
            Die();
        }
        else
            animator.SetTrigger("hurt");
    }

    public void Die()
    {
        animator.SetTrigger("die");
        GetComponent<Collider2D>().enabled = false;
        StartCoroutine(LoadNewScene());

        GameManager.instance.greySquare.SetTrigger("greytransition");

    }

    private void OnCollisionEnter2D(Collision2D collision)
    {
        if (collision.gameObject.CompareTag("Balle"))
        {
            TakeDamage(1, collision.gameObject);
            //StartCoroutine(DamageFx());
        }
    }
    
    public IEnumerator LoadNewScene()
    {
        yield return new WaitForSeconds(1.5f);
        SceneManager.LoadScene(sceneActuelleIndex + 1);
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
