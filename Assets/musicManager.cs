using UnityEngine;

public class musicManager : MonoBehaviour
{
    public static musicManager musicmanager;
    // Start is called once before the first execution of Update after the MonoBehaviour is created
    private void Awake()
    {
        //singleton
        //katostaan onko manageria jo olemassa.
        if (musicmanager == null)
        {
            DontDestroyOnLoad(gameObject);
            musicmanager = this;
        }
        else
        {
            Destroy(gameObject);
        }

    }
}
