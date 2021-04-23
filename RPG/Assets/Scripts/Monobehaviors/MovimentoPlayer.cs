using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class MovimentoPlayer : MonoBehaviour
{
    public float velocidadeMovimento = 3.0f;            // equivale ao movimento (impulso) a ser dado ao player
    Vector2 Movimento = new Vector2();                  // detectar movimento pelo teclado

    Animator animator;                                  // guarda a componente do controlador de animação
    //string estadoAnimacao = "EstadoAnimacao";           // guarda o nome do parametro da animação     // descenessário no Blend Tree

    Rigidbody2D rb2D;                                   // guarda a componente CorpoRigido do Player

    

    /*enum EstadosCaracteres {
        andaLeste = 1,
        andaOeste = 2,
        andaNorte = 3,
        andaSul = 4,
        idle = 5
    }*/

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        rb2D = GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        UpDateEstado();
    }

    private void FixedUpdate() {
       MoveCaractere();
    }

    private void MoveCaractere() {
        Movimento.x = Input.GetAxisRaw("Horizontal");
        Movimento.y = Input.GetAxisRaw("Vertical");
        Movimento.Normalize();
        rb2D.velocity = Movimento * velocidadeMovimento;
    }

        /* desnecessario
    private void UpdateEstado() {

        if (Movimento.x > 0) {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaracteres.andaLeste);
        }else if(Movimento.x < 0) {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaracteres.andaOeste);
        }else if(Movimento.y < 0) {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaracteres.andaNorte);
        }else if(Movimento.y > 0) {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaracteres.andaSul);
        } else {
            animator.SetInteger(estadoAnimacao, (int)EstadosCaracteres.idle);
        }
    }
        */
    
    public void UpDateEstado() {
        if(Mathf.Approximately(Movimento.x, 0) && (Mathf.Approximately(Movimento.y, 0))) {
            animator.SetBool("Caminhando", false);
        } else {
            animator.SetBool("Caminhando", true);
        }
        animator.SetFloat("dirX", Movimento.x);
        animator.SetFloat("dirY", Movimento.y);
    }
}
