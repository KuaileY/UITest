using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UIFramework;
using UnityEngine;

namespace Assets.UI
{
    public class UISkillPage:UIPage
    {
        public UISkillPage() : base(UIType.Normal,UIMode.HideOther,_uiPath)
        {
        }

        protected override void Awake(GameObject go)
        {
            
        }
        private const string _uiPath = "UIPrefab/UISkill";
    }
}
