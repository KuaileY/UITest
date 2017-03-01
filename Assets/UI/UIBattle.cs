using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    class UIBattle:UIPage
    {
        public UIBattle() : base(UIType.Normal,UIMode.HideOther,_uiPath)
        {
        }

        protected override void Awake(GameObject go)
        {
            GO.transform.Find("btn_skill").GetComponent<Button>().onClick.AddListener(OnClickSkillGo);
            GO.transform.Find("btn_battle").GetComponent<Button>().onClick.AddListener(OnClickGoBattle);
        }

        private void OnClickSkillGo()
        {
            ShowPage<UISkillPage>();
        }

        private void OnClickGoBattle()
        {
            Debug.Log("加载战斗场景");
        }

        private const string _uiPath = "UIPrefab/UIBattle";
    }
}
