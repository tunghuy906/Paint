using UnityEngine;

public class ResourceManager : MonoBehaviour
{
    public static ResourceManager Instance;

    public ResourceData currentSelected;
    private int currentAmount;

    void Awake()
    {
        Instance = this;
    }

    public void SelectResource(ResourceData data)
    {
        currentSelected = data;
        currentAmount = data.maxAmount;
    }

    public bool CanPlace()
    {
        return currentAmount > 0;
    }

    public void UseResource()
    {
        currentAmount--;
    }

    public int GetCurrentAmount()
    {
        return currentAmount;
    }
}
