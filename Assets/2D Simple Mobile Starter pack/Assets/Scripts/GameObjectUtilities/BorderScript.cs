using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Serialization;

[RequireComponent(typeof(BoxCollider2D))]
public class BorderScript : MonoBehaviour
{
    private enum ScreenBound
    {
        Up,
        Down,
        Left,
        Right
    }

    public enum FitType
    {
        ToScreen,
        ToMap
    }

    private Camera _camera;

    [Header("Properties")] [SerializeField]
    private ScreenBound screenBound = ScreenBound.Up;

    [SerializeField] private FitType fitType = FitType.ToScreen;

    [FormerlySerializedAs("collider")] [Header("References")] [SerializeField]
    private BoxCollider2D ownCollider2D;

    [SerializeField] private BoxCollider2D mapCollider2D;

    void Start()
    {
        _camera = Camera.main;
        if (!ownCollider2D) ownCollider2D = GetComponent<BoxCollider2D>();
        if (!mapCollider2D) mapCollider2D = GameObject.FindWithTag("Map").GetComponent<BoxCollider2D>();
        Fit();
    }

    public void Fit()
    {
        switch (fitType)
        {
            case FitType.ToScreen:
                FitToScreen();
                break;
            case FitType.ToMap:
                FitToMap();
                break;
        }
    }

    private void FitToScreen()
    {
        switch (screenBound)
        {
            case ScreenBound.Up:
                transform.localScale =
                    new Vector3(_camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x * 2f, 1f, 1f);
                transform.localPosition = new Vector3(0,
                    _camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y + ownCollider2D.bounds.size.y / 2f, 0);
                break;
            case ScreenBound.Down:
                transform.localScale =
                    new Vector3(_camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x * 2f, 1f, 1f);
                transform.localPosition = new Vector3(0,
                    _camera.ScreenToWorldPoint(new Vector2(0, 0)).y - ownCollider2D.bounds.size.y / 2f, 0);
                break;
            case ScreenBound.Left:
                transform.localScale =
                    new Vector3(1f, _camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y * 2f, 1f);
                transform.localPosition =
                    new Vector3(_camera.ScreenToWorldPoint(new Vector2(0, 0)).x - ownCollider2D.bounds.size.x / 2f,
                        _camera.ScreenToWorldPoint(new Vector2(0, Screen.height / 2f)).y, 0);
                break;
            case ScreenBound.Right:
                transform.localScale =
                    new Vector3(1f, _camera.ScreenToWorldPoint(new Vector2(0, Screen.height)).y * 2f, 1f);
                transform.localPosition =
                    new Vector3(
                        _camera.ScreenToWorldPoint(new Vector2(Screen.width, 0)).x + ownCollider2D.bounds.size.x / 2f,
                        _camera.ScreenToWorldPoint(new Vector2(0, Screen.height / 2f)).y, 0);
                break;
        }
    }

    private void FitToMap()
    {
        switch (screenBound)
        {
            case ScreenBound.Up:
                transform.localScale = new Vector3(mapCollider2D.bounds.size.x , 1f, 1f);
                transform.localPosition = new Vector3(0,
                    mapCollider2D.transform.localPosition.y + mapCollider2D.bounds.extents.y +
                    ownCollider2D.bounds.size.y / 2f, 0);
                break;
            case ScreenBound.Down:
                transform.localScale = new Vector3(mapCollider2D.bounds.size.x , 1f, 1f);
                transform.localPosition = new Vector3(0,
                    mapCollider2D.transform.localPosition.y - mapCollider2D.bounds.extents.y -
                    ownCollider2D.bounds.size.y / 2f, 0);
                break;
            case ScreenBound.Left:
                transform.localScale = new Vector3(1f, mapCollider2D.bounds.size.y , 1f);
                transform.localPosition =
                    new Vector3(
                        mapCollider2D.transform.localPosition.x - mapCollider2D.bounds.extents.x -
                        ownCollider2D.bounds.size.x / 2f, mapCollider2D.transform.localPosition.y, 0);
                break;
            case ScreenBound.Right:
                transform.localScale = new Vector3(1f, mapCollider2D.bounds.size.y , 1f);
                transform.localPosition =
                    new Vector3(
                        mapCollider2D.transform.localPosition.x + mapCollider2D.bounds.extents.x +
                        ownCollider2D.bounds.size.x / 2f, mapCollider2D.transform.localPosition.y, 0);
                break;
        }
    }
}