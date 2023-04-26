using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace UI.Base
{
    public abstract class BaseWindow : MonoBehaviour
    {
        private void Awake() => OnAwake();

        private void OnDestroy()
        {
            
        }

        public virtual void Open()
        {

        }
        public virtual void Close()
        {

        }

        protected virtual void Initialize()
        {

        }
        protected virtual void Subscribe()
        {

        }
        protected virtual void CleanUp()
        {

        }

        private void OnAwake()
        {

        }

        private void CloseWindow()
        {

        }
    }
}

