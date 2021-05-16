using Loxodon.Framework.Contexts;
using Loxodon.Framework.Views.Variables;
using UnityEngine;
using UnityEngine.UI;

public class LoginView : MonoBehaviour
{
    [SerializeField] protected VariableArray Variables;
    private Button m_SchoolBtn;
    private Button m_LibraryBtn;
    private Button m_RestaurantBtn;

    private void Awake()
    {
        m_SchoolBtn = Variables.Get<Button>("schoolBtn");
        m_LibraryBtn = Variables.Get<Button>("libraryBtn");
        m_RestaurantBtn = Variables.Get<Button>("restaurantBtn");

        m_SchoolBtn.onClick.AddListener(OnSchoolClick);
        m_LibraryBtn.onClick.AddListener(OnLibraryClick);
        m_RestaurantBtn.onClick.AddListener(OnRestaurantClick);
    }

    private void OnSchoolClick()
    {
        Change(StageType.School);
    }

    private void OnLibraryClick()
    {
        Change(StageType.Library);
    }

    private void OnRestaurantClick()
    {
        Change(StageType.Restaurant);
    }

    private void Change(StageType stage)
    {
        var applicationContext = Context.GetApplicationContext();
        applicationContext.Set(nameof(StageType), stage);
        applicationContext.GetService<StageManager>().ChangeScene<MainStage>();
    }
}