using System.IO;
using System.Linq;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class PackageSetUpWindow : EditorWindow
    {
        private static readonly string DefaultFullPackageName = CreateFullPackageName("new-package");

        public string PackageName;
        public string PackageDisplayName;
        public bool RepoNameSameAsPackageName = true;
        public string RepoName;
        public string PackageDescription;

        private void OnGUI()
        {
            PackageName = TextField("Package Name", PackageName);
            PackageDisplayName = TextField("Package Display Name", PackageDisplayName);
            RepoNameSameAsPackageName =
                EditorGUILayout.Toggle("Repo Name Same As Package Name", RepoNameSameAsPackageName);
            if (!RepoNameSameAsPackageName)
                RepoName = TextField("Repo Name", RepoName);

            GUILayout.Label("Package Description");
            PackageDescription = EditorGUILayout.TextArea(PackageDescription, GUILayout.Height(50));

            if (GUILayout.Button("Create Package"))
                CreatePackage();
        }

        private string TextField(string label, string text)
        {
            EditorGUI.BeginChangeCheck();

            text = EditorGUILayout.TextField(label, text);

            if (EditorGUI.EndChangeCheck())
                text = text.Trim();

            return text;
        }

        private void CreatePackage()
        {
            if (string.IsNullOrWhiteSpace(PackageName))
            {
                DisplayError("Package Name is required");
                return;
            }

            if (string.IsNullOrWhiteSpace(PackageDisplayName))
            {
                DisplayError("Package Display Name is required");
                return;
            }

            if (!RepoNameSameAsPackageName && string.IsNullOrWhiteSpace(RepoName))
            {
                DisplayError("Repo Name is required");
                return;
            }

            if (string.IsNullOrWhiteSpace(PackageDescription))
            {
                DisplayError("Package Description is required");
                return;
            }

            var packageBootstrapSettings = Resources.Load<PackageBootstrapSettings>("Package Bootstrap Settings");
            if (packageBootstrapSettings == null)
            {
                DisplayError("Package Bootstrap Settings not found");
                return;
            }


            var packageJsonAsset = packageBootstrapSettings.PackageJsonAsset;
            var packageJson = JsonUtility.FromJson<PackageJsonModel>(packageJsonAsset.text);
            packageJson.name = PackageName;
            packageJson.displayName = PackageDisplayName;
            packageJson.description = PackageDescription;
            packageJson.unity = string.Join(".", Application.unityVersion.Split('.').Take(2));
            WriteTextToAsset(packageJsonAsset, JsonUtility.ToJson(packageJson));

            // Directory.Move(
            //     PathInPackages(DefaultFullPackageName),
            //     PathInPackages(CreateFullPackageName(PackageName))
            // );
        }

        private void WriteTextToAsset(Object asset, string text)
        {
            File.WriteAllText(AssetDatabase.GetAssetPath(asset), text);
            EditorUtility.SetDirty(asset);
        }

        private static void DisplayError(string message)
        {
            EditorUtility.DisplayDialog("Error", message, "OK");
        }

        private static string CreateFullPackageName(string packageName) =>
            $"com.deltation.{packageName}";

        private static string PathInPackages(string path) =>
            Path.Combine(Application.dataPath, "..", "Packages", path);

        public static void Open()
        {
            CreateWindow<PackageSetUpWindow>().Show();
        }
    }
}