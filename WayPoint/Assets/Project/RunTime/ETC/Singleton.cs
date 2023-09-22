using System.Collections;
using System.Collections.Generic;
using UnityEngine;

// 제네릭 매서드로 어떤 스크립트도 사용이 가능하게 생성
// Type은 MonoBehaviour를 상속 받은 클래스.
public class Singleton<T> : MonoBehaviour where T : MonoBehaviour
{
    private static T Instance;

    public static T instance
    {
        get
        {
            if (Instance == null) Instance = FindObjectOfType<T>();

            return Instance;
        }
    }
}
