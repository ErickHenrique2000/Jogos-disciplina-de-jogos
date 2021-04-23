using UnityEngine;

[CreateAssetMenu(menuName = "Item")]

public class Item : ScriptableObject
{
    public  string  NomeObjeto;
    public  Sprite  sprite;
    public  int     quantidade;
    public  bool    empilhavel;

    public enum TipoItem {
        MOEDA,
        HEALTH,
        SWORD,                  // novo tipo
        SHIELD,                 // novo tipo
        POTION,                 // novo tipo
        BOOT                    // novo tipo
    }

    public TipoItem tipoItem;
}
