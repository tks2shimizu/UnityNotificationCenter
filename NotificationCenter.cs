/*!
 * NotificationCenter.cs
 *
 * Copyright (c) 2022 tomoaki
 *
 * Released under the MIT license.
 * see https://opensource.org/licenses/MIT
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;

public class NotificationCenter : MonoBehaviour
{
    private static NotificationCenter _defaultCenter;
    public static NotificationCenter DefaultCenter
    {
        get
        {
            if (!_defaultCenter)
            {
                var notificationObject = new GameObject("Default Notification Center");

                _defaultCenter = notificationObject.AddComponent<NotificationCenter>();
            }

            return _defaultCenter;
        }
    }

    private readonly Hashtable _notifications = new Hashtable();

    public void AddObserver(Component observer, string name)
    { 
        if (string.IsNullOrEmpty(name))
        {
            Debug.Log("Null name specified for notification in AddObserver.");
            return;
        }

        _notifications[name] ??= new List<Component>();

        if (_notifications[name] is List<Component> notifyList && !notifyList.Contains(observer))
        {
            notifyList.Add(observer);
        }
    }

    public void RemoveObserver(Component observer, string name)
    {
        var notifyList = (List<Component>)_notifications[name];

        if (notifyList != null)
        {
            if (notifyList.Contains(observer))
            { 
                notifyList.Remove(observer);
            }

            if (notifyList.Count == 0)
            {
                _notifications.Remove(name);
            }
        }
    }

    public void PostNotification(Component aSender, string aName)
    {
        PostNotification(aSender, aName, null);
    }

    public void PostNotification(Component aSender, string aName, Hashtable aData)
    {
        PostNotification(new Notification(aSender, aName, aData));
    }

    public void PostNotification(Notification aNotification)
    {
        if (string.IsNullOrEmpty(aNotification.name))
        {
            Debug.Log("Null name sent to PostNotification.");
            return;
        }

        var notifyList = (List<Component>)_notifications[aNotification.name];

        if (notifyList == null)
        {
            Debug.Log("Notify list not found in PostNotification: " + aNotification.name);
            return;
        }

        var observersToRemove = new List<Component>();

        foreach (var observer in notifyList)
        {
            if (!observer)
            {
                observersToRemove.Add(observer);
            }
            else
            {
                observer.SendMessage(aNotification.name, aNotification, SendMessageOptions.DontRequireReceiver);
            }
        }

        foreach (var observer in observersToRemove)
        {
            notifyList.Remove(observer);
        }
    }

    public class Notification
    {
        public Component sender;
        public string name;
        public Hashtable data;

        public Notification(Component aSender, string aName)
        {
            sender = aSender; 
            name = aName;
            data = null;
        }

        public Notification(Component aSender, string aName, Hashtable aData)
        {
            sender = aSender;
            name = aName;
            data = aData;
        }
    }
}
