using System.Collections;
using System.Collections.Generic;
using UI.Base;
using UnityEngine;
using TMPro;
using UnityEngine.UI;
using System;

namespace UI.Windows.SettingsMenu
{
    public class UIApproveWindow : BaseWindow
    {
        [SerializeField] private Button approve;
        [SerializeField] private Button disapprove;

        public Action<bool> PlayerApprove;

        private void Start()
        {
            approve.onClick.AddListener(Approve);
            disapprove.onClick.AddListener(Disapprove);
        }

        public void Answer(Action<bool> action)
        {
            PlayerApprove += action;
        }

        private void Approve()
        {
            PlayerApprove?.Invoke(true);
            PlayerApprove = null;
            gameObject.SetActive(false);
        }

        private void Disapprove()
        {
            PlayerApprove?.Invoke(false);
            PlayerApprove = null;
            gameObject.SetActive(false);
        }

        private void OnDestroy()
        {
            approve.onClick.RemoveListener(Approve);
            approve.onClick.RemoveListener(Disapprove);
        }
    }
}


