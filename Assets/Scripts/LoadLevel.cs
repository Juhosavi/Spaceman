using UnityEngine;

public class LoadLevel : MonoBehaviour
{
    public string levelToLoad;
    public bool cleared;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    void Start()
    {
        //katsotaan aina map scenea avattaessa, ett‰ onko GameManagerissa merkattu, ett‰ kyseinen taso on l‰p‰isty.
        //jos on l‰p‰isty, ajetaan Cleared funktio joka tekee tarpeelliset muutokset t‰h‰n objektiin eli n‰ytt‰‰ levelcleared ja poistaa collider.
        if(GameManager.manager.GetType().GetField(levelToLoad).GetValue(GameManager.manager).ToString() == "True")
        {
            Cleared(true);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
    public void Cleared(bool isClear)
    {
        if (isClear == true)
        {
            cleared = true;
            //asetetaan GameManagerissa oikea boolean trueksi
            GameManager.manager.GetType().GetField(levelToLoad).SetValue(GameManager.manager, true);
            //Laitetaan stage clear -kyltti n‰kyviin
            transform.GetChild(1).gameObject.GetComponent<SpriteRenderer>().enabled = true;
            //Koska taso on l‰p‰isty, poistetaan level trigger objektilta collider
            GetComponent<CircleCollider2D>().enabled = false;
        }

    }
}
