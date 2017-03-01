using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    public class UIMainPage:UIPage
    {
        public UIMainPage() : base(UIType.Normal,UIMode.HideOther,_uiPath)
        {
        }

        protected override void Awake(GameObject go)
        {
            GO.transform.Find("btn_skill").GetComponent<Button>().onClick.AddListener(ShowPage<UISkillPage>);
            GO.transform.Find("btn_battle").GetComponent<Button>().onClick.AddListener(ShowPage<UIBattle>);
        }

        private const string _uiPath = "UIPrefab/UIMain";
    }
}
