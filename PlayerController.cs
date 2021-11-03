using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class PlayerController : MonoBehaviour
{
    public float altura_salto;
    public float velocidad_movimiento;
    private Rigidbody2D rb;
    private bool toco_piso;
    private Animator anim;
    public GameObject Ataque;
    private Vector2 pos_o;
    public const string MUERTE = "Enemy";
    private int vidas = 3;
    public float cronometro;
    public GameObject GameOver;

    public GameObject SonidoSalto;
    public GameObject SonidoGolpe;

    public int Vidas 
    { 
        get => vidas; 
        
        set => vidas = value;
    }

    void Start()
    {
        GameOver.SetActive(false);
        pos_o = this.transform.position;
        rb = GetComponent<Rigidbody2D>();
        anim = GetComponent<Animator>();
        anim.SetInteger("Estado", 1);
        Ataque.GetComponent<BoxCollider2D>().enabled = false;
    }

    void OnCollisionEnter2D(Collision2D c)
    {
        toco_piso = c.gameObject.tag.Equals("Piso");
       
        if (c.transform.tag.Equals("Enemy"))
        {
            if(--vidas > 0)
            {
                this.transform.position = pos_o;
            }
            else
            {
                death();
            }
        }
    }

    void Update()
    {

        if (toco_piso)
        {
            anim.SetInteger("Estado", 0);
        }
        if (Input.GetKey(KeyCode.Space) && toco_piso)
        {
            Instantiate(SonidoSalto);
            rb.velocity = new Vector2(rb.velocity.x, altura_salto);
            toco_piso = false;
            anim.SetInteger("Estado", 2);

        }
        if (Input.GetKey(KeyCode.RightArrow))
        {
            rb.velocity = new Vector2(velocidad_movimiento, rb.velocity.y);
            rb.transform.localScale = new Vector2(1, 1);
            anim.SetInteger("Estado", 1);
        }
        if (Input.GetKey(KeyCode.LeftArrow))
        {
            rb.velocity = new Vector2(-velocidad_movimiento, rb.velocity.y);
            rb.transform.localScale = new Vector2(-1, 1);
            anim.SetInteger("Estado", 1);
        }

        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.Space))
        {
            anim.SetInteger("Estado", 2);
        }
        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.Space))
        {
            anim.SetInteger("Estado", 2);
        }

        if (Input.GetKey(KeyCode.RightArrow) && Input.GetKey(KeyCode.X))
        {
            anim.SetInteger("Estado", 3);
        }
        if (Input.GetKey(KeyCode.LeftArrow) && Input.GetKey(KeyCode.X))
        {
            anim.SetInteger("Estado", 3);
        }

        cronometro += 1 * Time.deltaTime;

        if (Input.GetKeyDown(KeyCode.X))
        {
            Instantiate(SonidoSalto);
            anim.SetInteger("Estado", 3);     
            Ataque.GetComponent<BoxCollider2D>().enabled = true;
        }
        
        if (cronometro >= 1)
        {
                Ataque.GetComponent<BoxCollider2D>().enabled = false;
                cronometro = 0;
         }
    }

    void death()
    {
        StartCoroutine("Respawn");
    }

    IEnumerator Respawn()
    {
        yield return new WaitForSeconds(1.3f);
        Gameover();
        yield return new WaitForSeconds(5.2f);
        GameOver.SetActive(false);

        SceneManager.LoadScene(SceneManager.GetActiveScene().name);

    }
    void Gameover()
    {
        GameOver.SetActive(true);
    }

}

