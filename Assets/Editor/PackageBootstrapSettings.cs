using UnityEngine;

namespace Editor
{
    [CreateAssetMenu]
    public class PackageBootstrapSettings : ScriptableObject
    {
        [SerializeField] private Object _packageDirectory;

    }
}