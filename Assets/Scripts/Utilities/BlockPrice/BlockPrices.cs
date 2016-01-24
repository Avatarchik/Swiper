using System;
using System.CodeDom;
using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AraxisTools.Json;
using SimpleJSON;

public class LevelTarget
{
    private readonly List<KeyValuePair<int, int>> _prices = new List<KeyValuePair<int, int>>();

    public LimitType LimitType;

    public int TimeSpan;

    public int Moves;

    public List<int> StarsLevels;

    public LevelTarget()
    {
        
    }

    public LevelTarget(JSONNode json)
    {
        switch (json["limit_type"])
        {
            case "Time":
                LimitType = LimitType.Time;
                TimeSpan = json["limit"].AsInt;
                break;
            case "Moves":
                LimitType = LimitType.Moves;
                Moves = json["limit"].AsInt;
                break;
        }

        StarsLevels = new List<int>();
        for (var i = 0; i < json["stars_levels"].AsArray.Count; i++)
        {
            StarsLevels.Add(json["stars_levels"][i].AsInt);
        }

        _prices = new List<KeyValuePair<int, int>>();
        for (int i = 0; i < json["prices"].AsArray.Count; i++)
        {
            KeyValuePair<int, int> pair = 
                new KeyValuePair<int, int>(json["prices"][i]["index"].AsInt, json["prices"][i]["price"].AsInt);
            _prices.Add(pair);
        }
    }

    public LevelTarget(LimitType limitType, int limit, List<int> starsLevels, IEnumerable<KeyValuePair<int, int>> prices = null)
    {
        LimitType = limitType;
        switch (limitType)
        {
            case LimitType.Moves:
                Moves = limit;
                break;
            case LimitType.Time:
                TimeSpan = limit;
                break;
        }

        StarsLevels = starsLevels;

        if (prices != null)
        {
            foreach (var p in prices)
            {
                _prices.Add(new KeyValuePair<int, int>(p.Key, p.Value));
            }
        }
    }

    public void SetTarget(LimitType limitType, int limit, List<int> starsLevels, IEnumerable<KeyValuePair<int, int>> prices = null)
    {
        LimitType = limitType;
        switch (limitType)
        {
            case LimitType.Moves:
                Moves = limit;
                break;
            case LimitType.Time:
                TimeSpan = limit;
                break;
        }

        StarsLevels = starsLevels;

        if (prices != null)
        {
            foreach (var p in prices)
            {
                _prices.Add(new KeyValuePair<int, int>(p.Key, p.Value));
            }
        }
    }

    public void SetPrice(IEnumerable<KeyValuePair<int, int>> prices)
    {
        foreach (var p in prices)
        {
            _prices.Add(new KeyValuePair<int, int>(p.Key, p.Value));
        }
    }

    public int GetLinePrice(int index, int size)
    {
        int price = 0;
        foreach (var p in _prices)
        {
            if (p.Key == index)
            {
                price = p.Value;
            }
        }
        return price*size;
    }

    public int GetBlockPrice(int index)
    {
        return (from p in _prices where p.Key == index select p.Value).FirstOrDefault();
    }

    public JsonClass GetJson(string jsonClassName)
    {
        JsonClass json = new JsonClass(jsonClassName);

        json.AddObjects(new JsonValue("limit_type", LimitType.ToString()));
        switch (LimitType)
        {
            case LimitType.Moves:
                json.AddObjects(new JsonValue("limit", Moves));
                break;
            case LimitType.Time:
                json.AddObjects(new JsonValue("limit", TimeSpan));
                break;
        }

        List<JsonClass> list = new List<JsonClass>();
        foreach (var p in _prices)
        {
            JsonClass c = new JsonClass();
            c.AddObjects(new JsonValue("index", p.Key), new JsonValue("price", p.Value));
            list.Add(c);
        }

        json.AddObjects(new JsonArray("prices", list));
        json.AddObjects(new JsonArray("stars_levels", StarsLevels));

        return json;
    }
}

public enum LimitType
{
    Time, Moves
}
