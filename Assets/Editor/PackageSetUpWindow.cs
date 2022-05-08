using System.IO;
using UnityEditor;
using UnityEngine;

namespace Editor
{
    public class PackageSetUpWindow : EditorWindow
    {
        private const string DefaultFullPackageName = "com.deltation.new-package";

        public string PackageName;
        public string PackageDisplayName;
        public bool RepoNameSameAsPackageName = true;
        public string RepoName;
        public string PackageDescription;

        private void OnGUI()
        {
            PackageName = EditorGUILayout.TextField("Package Name", PackageName);
            PackageDisplayName = EditorGUILayout.TextField("Package Display Name", PackageDisplayName);
            RepoNameSameAsPackageName =
                EditorGUILayout.Toggle("Repo Name Same As Package Name", RepoNameSameAsPackageName);
            if (!RepoNameSameAsPackageName)
                RepoName = EditorGUILayout.TextField("Repo Name", RepoName);

            GUILayout.Label("Package Description");
            PackageDescription = EditorGUILayout.TextArea(PackageDescription,GUILayout.Height(50));

            if (GUILayout.Button("Create Package")) 
                CreatePackage();
        }

        private void CreatePackage()
        {
            // Directory.Move(
            //     PathInPackages(DefaultFullPackageName),
            //     PathInPackages("com.deltation.a")
            // );
        }

        private static string PathInPackages(string path) =>
            Path.Combine(Application.dataPath, "..", "Packages", path);

        public static void Open()
        {
            CreateWindow<PackageSetUpWindow>().Show();
        }
    }
}