using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class HealthBar : MonoBehaviour
{

    public PontosDano pontosDano;       // Objeto de leitura dos dados de quantos pontos tem o Player
    public Player caractere;            // receberá o objeto do Player
    public Image medidor;               // recebe a barra de medição
    public Text text;                   // recebe os dados de PD
    float maxPontosDano;                // armazena a quantidade limite de vida do player

    // Start is called before the first frame update
    void Start()
    {
        maxPontosDano = caractere.MaxPontosDano;
    }

    // Update is called once per frame
    void Update()
    {
        if (caractere != null) {
            medidor.fillAmount = pontosDano.valor/ maxPontosDano;
            text.text = "PD:" + (medidor.fillAmount * 100);
        }
    }
}
