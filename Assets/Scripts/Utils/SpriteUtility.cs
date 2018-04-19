using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Assertions;

namespace  Utils
{
    public static class SpriteUtility  {
        public static Vector3 GetWorldSizeOfSprite(SpriteRenderer renderer)
        {
            Assert.IsNotNull( renderer );
            Vector2 spriteSize = renderer.sprite.rect.size;
            Vector2 localSpriteSize = spriteSize / renderer.sprite.pixelsPerUnit;
            Vector3 worldSize = localSpriteSize;
            worldSize.x *= renderer.transform.lossyScale.x;
            worldSize.y *= renderer.transform.lossyScale.y;
            return worldSize;
        }
      
        public static Vector3 GetScreenSizeOfSprite( SpriteRenderer renderer )
        {
            Assert.IsNotNull( renderer );
            Vector3 worldSize = GetWorldSizeOfSprite( renderer );
            Vector3 screenSize = 0.5f * worldSize / Camera.main.orthographicSize;
            screenSize.y *= Camera.main.aspect;
            return screenSize;
        }

        public static Vector3 GetPixelSizeOfSprite( SpriteRenderer renderer )
        {
            Vector3 screenSize = GetScreenSizeOfSprite( renderer );
            Vector3 inPixels = new Vector3(screenSize.x * Camera.main.pixelWidth, screenSize.y * Camera.main.pixelHeight, 0) * 0.5f;
            return inPixels;
        }
    }

}
