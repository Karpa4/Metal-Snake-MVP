using System.Collections.Generic;
using UnityEngine;

namespace Services.Pause
{
    public class PauseManager : MonoBehaviour
    {
        private List<IPause> allPauseObjects = new List<IPause>();

        public void PauseOn()
        {
            foreach (IPause pauseObj in allPauseObjects)
            {
                pauseObj.PauseOn();
            }
        }

        public void PauseOff()
        {
            foreach (IPause pauseObj in allPauseObjects)
            {
                pauseObj.PauseOff();
            }
        }

        public void Register(IPause pauseObject)
        {
            allPauseObjects.Add(pauseObject);
        }

        public void Unregister(IPause pauseObject)
        {
            allPauseObjects.Remove(pauseObject);
        }
    }
}
