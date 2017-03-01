using System.Collections;
using System.Collections.Generic;
using Assets.UI;
using Assets.UIFramework;
using UnityEngine;

public class GameMain : MonoBehaviour {
    public static GameMain GET { get { return _gameMain; } }
    public Context CurrentContext { get { return _currentContext; }set { _currentContext = value; } }
    private void Awake()
    {
        _gameMain = this;
    }
    // Use this for initialization
    void Start ()
    {
        UIPage.ShowPage<UITopBar>();
        UIPage.ShowPage<UIMainPage>();
        _currentContext = Context.NonBattle;
    }
	
	// Update is called once per frame
	void Update () {
		
	}

    private Context _currentContext;
    private static GameMain _gameMain;
}

public enum Context
{
    NonBattle,
    Battle,
    Pause
}
