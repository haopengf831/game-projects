using UnityEngine;
using UnityEngine.UI;

public class VocabularyUiView : MonoBehaviour
{
    public Button CloseBtn;
    public VocabularyItem VocabularyItem;
    public ScrollRect ScrollView;
    public VocabularyAsset VocabularyAsset;

    private void Awake()
    {
        VocabularyItem.SetActive(false);
        ScrollView.content.DestroyChildren();
    }

    private void Start()
    {
        VocabularyAsset.Words.ForEach(vocabulary =>
        {
            var item = Instantiate(VocabularyItem, ScrollView.content);
            item.SetData(vocabulary);
            item.SetActive(true);
        });
    }
}