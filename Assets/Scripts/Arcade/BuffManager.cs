using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour, IArcadePauseListener
{
    [SerializeField] List<ArcadeBuffTimer> timers;
    [SerializeField] List<Sprite> buffImages;

    public int amountOfActiveBuffs = 0;

    private bool isImmortal = false;
    private bool doubledCoins = false;

    private int currentImortalityBuffID;
    private int currentDoubledCoinsBuffID;

    public List<bool> isSlotFree;
    void Start()
    {
        isSlotFree = new List<bool>();

        for (int i = 0; i < timers.Count; i++)
        {
            isSlotFree.Add(true);
        }
    }

    public void SetImmortality()
    {
        if (isImmortal)
        {
            timers[currentImortalityBuffID].StartTimer("IMMORTALITY", buffImages[0]);
        }
        else
        {
            isImmortal = true;
            int timerIDToStart = amountOfActiveBuffs;
            while (!isSlotFree[timerIDToStart])
            {
                timerIDToStart++;

                if (timerIDToStart == timers.Count)
                    timerIDToStart = 0;
            }

            timers[timerIDToStart].StartTimer("IMMORTALITY", buffImages[0]);
            currentImortalityBuffID = timerIDToStart;
            isSlotFree[currentImortalityBuffID] = false;
            amountOfActiveBuffs++;
        }
    }

    public void SetDoubleCoins()
    {
        if (doubledCoins)
        {
            timers[currentDoubledCoinsBuffID].StartTimer("COINS", buffImages[1]);
        }
        else
        {
            doubledCoins = true;
            int timerIDToStart = amountOfActiveBuffs;
            while (!isSlotFree[timerIDToStart])
            {
                timerIDToStart++;

                if (timerIDToStart == timers.Count)
                    timerIDToStart = 0;
            }

            timers[timerIDToStart].StartTimer("COINS", buffImages[1]);
            currentDoubledCoinsBuffID = timerIDToStart;
            isSlotFree[currentDoubledCoinsBuffID] = false;

            amountOfActiveBuffs++;
        }
    }

    public bool GetImmortality()
    {
        return isImmortal;
    }

    public bool GetDoubledCoins()
    {
        return doubledCoins;
    }

    public void DeactivateImmortality()
    {
        isImmortal = false;
        isSlotFree[currentImortalityBuffID] = true;
        amountOfActiveBuffs--;
    }

    public void DeactivateDoubledCoins()
    {
        doubledCoins = false;
        isSlotFree[currentDoubledCoinsBuffID] = true;
        amountOfActiveBuffs--;
    }

    public void Restart()
    {
        amountOfActiveBuffs = 0;
        isImmortal = false;
        doubledCoins = false;

        for (int i = 0; i < isSlotFree.Count; i++)
        {
            isSlotFree[i] = true;
        }
    }
    private void SetAllTimersActive(bool isActive)
    {
        foreach (var timer in timers)
        {
            if (isActive) timer.ContinueTimer();
            else timer.PauseTimer();
        }
    }

    public void OnArcadePaused() => SetAllTimersActive(false);
    public void OnArcadeContinued() => SetAllTimersActive(true);
    public void OnArcadeRestart() 
    {
        SetAllTimersActive(false);
        Restart();
    }

}

