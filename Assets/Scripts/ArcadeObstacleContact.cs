using System.Collections;
using System.Collections.Generic;
using UnityEngine;


public class ArcadeObstacleContact : MonoBehaviour
{
    private GameObject particlesImortalityPrefab;
    private GameObject particlesCrashPrefab;

    private IEnumerator OnTriggerEnter(Collider other)
    {
        particlesImortalityPrefab = other.GetComponent<PlayerEffects>().GetParticlesImortalityPrefab();
        particlesCrashPrefab = other.GetComponent<PlayerEffects>().GetParticlesCrashPrefab();

        yield return new WaitForSeconds(0);
        if (other.gameObject.GetComponent<buffScripts>().GetImmortality())
        {
            Destroy(gameObject);
            Instantiate(particlesImortalityPrefab, other.gameObject.transform.position, transform.rotation);
        }
        else
        {
            FindObjectOfType<mainManager>().SaveCoinsInLevel();
            Instantiate(particlesCrashPrefab, other.gameObject.transform.position, transform.rotation);
            other.gameObject.GetComponent<ArcadePlayerMovement>().DestroyCar();
            yield return new WaitForSeconds(0.7f);
            int score = int.Parse(UIArcadeController.Instance.ScoreInGameText.text, System.Globalization.NumberStyles.AllowThousands);
           
            if (!Storage.Instance.isRewardArcadeShown && Storage.Instance.canShowArcadeRewardTime && score > 700* Storage.Instance.carMultiplier[Storage.Instance.SelectedCar])
            {

                UIArcadeController.Instance.ShowFinalAdvWindow();
                Storage.Instance.isRewardArcadeShown = true;

            }
            else
            {
                UIArcadeController.Instance.ShowFinalWindow();
                FindObjectOfType<smoothScore>().CountScore();
            }

        }
    }
}
