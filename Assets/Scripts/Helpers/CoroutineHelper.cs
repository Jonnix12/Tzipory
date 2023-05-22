using System.Collections;
using Tzipory.BaseScripts;

namespace Tzipory.Helpers
{
    public class CoroutineHelper : MonoSingleton<CoroutineHelper>
    {       
        public void StartCoroutineHelper(IEnumerator coroutine) =>
            StartCoroutine(coroutine);      
    }
}