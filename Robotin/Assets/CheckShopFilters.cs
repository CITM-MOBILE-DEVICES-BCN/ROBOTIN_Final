using UnityEngine;
using UnityEngine.UI;

public class CheckShopFilters : MonoBehaviour
{
    public Toggle[] toggles;
    public GetActiveItems getActiveItems;

    private void Start()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            int index = i;
            toggles[i].onValueChanged.AddListener((value) =>
            {
                if (value)
                {
                    getActiveItems.SetColorFilter(index);
                }
            });
        }
    }

    private void Update()
    {
        for (int i = 0; i < toggles.Length; i++)
        {
            toggles[i].isOn = (int)getActiveItems.colorFilter == i;
        }
    }

    public void SetColorFilter(int colorFilterIndex)
    {
        getActiveItems.SetColorFilter(colorFilterIndex);
    }
}
