using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Data;

namespace Assets
{
    public class GameData
    {
        public static GameData Instance
        {
            get
            {
                if (_mInstance == null)
                    _mInstance = new GameData();
                return _mInstance;
            }
        }
        public UDSkill PlayerSkill { get { return _mPlayerSkill; } }
        private GameData()
        {
            _mPlayerSkill = new UDSkill();
            _mPlayerSkill.Skills = new List<UDSkill.Skill>();
            for (int i = 0; i < 10; i++)
            {
                UDSkill.Skill skill = new UDSkill.Skill();
                skill.Name = "skill_" + i;
                skill.Level = 1;
                skill.Desc = "这是个牛逼的技能";
                PlayerSkill.Skills.Add(skill);
            }
        }

        private readonly UDSkill _mPlayerSkill;
        private static GameData _mInstance;
    }
}
