using System.Collections;
using TMPro;
using UnityEngine;

public class WebView : MonoBehaviour
{
    [SerializeField] protected UniWebView webView;
    [SerializeField] protected TMP_Text loadingText;

    private void Start()
    {
        webView.OnPageFinished += LoadingFinished;
        ApplicationChrome.statusBarState = ApplicationChrome.States.Visible;
        Screen.orientation = ScreenOrientation.AutoRotation;
    }
    
    
    private void LoadingFinished()
    {
        loadingText.gameObject.SetActive(false);
        var statusBarSize = GetAndroidStatusBarHeight() / Screen.height;
        SetNormalizedFrame(new Rect(0f, statusBarSize, 1f, 1f - statusBarSize));
        webView.Show();
    }

    private void SetNormalizedFrame(Rect normalizedRect)
    {
        Rect rect = new Rect()
        {
            x = normalizedRect.x * Screen.width,
            y = normalizedRect.y * Screen.height,
            width = normalizedRect.width * Screen.width,
            height = normalizedRect.height * Screen.height,
        };
        webView.Frame = rect;
        webView.UpdateFrame();
    }

    private void Update()
    {
        if ((Input.deviceOrientation == DeviceOrientation.LandscapeLeft) && (Screen.orientation != ScreenOrientation.LandscapeLeft))
        {
            Screen.orientation = ScreenOrientation.LandscapeLeft;
            StartCoroutine(SetFrameWithDelay());
            return;
        }
		
        if ((Input.deviceOrientation == DeviceOrientation.LandscapeRight) && (Screen.orientation != ScreenOrientation.LandscapeRight))
        {
            Screen.orientation = ScreenOrientation.LandscapeRight;
            StartCoroutine(SetFrameWithDelay());
            return;
        }
		
        if ((Input.deviceOrientation == DeviceOrientation.Portrait) && (Screen.orientation != ScreenOrientation.Portrait))
        {
            Screen.orientation = ScreenOrientation.Portrait;
            StartCoroutine(SetFrameWithDelay());
            return;
        }
		
        if ((Input.deviceOrientation == DeviceOrientation.PortraitUpsideDown) && (Screen.orientation != ScreenOrientation.PortraitUpsideDown))
        {
            Screen.orientation = ScreenOrientation.PortraitUpsideDown;
            StartCoroutine(SetFrameWithDelay());
            return;
        }
    }

    private IEnumerator SetFrameWithDelay()
    {
        yield return new WaitForSeconds(0.5f);
        var statusBarSize = GetAndroidStatusBarHeight() / Screen.height;
        SetNormalizedFrame(new Rect(0f, statusBarSize, 1f, 1f - statusBarSize));
    }
    
    private float GetAndroidStatusBarHeight() {
#if UNITY_ANDROID && !UNITY_EDITOR
  
                AndroidJavaClass up = new AndroidJavaClass("com.unity3d.player.UnityPlayer");
                AndroidJavaObject ca = up.GetStatic<AndroidJavaObject>("currentActivity");
                var getWindow = ca.Call<AndroidJavaObject>("getWindow");
                var DecorView = getWindow.Call<AndroidJavaObject>("getDecorView");
                var insets = DecorView.Call<AndroidJavaObject>("getRootWindowInsets");
                return insets.Call<int>("getSystemWindowInsetTop");

#else
        return 0;
#endif
    }

    private void OnDestroy()
    {
        webView.OnPageFinished -= LoadingFinished;
    }
}