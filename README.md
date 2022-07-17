# NotificationCenter

Unity で利用できる NotificationCenter です。  
オリジナルは以下の URL にありましたが、サイト閉鎖のため閲覧できなくなってしまいました。  
http://wiki.unity3d.com/index.php?title=CSharpNotificationCenter  
そのためオリジナルに敬意を払いつつ、リファクタリングしたものをこちらで公開させていただきます。 （本当は作者に連絡を取りたいところですが、ソースコード自体が古く、且つ連絡を取る手段がないので勝手ながら公開させて頂きました。） 

# 使い方
1. NotificationCenter.cs を Unity のプロジェクトに含めます
2. 受信側・送信側に、以下のようなソースコードを記述します。
## 受信側

```
void Start()
{
    NotificationCenter.DefaultCenter.AddObserver(this, nameof(CountUp));
}
```

```
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
```

## 送信側
```
public void PushButton ()
{
    var notifications = new Hashtable
    {
        { "count", 1 }
    };

    NotificationCenter.DefaultCenter.PostNotification(this, "CountUp", notifications);
}
```
