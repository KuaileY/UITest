using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using Object = UnityEngine.Object;

namespace Assets.UIFramework
{
    public abstract class UIPage
    {
        public GameObject GO { get { return _gameObject; } }
        public UIPage(UIType type,UIMode mode,string uiPath)
        {
            _type = type;
            _mode = mode;
            _uiPath = uiPath;
        }
        protected object Data { get { return m_data; } }
        private void Show()
        {
            if (_gameObject == null)
            {
                _gameObject = Object.Instantiate(Resources.Load(_uiPath)) as GameObject;
                AnchorUIGameObject(_gameObject);
                Awake(_gameObject);
            }

            Active();
            Refresh();
            PopNode(this);
        }

        #region virtual api
        public virtual void Hide()
        {
            _gameObject.SetActive(false);
            _isActived = false;
            m_data = null;
        }

        public virtual void Active()
        {
            _gameObject.SetActive(true);
            _isActived = true;
        }

        public virtual void Refresh() { }
        #endregion

        private void AnchorUIGameObject(GameObject ui)
        {
            if (UIRoot.Instance == null || ui == null) return;

            if (_type == UIType.Fixed)
                ui.transform.SetParent(UIRoot.Instance.FixedRoot);
            else if (_type == UIType.Normal)
                ui.transform.SetParent(UIRoot.Instance.NormalRoot);
            else if (_type == UIType.PopUp)
                ui.transform.SetParent(UIRoot.Instance.PopupRoot);

            Vector3 anchorPos = Vector3.zero;
            Vector2 sizeDel = Vector2.zero;
            Vector3 scale = Vector3.one;

            ui.GetComponent<RectTransform>().anchoredPosition = anchorPos;
            ui.GetComponent<RectTransform>().sizeDelta = sizeDel;
            ui.GetComponent<RectTransform>().localScale = scale;
        }

        protected abstract void Awake(GameObject go);

        public sealed override string ToString()
        {
            return "Name:" + this.GetHashCode();
        }

        public bool isActive()
        {
            bool ret = _gameObject != null && _gameObject.activeSelf;
            return ret || _isActived;
        }

        private bool CheckIfNeedBack()
        {
            if (_type == UIType.Fixed || _type == UIType.PopUp || _type == UIType.None) return false;
            else if (_mode == UIMode.NotNeedBack || _mode == UIMode.DoNothing) return false;
            return true;
        }

        #region static api
        private static bool CheckIfNeedBack(UIPage page)
        {
            return page != null && page.CheckIfNeedBack();
        }

        public static void ShowPage<T>() where T : UIPage, new()
        {
            Type t = typeof (T);
            string pageName = t.ToString();
            if (_allPages != null && _allPages.ContainsKey(pageName))
                ShowPage(pageName, _allPages[pageName]);
            else
            {
                T instance = new T();
                ShowPage(pageName,instance);
            }
        }

        private static void ShowPage(string pageName,UIPage pageInstance)
        {
            if (_allPages==null)
            {
                _allPages = new Dictionary<string, UIPage>();
            }
            UIPage page = null;
            if (_allPages.ContainsKey(pageName))
                page = _allPages[pageName];
            else
            {
                _allPages.Add(pageName, pageInstance);
                page = pageInstance;
            }
            page.Show();
        }

        private static void PopNode(UIPage page)
        {
            if (_currentPageNodes == null)
                _currentPageNodes = new List<UIPage>();
            if (page == null)
            {
                Debug.LogError("[UI] page popup is null.");
                return;
            }
            if (CheckIfNeedBack(page) == false)
            {
                return;
            }

            bool isFound = false;
            for (int i = 0; i < _currentPageNodes.Count; i++)
            {
                if (_currentPageNodes[i].Equals(page))
                {
                    _currentPageNodes.RemoveAt(i);
                    _currentPageNodes.Add(page);
                    isFound = true;
                    break;
                }
            }
            if (!isFound)
                _currentPageNodes.Add(page);

            HideOldNodes();
        }

        private static void HideOldNodes()
        {
            if (_currentPageNodes.Count < 0) return;
            UIPage topPage = _currentPageNodes[_currentPageNodes.Count - 1];
            if (topPage._mode == UIMode.HideOther)
            {
                //form bottm to top.
                for (int i = _currentPageNodes.Count - 2; i >= 0; i--)
                {
                    if (_currentPageNodes[i].isActive())
                        _currentPageNodes[i].Hide();
                }
            }
        }

        public static void ClosePage()
        {
            if (_currentPageNodes == null || _currentPageNodes.Count <= 1) return;
            UIPage closePage = _currentPageNodes[_currentPageNodes.Count - 1];
            _currentPageNodes.RemoveAt(_currentPageNodes.Count - 1);

            if (_currentPageNodes.Count > 0)
            {
                UIPage page = _currentPageNodes[_currentPageNodes.Count - 1];
                ShowPage(page.ToString(), page);
                closePage.Hide();
            }
        }
        #endregion

        private string _uiPath = string.Empty;
        private GameObject _gameObject;
        private static Dictionary<string, UIPage> _allPages;
        private static List<UIPage> _currentPageNodes;
        private UIType _type = UIType.Normal;
        private UIMode _mode = UIMode.DoNothing;
        protected bool _isActived;
        private object m_data = null;
    }

    public enum UIType
    {
        Normal,
        Fixed,
        PopUp,
        //独立的窗口
        None
    }

    public enum UIMode
    {
        DoNothing,
        // 闭其他界面
        HideOther,
        // 点击返回按钮关闭当前,不关闭其他界面(需要调整好层级关系)
        NeedBack,
        // 关闭TopBar,关闭其他界面,不加入backSequence队列
        NotNeedBack
    }
}
