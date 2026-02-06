using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.SceneManagement;
using UnityEngine.UI;

public class MenuButton : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler
{
    [Header("Feather Images")]
    public GameObject feather;

    void Start()
    {
        feather.SetActive(false);
    }

    public void OnPointerEnter(PointerEventData eventData)
    {
        feather.SetActive(true);
    }

    public void OnPointerExit(PointerEventData eventData)
    {
        feather.SetActive(false);
    }
}




