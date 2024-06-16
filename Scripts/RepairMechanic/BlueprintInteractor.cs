using UnityEngine;
using UnityEngine.UI;

public class BlueprintInteractor : MonoBehaviour
{
   

    public GameObject blueprint;
    public GameObject openButton;
    public GameObject closeButton;

    void Start()
    {
        
    }

    public void OpenBlueprint()
    {
        blueprint.SetActive(true);
        openButton.SetActive(false);
        closeButton.SetActive(true);
    }

    public void CloseBlueprint()
    {
        blueprint.SetActive(false);
        openButton.SetActive(true);
        closeButton.SetActive(false);

    }

}
