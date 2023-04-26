using UnityEngine;
using System;

namespace Game
{
    public class FinishGameTrigger : MonoBehaviour
    {
        public Action FinishGameEvent;

        private void OnTriggerEnter2D(Collider2D collision)
        {
            if (collision.TryGetComponent<MainPlayer>(out MainPlayer player))
            {
                FinishGameEvent?.Invoke();
            }
        }
    }
}
