using System;
using System.Collections;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using JetBrains.Annotations;
using UnityEngine;

namespace AraxisTools.Json
{
    public abstract class JsonObject
    {
        protected string Key;

        public string GetKey()
        {
            return Key;
        }

        public void SetKey(string key)
        {
            Key = key;
        }

    }

    public class JsonRoot : JsonObject
    {
        private List<JsonObject> _jsonObjects;

        public JsonRoot()
        {
            _jsonObjects = new List<JsonObject>();
            Key = string.Empty;
        }

        public void AddObjects(params JsonObject[] jsonObjects)
        {
            foreach (var jsonObject in jsonObjects)
            {
                _jsonObjects.Add(jsonObject);
            }
        }

        public void AddObjects(IEnumerable<JsonObject> jsonObjects)
        {
            foreach (var elem in jsonObjects)
            {
                _jsonObjects.Add(elem);
            }
        }

        public override string ToString()
        {
            string retString = "{";
            for (int i = 0; i < _jsonObjects.Count; i++)
            {
                if (i != _jsonObjects.Count - 1)
                {
                    retString += _jsonObjects[i] + ",";
                }
                else
                {
                    retString += _jsonObjects[i];
                }
            }
            retString += "}";
            return retString;
        }
    }

    public class JsonValue : JsonObject
    {
        private readonly string _value;

        public JsonValue(string key, object value)
        {
            Key = "\"" + key + "\"";
            if (value is int || value is float || value is bool || value is long || value is double)
            {
                _value = value.ToString().ToLower();
            }
            else
            {
                _value = "\"" + value + "\"";
            }
        }

        public override string ToString()
        {
            return Key + ":" + _value;
        }
    }

    public class JsonArray : JsonObject
    {
        private List<string> _arrayElements;

        public JsonArray(string key)
        {
            Key = "\"" + key + "\"";
            _arrayElements = new List<string>();
        }

        public JsonArray(string key, IEnumerable array)
        {
            Key = "\"" + key + "\"";
            _arrayElements = new List<string>();
            foreach (var elem in array)
            {
                if (elem is int || elem is float || elem is bool || elem is long || elem is double)
                {
                    _arrayElements.Add(elem.ToString());
                }
                else
                {
                    _arrayElements.Add("\"" + elem + "\"");
                }
            }
        }

        public JsonArray(IEnumerable array)
        {
            Key = String.Empty;
            _arrayElements = new List<string>();
            foreach (var elem in array)
            {
                if (elem is int || elem is float || elem is bool || elem is long || elem is double)
                {
                    _arrayElements.Add(elem.ToString());
                }
                else
                {
                    _arrayElements.Add("\"" + elem + "\"");
                }
            }
        }

        public JsonArray(string key, IEnumerable<JsonArray> array)
        {
            Key = "\"" + key + "\"";
            _arrayElements = new List<string>();
            foreach (var elem in array)
            {
                _arrayElements.Add(elem.ToString());
            }
        }

        public JsonArray(IEnumerable<JsonArray> array)
        {
            Key = String.Empty;
            _arrayElements = new List<string>();
            foreach (var elem in array)
            {
                _arrayElements.Add(elem.ToString());
            }
        }

        public JsonArray(string key, IEnumerable<JsonClass> array)
        {
            Key = "\"" + key + "\"";
            _arrayElements = new List<string>();
            foreach (var elem in array)
            {
                _arrayElements.Add(elem.ToString());
            }
        }

        public JsonArray(IEnumerable<JsonClass> array)
        {
            Key = String.Empty;
            _arrayElements = new List<string>();
            foreach (var elem in array)
            {
                _arrayElements.Add(elem.ToString());
            }
        }

        public JsonArray(string key, Vector3 vector)
        {
            Key = "\"" + key + "\"";
            _arrayElements = new List<string>
            {
                vector.x.ToString(CultureInfo.InvariantCulture),
                vector.y.ToString(CultureInfo.InvariantCulture),
                vector.z.ToString(CultureInfo.InvariantCulture)
            };
        }

        public JsonArray(Vector3 vector)
        {
            Key = string.Empty;
            _arrayElements = new List<string>
            {
                vector.x.ToString(CultureInfo.InvariantCulture),
                vector.y.ToString(CultureInfo.InvariantCulture),
                vector.z.ToString(CultureInfo.InvariantCulture)
            };
        }

        public override string ToString()
        {
            string retString;

            if (Key != String.Empty)
            {
                retString = Key + ":[";
            }
            else
            {
                retString = "[";
            }

            for (int i = 0; i < _arrayElements.Count; i++)
            {
                if (i != _arrayElements.Count - 1)
                {
                    retString += _arrayElements[i] + ",";
                }
                else
                {
                    retString += _arrayElements[i];
                }
            }
            retString += "]";
            return retString;
        }
    }

    public class JsonClass : JsonObject
    {
        private List<JsonObject> _jsonObjects;

        public JsonClass()
        {
            _jsonObjects = new List<JsonObject>();
            Key = String.Empty;
        }

        public JsonClass(string key)
        {
            _jsonObjects = new List<JsonObject>();
            Key = "\"" + key + "\"";
        }

        public JsonClass(string key, params JsonObject[] jsonObjects)
        {
            _jsonObjects = new List<JsonObject>();
            Key = "\"" + key + "\"";
            foreach (var jsonObject in jsonObjects)
            {
                _jsonObjects.Add(jsonObject);
            }
        }

        public void AddObjects(params JsonObject[] jsonObjects)
        {
            foreach (var jsonObject in jsonObjects)
            {
                _jsonObjects.Add(jsonObject);
            }
        }

        public void AddObjects(string key, object value)
        {
            _jsonObjects.Add(new JsonValue(key, value));
        }

        public void AddObjects(IEnumerable<JsonObject> jsonObjects)
        {
            foreach (var elem in jsonObjects)
            {
                _jsonObjects.Add(elem);
            }
        }

        public object this[string key]
        {
            set
            {
                var obj = _jsonObjects.FirstOrDefault(jsonObject => jsonObject.GetKey() == key);
                if (obj == null)
                {
                    _jsonObjects.Add(new JsonValue(key, value));
                }
                else
                {
                    _jsonObjects.Remove(obj);
                    _jsonObjects.Add(new JsonValue(key, value));

                }
            }
        }

        public override string ToString()
        {
            string retString;

            if (Key != String.Empty)
            {
                retString = Key + ":{";
            }
            else
            {
                retString = "{";
            }

            for (int i = 0; i < _jsonObjects.Count; i++)
            {
                if (i != _jsonObjects.Count - 1)
                {
                    retString += _jsonObjects[i] + ",";
                }
                else
                {
                    retString += _jsonObjects[i];
                }
            }
            retString += "}";
            return retString;
        }
    }
}