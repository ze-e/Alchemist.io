using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using TMPro;

public class CraftingController : MonoBehaviour
{

    // Stat dict
    Dictionary<Stat, float> stat = new Dictionary<Stat, float>{
        { Stat.Range, 0 },
        { Stat.Rate, 0 },
        { Stat.Speed, 0 },
        { Stat.Strength, 0 },
        { Stat.Cost, 0 }
    };

    Dictionary<Stat, Element> statElementMap = new Dictionary<Stat, Element>{
        { Stat.Range, Element.Water },
        { Stat.Rate, Element.Fire },
        { Stat.Speed, Element.Air },
        { Stat.Strength, Element.Earth },
        { Stat.Cost, Element.Aether }
    };

    /* UI */
    public GameObject CraftingUI;

    Element GetElementByStat(Stat _stat)
    {
        return statElementMap[_stat];
    }

    Stat ConvertStringToStat(string _string)
    {
        return (Stat)System.Enum.Parse(typeof(Stat), _string);
    }

    Element ConvertStringToElement(string _string)
    {
        return (Element)System.Enum.Parse(typeof(Element), _string);
    }

    Stat? GetStatByElement(Element _element)
    {
        foreach (var pair in statElementMap)
        {
            if (pair.Value == _element)
            {
                return pair.Key;
            }
        }
        return null;
    }

    float GetVal(Stat key)
    {
        return stat[key];
    }

    void SetVal(Stat key, float _val)
    {
        stat[key] = _val;
    }

    void SetCost()
    {
        float totalCost = 0;
        foreach (var item in stat)
        {
            if (item.Key != Stat.Cost) totalCost += item.Value;
        }
        UpdateUI("Cost", stat[Stat.Cost].ToString());
    }

    public void IncVal(string _string)
    {
        Stat key = ConvertStringToStat(_string);
        Element element = GetElementByStat(key);
        int elementVal = Manager.Instance.elementCounts[element];

        float newVal = stat[key] + 1;
        
        if (newVal <= elementVal)
        {
            SetVal(key, newVal);
            UpdateUI(key.ToString(), newVal.ToString());
            SetCost();
            Debug.Log(key.ToString() +":"+ newVal.ToString());
        }
    }

    public void DecVal(string _string)
    {
        Stat key = ConvertStringToStat(_string);
        float newVal = stat[key] - 1;

        if (newVal >= 0)
        {
            SetVal(key, newVal );
            UpdateUI(key.ToString(), newVal.ToString());
            SetCost();
            Debug.Log(key.ToString() +":"+ newVal.ToString());
        }

    }

    void ResetVals()
    {
        stat = new Dictionary<Stat, float>{
            { Stat.Range, 0 },
            { Stat.Rate, 0 },
            { Stat.Speed, 0 },
            { Stat.Strength, 0 },
            { Stat.Cost, 0 }
        };
        foreach (var item in stat)
        {
            UpdateUI(item.Key.ToString(), 0.ToString());
        }
    }

    public Weapon CraftWeapon()
    {
        var newWeapon = new Weapon((int)GetVal(Stat.Strength), GetVal(Stat.Range), GetVal(Stat.Speed), GetVal(Stat.Rate));
        ResetVals();
        return newWeapon;
    }

    public void SetPlayerWeapon()
    {
        Weapon newWeapon = CraftWeapon();
        GameObject playerObject = GameObject.Find("Player");
        if(playerObject != null)
        {
            Player playerScr = playerObject.GetComponent<Player>();
            playerScr.SetWeapon(newWeapon);
            playerScr.SetStats();
        }
    }

    /* UI */
    public void UpdateUI(string key, string newVal)
    {
        string UIName = "Value_" + key;
        Transform[] children = CraftingUI.GetComponentsInChildren<Transform>();
        foreach (var child in children)
        {
            if (child.gameObject.name == UIName)
            {
                child.gameObject.GetComponentInChildren<TMP_Text>().text = newVal;
            }
        }
    }
}
