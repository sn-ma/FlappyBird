using UnityEngine;


namespace FlappyBirdClone
{
    public class BackgroundMove : MonoBehaviour
    {
        [SerializeField]
        private float velocityFactor = 0.2f;

        private float textureUnitSizeX;

        private void Start()
        {
            Sprite sprite = GetComponent<SpriteRenderer>().sprite;
            textureUnitSizeX = sprite.texture.width * transform.localScale.x / sprite.pixelsPerUnit;
        }

        public void OnCameraMoved(float dx)
        {
            Vector3 position = transform.position;
            position.x += dx * velocityFactor;

            float cameraPositionX = Camera.main.transform.position.x;
            if (cameraPositionX - position.x > textureUnitSizeX)
            {
                position.x += textureUnitSizeX;
            }
            transform.position = position;
        }
    }
}