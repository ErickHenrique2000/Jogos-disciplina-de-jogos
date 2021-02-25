using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Tile : MonoBehaviour
{
    private bool    tileRevelada = false;   // Indicador da carta virada ou n�o
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
        /*print("Voc� precionou num Tile");
        if (tileRevelada) 
            EscondeCarta();
        else
            RevelaCarta();
        */
        GameObject.Find("gameManage").GetComponent<ManageCartas>().CartaSelecionada(gameObject);
    }

    public void EscondeCarta() {
        GetComponent<SpriteRenderer>().sprite = backCarta;
        tileRevelada = false;
    }

    public void RevelaCarta() {
        GetComponent<SpriteRenderer>().sprite = originalCarta;
        tileRevelada = true;
    }

    public void setCartaOriginal(Sprite s1) {
        originalCarta = s1;
    }
}
