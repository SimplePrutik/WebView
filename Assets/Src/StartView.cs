using UnityEngine;
using UnityEngine.UI;

public class StartView : MonoBehaviour
{
    [SerializeField] private Button goToWebBtn;
    [SerializeField] private WebView webView;

    private void Start()
    {
        Screen.orientation = ScreenOrientation.Portrait;
        ApplicationChrome.statusBarState = ApplicationChrome.States.Visible;
        ApplicationChrome.navigationBarState = ApplicationChrome.States.Hidden;
        goToWebBtn.onClick.AddListener(GoToWebView);
    }

    private void GoToWebView()
    {
        gameObject.SetActive(false);
        Instantiate(webView);
    }
}
