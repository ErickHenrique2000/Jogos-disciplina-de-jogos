using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public abstract class Caractere : MonoBehaviour
{
    //public int PontosDano;            // vers�o anterior
    
    //public int MaxPontosDano;         // vers�o anterior
    public float MaxPontosDano;         // vers�o nova
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
