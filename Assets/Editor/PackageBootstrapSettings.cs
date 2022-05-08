using UnityEngine;

namespace Editor
{
    [CreateAssetMenu]
    public class PackageBootstrapSettings : ScriptableObject
    {
        [SerializeField] private TextAsset _packageJsonAsset;

        public TextAsset PackageJsonAsset => _packageJsonAsset;
    }
}