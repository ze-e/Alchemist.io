using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;


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

    Stat GetStatByElement(Element _element)
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
        float totalCost = GetCost();
        SetVal(Stat.Cost, totalCost);
        Manager.Instance.UpdateUI("Value_Cost", stat[Stat.Cost].ToString(), gameObject);
    }

    float GetCost()
    {
        float totalCost = 0;
        foreach (var item in stat)
        {
            if (item.Key != Stat.Cost) totalCost += item.Value;
        }
        return totalCost;
    }

    public void IncVal(string _string)
    {
        Stat key = ConvertStringToStat(_string);
        Element element = GetElementByStat(key);
        int elementVal = Manager.Instance.GetElementCount(element);

        float newVal = stat[key] + 10;

        if (newVal <= elementVal)
        {
            SetVal(key, newVal);
            Manager.Instance.UpdateUI("Value_" + key.ToString(),  newVal.ToString(), gameObject);
            SetCost();
        }
        else
        {
            Manager.Instance.UpdateUI("Error", "Not Enough " + GetElementByStat(key).ToString(), gameObject);
        }
    }

    public void DecVal(string _string)
    {
        Stat key = ConvertStringToStat(_string);
        float newVal = stat[key] - 10;

        if (newVal >= 0)
        {
            SetVal(key, newVal );
            Manager.Instance.UpdateUI("Value_" + key.ToString(), newVal.ToString(), gameObject);
            SetCost();
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
            Manager.Instance.UpdateUI(item.Key.ToString(), 0.ToString(), gameObject);
        }
    }

    public Weapon CraftWeapon()
    {
        string error = CanCraft();
        if (error == null)
        {
            var newWeapon = new Weapon((int)GetVal(Stat.Strength), GetVal(Stat.Range), GetVal(Stat.Speed), GetVal(Stat.Rate));
            ResetVals();
            return newWeapon;
        }

        else
        {
            Manager.Instance.UpdateUI("Error", error, gameObject);
            return null;
        }
    }

    public string CanCraft()
    {
        if (GetCost() < Manager.Instance.GetElementCount(Element.Aether))
        {
            return "Not Enough Aether";
        }

        if (stat.Values.Any(value => value == 0))
        {
            return "Must use all elements";
        }

        if (!ElementEquivalency())
        {
            return "Fire/Water and Air/Earth must be equivalent";
        }

        return null;
    }

    bool ElementEquivalency()
    {
        return stat[GetStatByElement(Element.Air)] == stat[GetStatByElement(Element.Earth)] && stat[GetStatByElement(Element.Water)] == stat[GetStatByElement(Element.Fire)];
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
}
