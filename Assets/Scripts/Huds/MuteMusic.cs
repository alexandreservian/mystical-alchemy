using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MuteMusic : MonoBehaviour
{
    private bool muted = false;
    [SerializeField] private Sprite mutedSprite, unmutedSprite;
    [SerializeField] private Image buttonImage;

    // Start is called before the first frame update
    void Start()
    {
        GetComponent<Button>().onClick.AddListener(() =>
        {
            if (muted)
            {
                SoundManager.Instance.Unmute();
                
            } else
            {
                SoundManager.Instance.Mute();
            }

            buttonImage.sprite = muted ? mutedSprite : unmutedSprite;
            muted = !muted;
        });
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
