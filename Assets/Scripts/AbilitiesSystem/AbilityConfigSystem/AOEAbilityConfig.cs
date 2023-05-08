using UnityEngine;

namespace Tzipory.AbilitiesSystem.AbilityConfigSystem
{
    [CreateAssetMenu(fileName = "AOEAbilityConfig", menuName = "ScriptableObjects/AbilitySystem/AbilityConfig/New AOE ability config", order = 0)]
    public class AOEAbilityConfig : BaseAbilityConfig
    {
        [SerializeField,Tooltip("")] private float _radius;
        [SerializeField,Tooltip("")] private float _duration;
    }
}