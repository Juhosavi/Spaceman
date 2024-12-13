using UnityEngine;
using UnityEngine.UI;
using System.Collections;

public class SaveGameManager : MonoBehaviour
{
    public GameObject objectToShow; // Objekti, joka n‰ytet‰‰n hetkellisesti

    public void ShowObjectForTwoSeconds()
    {
        StartCoroutine(ShowAndHideObject());
    }

    private IEnumerator ShowAndHideObject()
    {
        objectToShow.SetActive(true); // N‰yt‰ objekti
        yield return new WaitForSeconds(2f); // Odota 2 sekuntia
        objectToShow.SetActive(false); // Piilota objekti
    }
}

