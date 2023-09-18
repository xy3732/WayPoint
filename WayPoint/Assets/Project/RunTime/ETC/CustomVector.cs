using UnityEngine;

namespace GamePix.CustomVector
{
    public struct FlipVector3
    {
        public static Vector3 Left { get { return new Vector3(-1f, 1f, 1f); } }
        public static Vector3 Right { get { return new Vector3(1f, 1f, 1f); } }

        public static Vector3 Default { get { return new Vector3(1f, 1f, 1f); } }
    }
}
