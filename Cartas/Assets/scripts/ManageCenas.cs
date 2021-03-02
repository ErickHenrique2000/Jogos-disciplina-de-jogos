using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class ManageCenas : MonoBehaviour
{
    // Start is called before the first frame update
    void Start()            // verifica se o ultimo jogo é maior que o recorde e seta o novo recorde caso seja o caso tocando uma musica, posteriormente mostra o recorde na tela
    {
        int n = PlayerPrefs.GetInt("recorde", 0);
        int last = PlayerPrefs.GetInt("jogadas");
        if (last < n || n == 0) {
            PlayerPrefs.SetInt("recorde", last);
            GameObject.Find("soundEffect").GetComponent<AudioSource>().Play();
        }
        GameObject.Find("recorde").GetComponent<Text>().text = "Recorde: " + PlayerPrefs.GetInt("recorde", 0);
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    public void Recomeca() {        // Recomeça o jogo
        SceneManager.LoadScene("Lab3");
    }

    public void Creditos() {        // Vai para a cena de creditos
        SceneManager.LoadScene("Lab3_Creditos");
    }

    public void Sair() {            // Fecha o jogo
        print("sai");
        Application.Quit();
    }
}
