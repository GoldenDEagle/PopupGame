using System.Collections;
using UnityEngine;

namespace Assets.Codebase.GameElements
{
    public class Rock : MonoBehaviour
    {
        public void Remove()
        {
            gameObject.SetActive(false);
        }
    }
}