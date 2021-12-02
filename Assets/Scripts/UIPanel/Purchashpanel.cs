
using UnityEngine;
using UnityEngine.UI;

public class Purchashpanel : BasePanel
{
    
    public Purchashpanel(UIType ui) : base(ui)
    {
        
    }

    protected override void InitEvent()
    {
        ActivePanel.GetOrAddComponentInChildren<Button>("Upgrade").onClick.AddListener(() =>
        {
            Debug.Log("Hello");
            Pop();
        });
    }
    
    
}
