using System.Collections;
using UnityEngine;
using UnityEngine.UI;

public class TextController : MonoBehaviour
{
    [SerializeField] private Text countUpLabel;
    
    void Start()
    {
        countUpLabel = GetComponent<Text>();

        NotificationCenter.DefaultCenter.AddObserver(this, nameof(CountUp));
        NotificationCenter.DefaultCenter.AddObserver(this, nameof(CountDown));
    }

    void CountUp(NotificationCenter.Notification notification)
    {
        foreach (DictionaryEntry data in notification.data)
        {
            if ((string)data.Key == "count")
            {
                countUpLabel.text = ((int)data.Value).ToString();
            }
        }
    }

    void CountDown(NotificationCenter.Notification notification)
    {
        foreach (DictionaryEntry data in notification.data)
        {
            if ((string)data.Key == "count")
            {
                countUpLabel.text = ((int)data.Value).ToString();
            }
        }
    }
}
