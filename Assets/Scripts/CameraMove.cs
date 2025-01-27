using UnityEngine;
using UnityEngine.EventSystems;

public class CameraMove : MonoBehaviour, IDragableItems {
    private RectTransform cameraTransform;
    private bool isMovingCamera = false;

    private float rightBoundaryPoint;
    private float leftBoundaryPoint;


    private void Awake () {
        cameraTransform = Camera.main.gameObject.GetComponent<RectTransform>();

        CalculateBoundaryValues();
        //RectTransform rectTransformBackground = gameObject.GetComponent<RectTransform>();
        //Debug.Log($"Ширина фона {rectTransformBackground.rect.width}");
        //Debug.Log($"Размеры фона {rectTransformBackground.localScale}");
        //Debug.Log($"Глобальные размеры фона {rectTransformBackground.lossyScale}");

        //RectTransform transformBackgroundParent = rectTransformBackground.parent.GetComponent<RectTransform>();
        //Debug.Log($"Ширина фона {transformBackgroundParent.rect.width}");
        //Debug.Log($"Размеры фона {transformBackgroundParent.localScale}");
        //Debug.Log($"Глобальные размеры фона {transformBackgroundParent.lossyScale}");

    }

    public void OnBeginDrag (PointerEventData eventData) {
        isMovingCamera = eventData.pointerCurrentRaycast.gameObject.CompareTag("Background");
    }

    public void OnDrag (PointerEventData eventData) {
        if (!isMovingCamera)
            return;

        Vector2 deltaX = new Vector2(-eventData.delta.x/100, 0);

        // предварительный рассчет позиции камеры
        if (rightBoundaryPoint < cameraTransform.anchoredPosition.x + deltaX.x ||
            leftBoundaryPoint > cameraTransform.anchoredPosition.x + deltaX.x)
            return;

        cameraTransform.anchoredPosition += deltaX;
    }

    public void OnEndDrag (PointerEventData eventData) { }


    private void CalculateBoundaryValues () {
        RectTransform rectTransformBackground = gameObject.GetComponent<RectTransform>();
        // узнаем правую границу фона, умножив ширину на масштаб
        float scaledBackgroundWidth = rectTransformBackground.rect.width * rectTransformBackground.lossyScale.x;
        float scaledBackgroundBorder = scaledBackgroundWidth / 2f;

        // Узнаем правую границу камеры
        float scaledCameraWidth = cameraTransform.rect.width * rectTransformBackground.lossyScale.x;
        float scaledCameraBorder = scaledCameraWidth / 2f;

        rightBoundaryPoint = scaledBackgroundBorder - scaledCameraBorder;
        leftBoundaryPoint = -rightBoundaryPoint;
    }
}