using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using UnityEngine.UIElements;

using Button = UnityEngine.UI.Button;

public class BlockSeason : MonoBehaviour
{
    private int[] seasonStars = new[] { 25, 52, 84, 126 };

    int getNumberOfSeason()
    {
        return int.Parse(SceneManager.GetActiveScene().name) / 12 - 1;
    }
    void Start()
    {
        if (Storage.Instance.stars < seasonStars[getNumberOfSeason()])
        {
            UIController.Instance.NextWinButton.enabled =  false;

        }     
    }
}
