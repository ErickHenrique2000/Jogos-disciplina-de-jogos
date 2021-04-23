using System.Collections;
using UnityEngine;

[RequireComponent(typeof(Rigidbody2D))]
[RequireComponent(typeof(CircleCollider2D))]
[RequireComponent(typeof(Animator))]

public class Perambular : MonoBehaviour
{

    public  float   velocidadePerseguicao;      // velocidade do inimigo na área de spot
    public  float   velocidadePerambular;       // velocidade do inimigo passeando
    float   velocidadeCorrente;                 // velocidade do inimigo atribuida

    public  float   intervaloMudancaDirecao;    // tempo para alterar a direção
    public  bool    perseguePlayer;             // indicador de perseguidor ou não

    Coroutine MoverCoroutine;

    Rigidbody2D rd2D;                           // armazena o componente rigidbody2D
    Animator    animator;                       // armazena o componente animator

    Transform   alvoTransform = null;           // armazena o componente transfrom do alvo

    Vector3     posicaoFinal;                   
    float   anguloAtual = 0;                    // Angulo atribuido 

    CircleCollider2D circleCollider;            // armazena o componente de Spot

    // Start is called before the first frame update
    void Start()
    {
        animator = GetComponent<Animator>();
        velocidadeCorrente = velocidadePerambular;
        rd2D = GetComponent<Rigidbody2D>();
        StartCoroutine(RotinaPerambular());
        circleCollider = GetComponent<CircleCollider2D>();
    }

    // Update is called once per frame
    void Update()
    {
        Debug.DrawLine(rd2D.position, posicaoFinal, Color.red);
    }

    IEnumerator RotinaPerambular() {

        while (true) {
            EscolheNovoPontoFinal();
            if (MoverCoroutine != null) {
                StopCoroutine(MoverCoroutine);
            }
            MoverCoroutine = StartCoroutine(Mover(rd2D, velocidadeCorrente));
            yield return new WaitForSeconds(intervaloMudancaDirecao);
        }

    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if(collision.gameObject.CompareTag("Player") && perseguePlayer){
            velocidadeCorrente = velocidadePerseguicao;
            alvoTransform = collision.gameObject.transform;
            if(MoverCoroutine != null) {
                StopCoroutine(MoverCoroutine);
            }
            MoverCoroutine = StartCoroutine(Mover(rd2D, velocidadeCorrente));
        }
    }

    private void OnTriggerExit2D(Collider2D collision) {
        if (collision.gameObject.CompareTag("Player")) {
            animator.SetBool("Caminhando", false);
            velocidadeCorrente = velocidadePerambular;
            if (MoverCoroutine != null) {
                StopCoroutine(MoverCoroutine);
            }
            alvoTransform = null;
            MoverCoroutine = StartCoroutine(Mover(rd2D, velocidadeCorrente));
        }
    }

    public void EscolheNovoPontoFinal() {
        anguloAtual += Random.Range(0, 360);
        anguloAtual = Mathf.Repeat(anguloAtual, 360);
        posicaoFinal += Vector3ParaAngulo(anguloAtual);
    }

    public Vector3 Vector3ParaAngulo(float anguloEntradaGraus) {
        float anguloEntradaRadianos = anguloEntradaGraus * Mathf.Deg2Rad;
        return new Vector3(Mathf.Cos(anguloEntradaRadianos), Mathf.Sin(anguloEntradaRadianos), 0);
    }

    public IEnumerator Mover(Rigidbody2D rbParaMover, float velocidade) {

        float distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude;

        while(distanciaFaltante > float.Epsilon) {
            if (alvoTransform != null) {
                posicaoFinal = alvoTransform.position;
            }

            if(rbParaMover != null) {
                animator.SetBool("Caminhando", true);
                Vector3 novaPosicao = Vector3.MoveTowards(rbParaMover.position, posicaoFinal, velocidade*Time.deltaTime);
                rd2D.MovePosition(novaPosicao);
                distanciaFaltante = (transform.position - posicaoFinal).sqrMagnitude;
            }
            yield return new WaitForFixedUpdate();
        }
        animator.SetBool("Caminhando", false);

    }

    private void OnDrawGizmos() {
        if(circleCollider != null) {
            Gizmos.DrawWireSphere(transform.position, circleCollider.radius);
        }
    }
}
