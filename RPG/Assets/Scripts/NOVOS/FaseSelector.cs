using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class FaseSelector : MonoBehaviour
{
    public void Inicio() {           // Carrega o inicio do jogo
        SceneManager.LoadScene("Lab5_Start");
    }

    public void Fase1() {           // Carrega a fase 1
        SceneManager.LoadScene("Lab5_RPGSetup");
    }

    public void Fase2() {           // Carrega a fase 2
        SceneManager.LoadScene("Lab5_fase2");
    }

    public void Fase3() {           // Carrega a fase 3
        SceneManager.LoadScene("Lab5_fase3");
    }

    public void Creditos() {           // Carrega os creditos
        SceneManager.LoadScene("Lab5_Creditos");
    }

    public void Sair() {                // Fecha o jogo
        Application.Quit();
    }
}
