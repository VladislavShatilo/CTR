using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class BuffManager : MonoBehaviour, IArcadeStateListener
{
    [System.Serializable]
    private class BuffData
    {
        public BuffType Type;
        public Sprite Icon;
        public bool IsActive;
        public int TimerId = -1;
    }

    [Header("Settings")]
    [SerializeField] private List<ArcadeBuffTimer> timers;
    [SerializeField] private List<Sprite> buffImages;


    private Dictionary<BuffType, BuffData> buffs;
    private List<bool> isSlotFree;

    private void Awake()
    {
        for (int i = 0; i < timers.Count; i++)
        {
            ComponentValidator.CheckAndLog(timers[i], nameof(timers), this);

        }
        for (int i = 0; i < buffImages.Count; i++)
        {
            ComponentValidator.CheckAndLog(buffImages[i], nameof(buffImages), this);

        }
        InitializeBuffs();
        InitializeSlots();
        SubscribeToTimers();
    }

    private void OnDestroy()
    {
        UnsubscribeFromTimers();
    }

    private void InitializeBuffs()
    {
        buffs = new Dictionary<BuffType, BuffData>
        {
            { BuffType.Immortality, new BuffData { Type = BuffType.Immortality, Icon = buffImages[0] } },
            { BuffType.DoubleCoins, new BuffData { Type = BuffType.DoubleCoins, Icon = buffImages[1] } }
        };
    }

    private void InitializeSlots()
    {
        isSlotFree = new List<bool>(timers.Count);
        for (int i = 0; i < timers.Count; i++)
        {
            isSlotFree.Add(true);
        }
    }

    private void SubscribeToTimers()
    {
        foreach (var timer in timers)
        {
            timer.OnTimerCompleted += HandleTimerCompleted;
        }
    }

    private void UnsubscribeFromTimers()
    {
        foreach (var timer in timers)
        {
          
            timer.OnTimerCompleted -= HandleTimerCompleted;
        }
    }

    private void HandleTimerCompleted(BuffType buffType)
    {
        if (buffs.ContainsKey(buffType))
        {
            var buff = buffs[buffType];
            if (buff.IsActive)
            {
                buff.IsActive = false;
                isSlotFree[buff.TimerId] = true;
                buff.TimerId = -1;
            }
        }
    }

    public void ActivateBuff(BuffType buffType)
    {
        if (!buffs.ContainsKey(buffType))
        {
            return;
        }

        var buff = buffs[buffType];

        if (buff.IsActive)
        {
            RefreshExistingBuff(buff);
        }
        else
        {
            StartNewBuff(buff);
        }
    }

    private void RefreshExistingBuff(BuffData buff)
    {
        timers[buff.TimerId].StartTimer(buff.Type, buff.Icon);
    }

    private void StartNewBuff(BuffData buff)
    {
        buff.IsActive = true;
        int freeSlotId = FindFreeSlot();

        if (freeSlotId == -1)
        {
            Debug.LogWarning("No free slots available for new buff");
            return;
        }

        buff.TimerId = freeSlotId;
        timers[freeSlotId].StartTimer(buff.Type, buff.Icon);
        isSlotFree[freeSlotId] = false;
        
    }

    private int FindFreeSlot()
    {
        for (int i = 0; i < isSlotFree.Count; i++)
        {
            if (isSlotFree[i]) return i;
        }
        return -1;
    }

    public bool IsBuffActive(BuffType buffType)
    {
        return buffs.ContainsKey(buffType) && buffs[buffType].IsActive;
    }

    public void Restart()
    {
        foreach (var buff in buffs.Values)
        {
            buff.IsActive = false;
            buff.TimerId = -1;
        }

        for (int i = 0; i < isSlotFree.Count; i++)
        {
            isSlotFree[i] = true;
        }
    }

    private void SetAllTimersActive(bool isActive)
    {
        foreach (var timer in timers)
        {
            if (isActive)
            {
                timer.ContinueTimer();

            }
            else
            {
                timer.PauseTimer();

            }
        }
    }

    // IArcadePauseListener implementation
    public void OnArcadePaused() => SetAllTimersActive(false);
    public void OnArcadeContinued() => SetAllTimersActive(true);
    public void OnArcadeRestart()
    {
        SetAllTimersActive(false);
        Restart();
    }
}