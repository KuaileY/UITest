using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    class UITopBar:UIPage
    {
        public UITopBar() : base(UIType.Fixed,UIMode.DoNothing,uiPath)
        {
        }

        protected override void Awake(GameObject go)
        {
            GameMain.GET.CurrentContext = Context.Pause;
            GO.transform.Find("btn_notice").GetComponent<Button>().onClick.AddListener(ShowPage<UINotice>);
            GO.transform.Find("btn_back").GetComponent<Button>().onClick.AddListener(ClosePage);
        }

        private const string uiPath = "UIPrefab/Topbar";
    }
}
