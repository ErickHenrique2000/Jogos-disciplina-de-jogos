using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Caractere : MonoBehaviour
{
    //public int PontosDano;            // versão anterior
    
    //public int MaxPontosDano;         // versão anterior
    public float MaxPontosDano;         // versão nova
    public float inicioPontosDano;      // valor minimo de vida

    public abstract void ResetCaractere();

    public virtual IEnumerator FlickerCaractere() {
        GetComponent<SpriteRenderer>().color = Color.red;
        yield return new WaitForSeconds(0.1f);
        GetComponent<SpriteRenderer>().color = Color.white;
    }

    public abstract IEnumerator DanoCaractere(int dano, float intervalo);

    public virtual void KillCaractere() {
        Destroy(gameObject);
    }
}
