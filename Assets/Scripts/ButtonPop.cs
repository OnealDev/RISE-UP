using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class ButtonPop : MonoBehaviour, IPointerEnterHandler, IPointerExitHandler, IPointerClickHandler
{
     public Vector3 normalScale = Vector3.one;
     public Vector3 hoverScale = new Vector3(1.1f, 1.1f, 1f);
     public float speed = 10f;

     bool hovering = false;

     [Header("Sound Settings")]
     public AudioSource audioSource;
     public AudioClip hoverSound;
     public AudioClip clickSound;
     private bool hasPlayedHover = false;

     void Update()
     {
          transform.localScale = Vector3.Lerp(
              transform.localScale,
              hovering ? hoverScale : normalScale,
              Time.deltaTime * speed
          );
     }

     public void OnPointerEnter(PointerEventData eventData)
     {
          hovering = true;

          if (!hasPlayedHover && hoverSound != null && audioSource != null)
          {
               audioSource.PlayOneShot(hoverSound);
               hasPlayedHover = true;
          }
     }

     public void OnPointerExit(PointerEventData eventData)
     {
          hovering = false;
          hasPlayedHover = false;
     }

     public void OnPointerClick(PointerEventData eventData)
     {
          if (clickSound != null && audioSource != null)
          {
               audioSource.PlayOneShot(clickSound);
          }
     }
}
