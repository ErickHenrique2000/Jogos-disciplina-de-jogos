using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;

public class Inventario : MonoBehaviour
{

    public GameObject slotPrefb;                    // objeto que recebe o prefab slot
    public const int numSlots = 5;                  // numero fixo de slots
    Image[] itemImagens = new Image[numSlots];      // array de imagens
    Item[] items = new Item[numSlots];              // array de itens
    GameObject[] slots = new GameObject[numSlots];  // array de itens

    // Start is called before the first frame update
    void Start()
    {
        CriaSlots();
    }

    // Update is called once per frame
    void Update()
    {
        if (NaoTemNulos()) {
            Debug.Log("Pronto");
            SceneManager.LoadScene("Lab5_GameOver");
        }
    }

    private bool NaoTemNulos() {
        for(int i = 0; i < items.Length; i++) {
            if(items[i] == null) {
                return false;
            }
        }
        return true;
    }

    public void CriaSlots() {
        if(slotPrefb != null) {
            for(int i = 0; i < numSlots; i++) {
                GameObject novoSlot = Instantiate(slotPrefb);
                novoSlot.name = "ItemSlot_" + i;
                novoSlot.transform.SetParent(gameObject.transform.GetChild(0).transform);
                slots[i] = novoSlot;
                itemImagens[i] = novoSlot.transform.GetChild(1).GetComponent<Image>();
            }
        }
    }

    public bool AddItem(Item itemToAdd) {
        for(int i = 0; i < items.Length; i++) {
            if(items[i] != null && items[i].tipoItem == itemToAdd.tipoItem && itemToAdd.empilhavel == true) {
                items[i].quantidade = items[i].quantidade + 1;
                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();
                Text quantidadeTexto = slotScript.qtdTexto;
                quantidadeTexto.enabled = true;
                quantidadeTexto.text = items[i].quantidade.ToString();
                return true;
            }
            if(items[i] == null) {
                items[i] = Instantiate(itemToAdd);
                items[i].quantidade = 1;
                itemImagens[i].sprite = itemToAdd.sprite;
                itemImagens[i].enabled = true;

                Slot slotScript = slots[i].gameObject.GetComponent<Slot>();
                Text quantidadeTexto = slotScript.qtdTexto;
                quantidadeTexto.enabled = true;
                quantidadeTexto.text = items[i].quantidade.ToString();
                return true;
            }
        }
        return false;
    }
}
