using UnityEngine;

namespace FlappyBirdClone
{
    public class PageTiledImage : MonoBehaviour
    {
        private float textureUnitSizeX;

        void Start()
        {
            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            textureUnitSizeX = sprite.texture.width * transform.localScale.x / sprite.pixelsPerUnit;
        }

        void Update()
        {
            Vector3 position = transform.position;
            float cameraPositionX = Camera.main.transform.position.x;
            if (cameraPositionX - position.x > textureUnitSizeX)
            {
                position.x += textureUnitSizeX;
            }
            transform.position = position;
        }
    }
}