using UnityEngine;
using TMPro;
using System.Collections;

namespace UI.Windows.Game.HUD
{
    public class PlayerGameTimeDisplayer : MonoBehaviour
    {
        [SerializeField] private TextMeshProUGUI timeText;

        //private PlayerGameInfo playerGameInfo;

        //public void Construct(PlayerGameInfo gameInfo)
        //{
            //this.playerGameInfo = gameInfo;
            //StartCoroutine(DisplayTime());
        //}

        public void Construct()
        {
            StartCoroutine(DisplayTime());
        }

        private IEnumerator DisplayTime()
        {
            while (gameObject.activeSelf)
            {
                //timeText.text = TimeConvert();
                yield return null;
            }
        }

        private string TimeConvert(float time)
        {
            int minutes = (int)time / 60;
            int seconds = Mathf.FloorToInt(Mathf.Round((time % 60) * 10) / 10);

            if (minutes == 0)
            {
                if (seconds < 10)
                {
                    return ("00:0" + seconds);
                }
                else
                {
                    return ("00:" + seconds);
                }
                
            }
            else
            {
                if (minutes < 10)
                {
                    if (seconds < 10)
                    {
                        return ("0" + minutes + ":0" + seconds);
                    }
                    else
                    {
                        return ("0" + minutes + ":" + seconds);
                    }
                    
                }
                else
                {
                    if (seconds < 10)
                    {
                        return (minutes + ":0" + seconds);
                    }
                    else
                    {
                        return (minutes + ":" + seconds);
                    }
                }

                
            }
        }
    }
}