using Loxodon.Framework.Contexts;
using UnityEngine;
using UnityEngine.UI;

public class FinishUiView : MonoBehaviour
{
    public Button RestartBtn;
    public Button BackBtn;

    private void Awake()
    {
        RestartBtn.onClick.AddListener(Restart);
        BackBtn.onClick.AddListener(Back);
    }

    private void Restart()
    {
        this.SetActive(false);
        var applicationContext = Context.GetApplicationContext();
        applicationContext.GetService<StageManager>().ChangeScene<MainStage>();
    }

    private void Back()
    {
        this.SetActive(false);
        var applicationContext = Context.GetApplicationContext();
        applicationContext.GetService<StageManager>().ChangeScene<LoginStage>();
    }
}