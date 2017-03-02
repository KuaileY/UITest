using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using Assets.Data;
using Assets.UIFramework;
using DG.Tweening;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

namespace Assets.UI
{
    public class UISkillPage:UIPage
    {
        public UISkillPage() : base(UIType.Normal,UIMode.HideOther,_uiPath)
        {
        }

        protected override void Awake(GameObject go)
        {
            _skillList = GO.transform.Find("list").gameObject;
            _skillDesc = GO.transform.Find("desc").gameObject;
            _skillDesc.transform.Find("btn_upgrade").GetComponent<Button>().onClick.AddListener(OnClickUpgrade);

            _skillItem = GO.transform.Find("list/Viewport/Content/item").gameObject;
            _skillItem.SetActive(false);
        }

        public override void Refresh()
        {
            _skillDesc.SetActive(false);
            _skillList.transform.localScale = Vector3.zero;
            _skillList.transform.DOScale(Vector3.one, 0.5f);

            UDSkill skillData = Data != null ? Data as UDSkill : GameData.Instance.PlayerSkill;

            for (int i = 0; i < skillData.Skills.Count; i++)
            {
                CreateSkillItem(skillData.Skills[i]);
            }
        }

        public override void Hide()
        {
            for (int i = 0; i < _skillItems.Count; i++)
            {
                GameObject.Destroy(_skillItems[i].gameObject);
            }
            _skillItems.Clear();
            GO.gameObject.SetActive(false);
        }

        private void CreateSkillItem(UDSkill.Skill skill)
        {
            GameObject go = GameObject.Instantiate(_skillItem) as GameObject;
            go.transform.SetParent(_skillItem.transform.parent);
            go.transform.localScale = Vector3.one;
            go.SetActive(true);

            UISkillItem item = go.AddComponent<UISkillItem>();
            item.Refresh(skill);
            _skillItems.Add(item);

            go.AddComponent<Button>().onClick.AddListener(OnClickSkillItem);
        }

        private void OnClickSkillItem()
        {
            UISkillItem item = EventSystem.current.currentSelectedGameObject.GetComponent<UISkillItem>();
            ShowDesc(item);
        }

        private void ShowDesc(UISkillItem skill)
        {
            _currentItem = skill;
            _skillDesc.SetActive(true);
            _skillDesc.transform.localPosition = new Vector3(300f, _skillDesc.transform.localPosition.y,
                _skillDesc.transform.localPosition.z);
            _skillDesc.GetComponent<RectTransform>().DOAnchorPos(new Vector2(-290f, -44f), 0.25f, true);

            RefreshDesc(skill);
        }

        private void OnClickUpgrade()
        {
            _currentItem.data.Level++;
            _currentItem.Refresh(_currentItem.data);
            RefreshDesc(_currentItem);
        }

        private void RefreshDesc(UISkillItem skill)
        {
            _skillDesc.transform.Find("content").GetComponent<Text>().text
                = skill.data.Desc + "\n名称：" + skill.data.Name + "\n等级：" + skill.data.Level;
        }
        private const string _uiPath = "UIPrefab/UISkill";
        private GameObject _skillList = null;
        private GameObject _skillDesc = null;
        private GameObject _skillItem = null;
        private List<UISkillItem> _skillItems = new List<UISkillItem>();
        private UISkillItem _currentItem = null;
    }
}
