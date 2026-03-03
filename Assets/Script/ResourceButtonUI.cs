using UnityEngine;
using UnityEngine.UI;
using TMPro;

public class ResourceButtonUI : MonoBehaviour
{
    public ResourceData resourceData;
    public Image icon;
    public TextMeshProUGUI amountText;

    void Start()
    {
        icon.sprite = resourceData.icon;
        amountText.text = resourceData.maxAmount.ToString();
    }

    public void OnClick()
    {
        ResourceManager.Instance.SelectResource(resourceData);
    }

    void Update()
    {
        if (ResourceManager.Instance.currentSelected == resourceData)
        {
            amountText.text = ResourceManager.Instance.GetCurrentAmount().ToString();
        }
    }
}
