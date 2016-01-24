using UnityEngine;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using AraxisTools.Json;
using SimpleJSON;

public class RandomSequence
{
    private List<RandomObject> _randomObjects;

    public bool IsCorrect
    {
        get
        {
            return CheckProbabilitySum();
        }
    }

    public RandomSequence(JSONNode node)
    {
        _randomObjects = new List<RandomObject>();
        int size = node.AsArray.Count;

        for (int i = 0; i < size; i++)
        {
            var ro = new RandomObject
            {
                Index = node[i]["index"].AsInt,
                Probability = node[i]["probability"].AsFloat
            };

            _randomObjects.Add(ro);
        }
    }

    public RandomSequence(params RandomObject[] sequence)
    {
        _randomObjects = new List<RandomObject>();
        foreach (var obj in sequence)
        {
            _randomObjects.Add(obj);
        }
    }

    public RandomSequence(IEnumerable<RandomObject> sequence)
    {
        _randomObjects = new List<RandomObject>();
        foreach (var obj in sequence)
        {
            _randomObjects.Add(obj);
        }
    }

    public RandomSequence(params KeyValuePair<Object, float>[] sequence)
    {
        _randomObjects = new List<RandomObject>();
        foreach (var obj in sequence)
        {
            _randomObjects.Add(new RandomObject()
            {
                Object = obj.Key,
                Probability = obj.Value
            });
        }
    }

    public RandomSequence(IEnumerable<KeyValuePair<Object, float>> sequence)
    {
        _randomObjects = new List<RandomObject>();
        foreach (var obj in sequence)
        {
            _randomObjects.Add(new RandomObject()
            {
                Object = obj.Key,
                Probability = obj.Value
            });
        }
    }

    public RandomSequence(params Object[] sequence)
    {
        _randomObjects = new List<RandomObject>();
        foreach (var obj in sequence)
        {
            _randomObjects.Add(new RandomObject()
            {
                Object = obj
            });
        }
    }

    public RandomSequence(IEnumerable<Object> sequence)
    {
        _randomObjects = new List<RandomObject>();
        foreach (var obj in sequence)
        {
            _randomObjects.Add(new RandomObject()
            {
                Object = obj
            });
        }
    }

    public void RebuildSequence(params RandomObject[] objects)
    {
        _randomObjects = new List<RandomObject>();
        foreach (var obj in objects)
        {
            _randomObjects.Add(obj);
        }
    }

    public void RebuildSequence(IEnumerable<RandomObject> sequence)
    {
        _randomObjects = new List<RandomObject>();
        foreach (var obj in sequence)
        {
            _randomObjects.Add(obj);
        }
    }

    public void RebuildSequence(params KeyValuePair<Object, float>[] sequence)
    {
        _randomObjects = new List<RandomObject>();
        foreach (var obj in sequence)
        {
            _randomObjects.Add(new RandomObject()
            {
                Object = obj.Key,
                Probability = obj.Value
            });
        }
    }

    public void RebuildSequence(IEnumerable<KeyValuePair<Object, float>> sequence)
    {
        _randomObjects = new List<RandomObject>();
        foreach (var obj in sequence)
        {
            _randomObjects.Add(new RandomObject()
            {
                Object = obj.Key,
                Probability = obj.Value
            });
        }
    }

    public void RebuildSequence(params Object[] sequence)
    {
        _randomObjects = new List<RandomObject>();
        foreach (var obj in sequence)
        {
            _randomObjects.Add(new RandomObject()
            {
                Object = obj
            });
        }
    }

    public void RebuildSequence(IEnumerable<Object> sequence)
    {
        _randomObjects = new List<RandomObject>();
        foreach (var obj in sequence)
        {
            _randomObjects.Add(new RandomObject()
            {
                Object = obj
            });
        }
    }

    public void SetEqualProbabilities()
    {
        if (_randomObjects == null)
        {
            Debug.Log("Objects list is empty!");
            return;
        }

        var step = (float)1/_randomObjects.Count;
        var baseStep = 0.0f;

        foreach (var obj in _randomObjects)
        {
            obj.Probability = step;
            obj.MinValue = baseStep;
            baseStep += step;
            obj.MaxValue = baseStep;
        }
    }

    public bool CalculateProbabilities()
    {
        if (_randomObjects == null)
        {
            Debug.Log("Objects list is empty!");
            return false;
        }

        if (!CheckProbabilitySum())
        {
            Debug.Log("Wrong summary probability!");
            return false;
        }

        var baseStep = 0.0f;
        foreach (var obj in _randomObjects)
        {
            obj.MinValue = baseStep;
            baseStep += obj.Probability;
            obj.MaxValue = baseStep;
        }

        return true;
    }

    private bool CheckProbabilitySum()
    {
        const float eps = 0.005f;
        var sum = _randomObjects.Sum(obj => obj.Probability);

        if (sum >= 1 - eps && sum <= 1 + eps)
        {
            Debug.Log("Total probability = " + sum);
            return true;
        }
        else
        {
            Debug.Log("Total probability must be 1, but it is " + sum);
            return false;
        }
    }

    public Object GetRandomObject()
    {
        if (_randomObjects == null)
        {
            Debug.Log("Objects list is empty!");
            return null;
        }

        var random = Random.Range(0.0f, 1.0f);

        return _randomObjects.Where(obj => random >= obj.MinValue && random <= obj.MaxValue).Select(obj => obj.Object).FirstOrDefault();
    }

    public int GetRandomIndex()
    {
        if (_randomObjects == null)
        {
            Debug.Log("Objects list is empty!");
            return -1;
        }

        var random = Random.Range(0.0f, 1.0f);

        return _randomObjects.Where(obj => random >= obj.MinValue && random <= obj.MaxValue).Select(obj => obj.Index).FirstOrDefault();
    }

    public JsonArray GetJson(string jsonArrayName)
    {
        List<JsonClass> classList = new List<JsonClass>();
        foreach (var obj in _randomObjects)
        {
            JsonClass jclass = new JsonClass();
            jclass.AddObjects(new JsonValue("index", obj.Index), new JsonValue("probability", obj.Probability));
            classList.Add(jclass);
        }
        return new JsonArray(jsonArrayName, classList);
    }

    public RandomObject GetRandomObjectByIndex(int index)
    {
        return _randomObjects.FirstOrDefault(obj => obj.Index == index);
    }
}

public class RandomObject
{
    public Object Object;

    public int Index;

    public float Probability;

    public float MinValue;

    public float MaxValue;
}