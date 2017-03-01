using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.UIFramework;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    public class UINotice:UIPage
    {
        public UINotice() : base(UIType.PopUp,UIMode.DoNothing,_uiPath)
        {
        }

        protected override void Awake(GameObject go)
        {
            GO.transform.Find("content/btn_confim").GetComponent<Button>().onClick.AddListener(Hide);
        }

        private const string _uiPath = "UIPrefab/Notice";
    }
}
