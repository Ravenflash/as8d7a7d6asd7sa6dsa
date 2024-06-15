using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.U2D;
using UnityEngine.UI;

namespace Ravenflash.GamePrototype
{
    public class SpriteFromAtlas : MonoBehaviour
    {
        [SerializeField] string _spriteName;

        [SerializeField] SpriteAtlas _atlas;
        [SerializeField] Image _image;
        Image Image { get { if (!_image) _image = GetComponent<Image>(); return _image; } }

        void Start()
        {
            if (_spriteName is object)
                SetSprite(_spriteName);
        }

        public void SetSprite(string spriteName)
        {
            try
            {
                _spriteName = spriteName;
                Image.sprite = _atlas.GetSprite(spriteName);
            }
            catch { throw; }
        }
    }
}
