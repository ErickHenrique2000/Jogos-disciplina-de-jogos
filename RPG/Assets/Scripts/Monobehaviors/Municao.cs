using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Municao : MonoBehaviour
{

    public  int danoCausado;          // poder de dano da muni��o

    // Start is called before the first frame update
    void Start()
    {
        
    }

    // Update is called once per frame
    void Update()
    {
        
    }

    private void OnTriggerEnter2D(Collider2D collision) {
        
        if(collision is BoxCollider2D) {
            Inimigo inimigo = collision.gameObject.GetComponent<Inimigo>();
            if(inimigo != null) { 
                StartCoroutine(inimigo.DanoCaractere(danoCausado, 0.0f));
            }
            gameObject.SetActive(false);
        }

    }
}
