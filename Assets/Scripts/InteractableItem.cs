using UnityEngine;
using UnityEngine.EventSystems;


public class InteractableItem : MonoBehaviour, IPointerDownHandler, IPointerUpHandler, IDragableItems {
    private Rigidbody2D rb;
    private bool NeedSimulatedRB = true;
    private bool CheckTriggers = false;
    private RectTransform rectTransform;


    private void Awake () {
        rb = GetComponent<Rigidbody2D>();
        if (rb == null) {
            Destroy(gameObject);
        }

        rectTransform = GetComponent<RectTransform>();
    }


    /// <summary> ������� ������� �� ������ </summary>
    /// <param name="eventData"></param>
    public void OnPointerDown (PointerEventData eventData) {
        //Debug.Log("OnPointerDown");
        NeedSimulatedRB = rb.bodyType == RigidbodyType2D.Dynamic;
        rb.bodyType = RigidbodyType2D.Kinematic;
        rb.velocity = Vector2.zero;
        CheckTriggers = true;
    }


    /// <summary> ������� ���������� ������� </summary>
    /// <param name="eventData"></param>
    public void OnPointerUp (PointerEventData eventData) {
        //Debug.Log("OnPointerUp");
        rb.bodyType = NeedSimulatedRB ? RigidbodyType2D.Dynamic : RigidbodyType2D.Kinematic;
        CheckTriggers = false;
    }


    /// <summary> ������� ����� ������� �������������� </summary>
    /// <param name="eventData"></param>
    public void OnBeginDrag (PointerEventData eventData) {
        //Debug.Log("OnBeginDrag");
    }


    /// <summary> ������� �������������� ������� </summary>
    /// <param name="eventData"></param>
    public void OnDrag (PointerEventData eventData) {
        //Debug.Log("OnDrag");
        rectTransform.anchoredPosition += eventData.delta;
    }


    /// <summary> ������� ����� ��������� �������������� </summary>
    /// <param name="eventData"></param>
    public void OnEndDrag (PointerEventData eventData) {
        //Debug.Log("OnEndDrag");
    }


    /// <summary> ������� ������ ������� �������� � ����������� </summary>
    /// <param name="collision"></param>
    private void OnTriggerEnter2D (Collider2D collision) {
        //Debug.Log("OnTriggerEnter2D");
        if (collision.gameObject.CompareTag("Background")) {
            return;
        }

        if (CheckTriggers) {
            NeedSimulatedRB = false;
        } else {
            rb.bodyType = RigidbodyType2D.Kinematic;
            rb.velocity = Vector2.zero;
        }
    }


    /// <summary> ������� ��������� ������� �������� � ����������� </summary>
    /// <param name="collision"></param>
    private void OnTriggerExit2D (Collider2D collision) {
        if (collision.gameObject.CompareTag("Background")) {
            return;
        }

        //Debug.Log("OnTriggerExit2D");
        if (CheckTriggers) {
            NeedSimulatedRB = true;
        } else {
            rb.bodyType = RigidbodyType2D.Dynamic;
        }
    }
}
