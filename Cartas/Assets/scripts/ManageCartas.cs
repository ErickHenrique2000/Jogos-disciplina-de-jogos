using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Tilemaps;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class ManageCartas : MonoBehaviour
{
    public  GameObject  carta;      // A carta a ser descartada
    private bool        primeiraCartaSelecionada, segundaCartaSelecionada;      // indicadores para cada carta escolhida em cada linha
    private GameObject  carta1, carta2;
    private string linhaCarta1, linhaCarta2;

    bool timerPausado, timerAcionado;
    float timer;

    int numTentativas = 0;
    int numAcertos = 0;
    public AudioSource somOk;

    int ultimoJogo = 0;
    
    // Start is called before the first frame update
    void Start()
    {
        MostraCartas();
        UpDateTentativas();
        somOk = GetComponent<AudioSource>();
        ultimoJogo = PlayerPrefs.GetInt("jogadas", 0);
        GameObject.Find("ultimaJogada").GetComponent<Text>().text = "Jogo anterior: " + ultimoJogo;
        GameObject.Find("recorde").GetComponent<Text>().text = "Recorde: " + PlayerPrefs.GetInt("recorde", 0);
    }

    // Update is called once per frame
    void Update()
    {
        if (timerAcionado) {
            timer += Time.deltaTime;
            print(timer);
            if (timer > 1) {
                timerPausado = true;
                timerAcionado = false;
                if (carta1.tag == carta2.tag) {
                    Destroy(carta1);
                    Destroy(carta2);
                    numAcertos++;
                    somOk.Play();
                    if (numAcertos == 26) {             // Verifica se já achou todas as cartas
                        PlayerPrefs.SetInt("jogadas", numTentativas);
                        SceneManager.LoadScene("Lab3_end_game");
                    }
                } else {
                    carta1.GetComponent<Tile>().EscondeCarta();
                    carta2.GetComponent<Tile>().EscondeCarta();  
                }
                primeiraCartaSelecionada = false;
                segundaCartaSelecionada = false;
                carta1 = null;
                carta2 = null;
                linhaCarta1 = "";
                linhaCarta2 = "";
                timer = 0;
            }
        }
    }

    void MostraCartas() {
        //Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity);
        int[] arrayEmbaralhado = criaArreyEmbaralhado();        //instancia um array pra linha 1
        int[] arrayEmbaralhado2 = criaArreyEmbaralhado();       //instancia um array pra linha 2
        int[] arrayEmbaralhado3 = criaArreyEmbaralhado();       //instancia um array pra linha 3
        int[] arrayEmbaralhado4 = criaArreyEmbaralhado();       //instancia um array pra linha 4
        for (int i = 0; i < 13 ; i++){
            AddUmaCarta(0 ,i, arrayEmbaralhado[i]);             //instancia uma carta pra linha 1
            AddUmaCarta(1, i, arrayEmbaralhado2[i]);            //instancia uma carta pra linha 2
            AddUmaCarta(2, i, arrayEmbaralhado3[i]);            //instancia uma carta pra linha 3
            AddUmaCarta(3, i, arrayEmbaralhado4[i]);            //instancia uma carta pra linha 4
        }
            
    }

    void AddUmaCarta(int linha, int rank, int valor) {
        GameObject centro = GameObject.Find("centroDaTela");
        float escalaCartaOriginal = carta.transform.localScale.x;
        float fatorEscalaX = (650 * escalaCartaOriginal)/110.0f;
        float fatorEscalaY = (945 * escalaCartaOriginal)/110.0f;
        //Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank-13/2)*1.5f), centro.transform.position.y, centro.transform.position.z);
        //Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2) * fatorEscalaX), centro.transform.position.y, centro.transform.position.z);
        Vector3 novaPosicao = new Vector3(centro.transform.position.x + ((rank - 13 / 2) * fatorEscalaX), centro.transform.position.y + ((linha - 4 / 2) * fatorEscalaY), centro.transform.position.z);
        //GameObject c = (GameObject)(Instantiate(carta, new Vector3(0, 0, 0), Quaternion.identity));
        //GameObject c = (GameObject)(Instantiate(carta, new Vector3(rank*1.5f, 0, 0), Quaternion.identity));
        GameObject c = (GameObject)(Instantiate(carta, novaPosicao, Quaternion.identity));
        c.tag = "" + (valor + 1);
        c.name = "" + linha + "_" + valor;
        string nomeDaCarta = "";
        string numeroCarta = "";
        /*if(rank == 0) {
            numeroCarta = "ace";
        } else if (rank == 10) {
            numeroCarta = "jack";
        }else if (rank == 11) {
            numeroCarta = "queen";
        }else if (rank == 12) {
            numeroCarta = "king";
        } else {
            numeroCarta = "" + (rank + 1);
        }*/
        if (valor == 0) {
            numeroCarta = "ace";
        } else if (valor == 10) {
            numeroCarta = "jack";
        } else if (valor == 11) {
            numeroCarta = "queen";
        } else if (valor == 12) {
            numeroCarta = "king";
        } else {
            numeroCarta = "" + (valor + 1);
        }
        if(linha == 0) { 
            nomeDaCarta = numeroCarta + "_of_clubs";            //seta o nome da carta pra linha 1
        } else if (linha == 1){
            nomeDaCarta = numeroCarta + "_of_diamonds";         //seta o nome da carta pra linha 2
        } else if (linha == 2) {
            nomeDaCarta = numeroCarta + "_of_spades";           //seta o nome da carta pra linha 3
        } else if (linha == 3) {
            nomeDaCarta = numeroCarta + "_of_hearts";           //seta o nome da carta pra linha 4
        }
        Sprite s1 = (Sprite)Resources.Load<Sprite>(nomeDaCarta);
        Sprite back;
        if(linha == 1 || linha == 3) {
            back = (Sprite)Resources.Load<Sprite>("playCardBackRed");       //seta o sprete do verso da carta pra linha 2 e 4
        } else {
            back = (Sprite)Resources.Load<Sprite>("playCardBackBlue");      //seta o sprete do verso da carta pra linha 1 e 3
        }
        print("S1: " + s1);
        //GameObject.Find("" + rank).GetComponent<Tile>().setCartaOriginal(s1);
        //GameObject.Find("" + valor).GetComponent<Tile>().setCartaOriginal(s1);
        GameObject.Find("" + linha + "_" + valor).GetComponent<Tile>().setCartaOriginal(s1, back);          //envia o sprite da frente e do verso da carta
    }

    public int[] criaArreyEmbaralhado(){
        int[] novoArray = new int[] {0, 1, 2, 3, 4, 5, 6, 7, 8, 9, 10, 11, 12};
        int temp;
        for (int t=0; t<13; t++) {
            temp = novoArray[t];
            int r = Random.RandomRange(t, 13);
            novoArray[t] = novoArray[r];
            novoArray[r] = temp;
        }
        return novoArray;
    }

    public void CartaSelecionada(GameObject carta) {
        if (!primeiraCartaSelecionada) {
            string linha = carta.name.Substring(0, 1);
            linhaCarta1 = linha;
            primeiraCartaSelecionada = true;
            carta1 = carta;
            carta1.GetComponent<Tile>().RevelaCarta();
        } else if(primeiraCartaSelecionada && !segundaCartaSelecionada) {
            string linha = carta.name.Substring(0, 1);
            linhaCarta2 = linha;
            segundaCartaSelecionada = true;
            carta2 = carta;
            carta2.GetComponent<Tile>().RevelaCarta();
            VerificaCartas();
        }
    }

    public void VerificaCartas() {
        DisparaTimer();
        numTentativas++;
        UpDateTentativas();
    }

    public void DisparaTimer() {
        timerPausado = false;
        timerAcionado = true;

    }

    void UpDateTentativas() {
        GameObject.Find("numTentativas").GetComponent<Text>().text = "Tentativas = " + numTentativas;
    }
}
