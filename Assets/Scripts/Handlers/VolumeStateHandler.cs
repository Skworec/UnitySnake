using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class VolumeStateHandler : MonoBehaviour
{
    [SerializeField] private Image VolumeButtonImage;
    [SerializeField] private Sprite volumeOnSprite;
    [SerializeField] private Sprite volumeOffSprite;

    private void Start()
    {
        DataController.instance.onVolumeStateChange.AddListener(onVolumeStateChangedHandler);
        VolumeButtonImage.sprite = DataController.instance.IsSoundEnabled() ? volumeOnSprite : volumeOffSprite;
    }

    public void onVolumeStateChangedHandler()
    {
        //if (DataController.instance.IsSoundEnabled())
        //{
        //    VolumeButtonImage.sprite = volumeOnSprite;
        //}
        //else
        //{
        //    VolumeButtonImage.sprite = volumeOffSprite;
        //}
        VolumeButtonImage.sprite = DataController.instance.IsSoundEnabled() ? volumeOnSprite : volumeOffSprite;
    }
}
