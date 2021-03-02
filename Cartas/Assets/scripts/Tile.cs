using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool    tileRevelada = false;   // Indicador da carta virada ou não
    public  Sprite  originalCarta;          // Sprite da carta desejada
    public  Sprite  backCarta;              // Sprite do avesso da carta
    // Start is called before the first frame update
    void Start()
    {
        EscondeCarta();
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void OnMouseDown() {
        /*print("Você precionou num Tile");
        if (tileRevelada) 
            EscondeCarta();
        else
            RevelaCarta();
        */
        GameObject.Find("gameManage").GetComponent<ManageCartas>().CartaSelecionada(gameObject);
    }

    public void EscondeCarta() {                                //troca o sprite da carta para o sprite da parte de tras da carta
        GetComponent<SpriteRenderer>().sprite = backCarta;
        tileRevelada = false;
    }

    public void RevelaCarta() {                                 //troca o sprite da carta para o sprite de carta revelada
        GetComponent<SpriteRenderer>().sprite = originalCarta;
        tileRevelada = true;
    }

    public void setCartaOriginal(Sprite s1, Sprite back) {      //recebe o sprite do verso e da frente da carta
        originalCarta = s1;
        backCarta = back;
    }
}
