using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using SimpleJSON;
using UnityEngine;
using Action = AraxisTools.Action;

public class ResourcesController : Controller<ResourcesController>
{
    #region popups management

    private ListObjectsFromCache _popupsList;

    public List<GameObject> Popups;

    private GameObject _currentPopup;

    public GameObject LoadPopup(Type t)
    {
        //return _popupsList.CreateObject(t.Name);
        _currentPopup = (from popup in Popups where popup.name == t.Name select Instantiate(popup) as GameObject).FirstOrDefault();
        return _currentPopup;
    }

    public void UnloadPopup()
    {
        //_popupsList.DestroyObject();
        Destroy(_currentPopup);
    }

    #endregion

    #region level objects management

    #endregion

    protected override void ParseConfig()
    {
        JSONNode json = JSONNode.Parse(Config.text);

        TextAsset popupsConfig = Resources.Load<TextAsset>(json["popups_config"]);
        _popupsList = new ListObjectsFromCache(popupsConfig);
    }
}