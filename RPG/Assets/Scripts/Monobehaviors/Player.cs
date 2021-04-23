using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class Player : Caractere
{

    public HealthBar healthBarPrefab;               // referencia ao objeto prefab criado na HealthBar
    HealthBar healthBar;
    public PontosDano pontosDano;                   // Tem o valor da "saúde" do objeto
    public Inventario inventarioPrefab;             // referencia ao objeto prefab do Inventario
    Inventario inventario;

    private void Start() {
        pontosDano.valor = inicioPontosDano;
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;

        inventario = Instantiate(inventarioPrefab);
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        if (collision.CompareTag("Coletavel")) {

            Item DanoObjeto = collision.GetComponent<Consumable>().item;
            if(DanoObjeto != null) {
                bool deveDesaparecer = false;
                print("Acertou: " + DanoObjeto.NomeObjeto);

                switch (DanoObjeto.tipoItem) {
                    case Item.TipoItem.MOEDA:
                        //deveDesaparecer = true;
                        deveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;
                    case Item.TipoItem.HEALTH:
                        deveDesaparecer = AjustePontosDano(DanoObjeto.quantidade);
                        break;
                    case Item.TipoItem.SWORD:
                        deveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;
                    case Item.TipoItem.SHIELD:
                        deveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;
                    case Item.TipoItem.POTION:
                        deveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;
                    case Item.TipoItem.BOOT:
                        deveDesaparecer = inventario.AddItem(DanoObjeto);
                        break;
                    default:
                        break;
                }
                if (deveDesaparecer) {
                    collision.gameObject.SetActive(false);
                }
            }

        }
    }

    public bool AjustePontosDano(int quantidade) {
        if(pontosDano.valor < MaxPontosDano) { 
            print("oi");
            pontosDano.valor += quantidade;
            print("Ajustando PD por: " + quantidade + ". Novo Valor = " + pontosDano.valor);
            return true;
        } else {
            return false;
        }
    }

    public override IEnumerator DanoCaractere(int dano, float intervalo) {
        while (true) {
            StartCoroutine(FlickerCaractere());
            pontosDano.valor = pontosDano.valor - dano;
            if(pontosDano.valor <= float.Epsilon) {
                KillCaractere();
                SceneManager.LoadScene("Lab5_GameOver");
                break;
            }
            if(intervalo > float.Epsilon) {
                yield return new WaitForSeconds(intervalo);
            } else {
                break;
            }
        }
    }

    public override void KillCaractere() {
        base.KillCaractere();
        Destroy(healthBar.gameObject);
        Destroy(inventario.gameObject);
    }

    public override void ResetCaractere() {
        inventario = Instantiate(inventarioPrefab);
        healthBar = Instantiate(healthBarPrefab);
        healthBar.caractere = this;
        pontosDano.valor = inicioPontosDano;
    }
}
