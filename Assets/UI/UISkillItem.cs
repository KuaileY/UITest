using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Data;
using UnityEngine;
using UnityEngine.UI;

namespace Assets.UI
{
    public class UISkillItem:MonoBehaviour
    {
        public UDSkill.Skill data = null;

        public void Refresh(UDSkill.Skill skill)
        {
            data = skill;
            transform.Find("title").GetComponent<Text>().text = skill.Name + "[LV." + skill.Level + "]";
        }
    }
}
