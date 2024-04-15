using UnityEngine;
using UnityEngine.SceneManagement;

[RequireComponent(typeof(PlayerController)), RequireComponent(typeof(SaveableEntity))]
public class PlayerSavedData : MonoBehaviour, ISaveable
{

    //Nuestro player controler tiene datos concretos que no queremos guardar (por ejemplo, referencias a componentes relativos a la escena), as� que es mejor elegir lo que queremos (quiz� para simplificar poner lo que se quiera guardar a este componente?)
    [System.Serializable]
    class SaveablePlayerData
    {
        public PlayerStats stats;
        public string lastScene;
        public Vector3 pos;
    }
    private void Start() { }

    public string SaveData()
    {
        //La pongo en la estructura
        SaveablePlayerData data = new SaveablePlayerData
        {
            stats = (PlayerStats)gameObject.GetComponent<PlayerController>().GetStats(),
            lastScene = SceneManager.GetActiveScene().name,
            pos = transform.position,
        };

        //Devuelvo los datos a guardar
        return JsonUtility.ToJson(data);
    }

    public void LoadData(string json)
    {
        //Comprobaci�n
        if (json == null) return;

        //Casteo al tipo
        SaveablePlayerData d = JsonUtility.FromJson<SaveablePlayerData>(json);

        //Pongo los datos donde toque
        ((PlayerStats)gameObject.GetComponent<PlayerController>().GetStats()).Update((PlayerStats)d.stats);
        if(d.lastScene == SceneManager.GetActiveScene().name) transform.position = d.pos;
    }
}