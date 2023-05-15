using System.Collections;
using Tzipory.BaseScripts;

namespace Tzipory.Helpers
{
    public class CoroutineHelper : MonoSingleton<CoroutineHelper>
    {
        public void StartCoroutine(IEnumerator coroutine) =>
            StartCoroutine(coroutine);
    }
}