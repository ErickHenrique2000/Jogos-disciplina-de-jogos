using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;

public class pauseMenu : MonoBehaviour
{
    public  static  bool    GameIsPaused = false;
    public  GameObject      menu;    
    // Start is called before the first frame update
    void Start()
    {
        menu = GameObject.Find("PauseCanvas");
        menu.SetActive(false);
    }

    // Update is called once per frame
    void Update()                                                   // detecta o uso do esc
    {
        if(Input.GetKeyDown(KeyCode.Escape)){
            if(GameIsPaused){                                       // se está pausado continuar
                Resume();
            }else{
                Pause();                                            // se não está pausado, pausa
            }
        }
    }

    public  void    Resume(){                                       // desativa o menu, volta a velocidade do jogo e seta como não pausado
        menu.SetActive(false);
        Time.timeScale = 1f;
        GameIsPaused = false;
    }

    void    Pause(){                                                // ativa o menu pause, zera a velocidade do jogo e seta como pausado
        menu.SetActive(true);
        Time.timeScale = 0f;
        GameIsPaused = true;
    }

    public  void    mainMenu(){                                     // vai para o menu principal
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Lab5_Start");   
    }

    public  void    quit(){                                         // sai do jogo
        Time.timeScale = 1f;
        GameIsPaused = false;
        SceneManager.LoadScene("Lab5_Creditos");
    }
}
