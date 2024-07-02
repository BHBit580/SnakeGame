using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CoolDownSystem : MonoBehaviour
{
    public readonly List<CoolDownData> CoolDowns = new List<CoolDownData>();
    
    public void StartCoolDown(IHasCoolDown coolDown)
    {
        CoolDowns.Add(new CoolDownData(coolDown));
    }
    
    private void Update()
    {
        ProcessCoolDowns();
    }

    private void ProcessCoolDowns()
    {
        float deltaTime = Time.deltaTime;
        
        for (int i = CoolDowns.Count - 1; i >= 0; i--)
        {
            if (CoolDowns[i].DecrementCoolDown(deltaTime))
            {
                CoolDowns.RemoveAt(i);
            }
        }
    }

    public bool IsInCoolDown(int id)
    {
        foreach (CoolDownData coolDown in CoolDowns)
        {
            if (coolDown.Id == id)
            {
                return true;
            }
        }
        return false;
    }

    public float GetRemainingDuration(int id)
    {
        foreach (CoolDownData coolDown in CoolDowns)
        {
            if(coolDown.Id != id) continue;
            return coolDown.RemainingTime;
        }

        return 0f;
    }
}

public class CoolDownData
{
    public CoolDownData(IHasCoolDown coolDown)
    {
        Id = coolDown.ID;
        RemainingTime = coolDown.CoolDownDuration;
    }
    
    public int Id { get; }
    public float RemainingTime { get; private set; }
    
    public bool DecrementCoolDown(float deltaTime)
    {
        RemainingTime = Mathf.Max(RemainingTime - deltaTime, 0f);
        return RemainingTime == 0;
    }
}