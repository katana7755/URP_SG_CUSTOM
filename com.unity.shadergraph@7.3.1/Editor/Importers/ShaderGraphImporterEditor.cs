using System;
using System.IO;
using UnityEditor.Callbacks;
using UnityEditor.Experimental.AssetImporters;
using UnityEditor.ShaderGraph.Drawing;
using UnityEngine;

namespace UnityEditor.ShaderGraph
{

    [CustomEditor(typeof(ShaderGraphImporter))]
    class ShaderGraphImporterEditor : ScriptedImporterEditor
    {
        protected override bool needsApplyRevert => false;

        public override void OnInspectorGUI()
        {
            if (GUILayout.Button("Open Shader Editor"))
            {
                AssetImporter importer = target as AssetImporter;
                Debug.Assert(importer != null, "importer != null");
                ShowGraphEditWindow(importer.assetPath);
            }

            if (GUILayout.Button("PBRExtTest - Extract Codes From Slots"))
            {                
                AssetImporter importer = target as AssetImporter;
                Debug.Assert(importer != null, "importer != null");

                var path = EditorUtility.OpenFolderPanel("Choose folder for extracting", "", "");

                if (!string.IsNullOrEmpty(path))
                {
                    PBRExtensionUtility.ExtractPartialCodesFromSlots(importer.assetPath, path);
                }                
            }

            if (GUILayout.Button("PBRExtTest - Extract Shader Code"))
            {                
                AssetImporter importer = target as AssetImporter;
                Debug.Assert(importer != null, "importer != null");

                var path = EditorUtility.OpenFolderPanel("Choose folder for extracting", "", "");

                if (!string.IsNullOrEmpty(path))
                {
                    PBRExtensionUtility.ExtractShaderCode(importer.assetPath, path);
                }                
            }

            ApplyRevertGUI();
        }

        internal static bool ShowGraphEditWindow(string path)
        {
            var guid = AssetDatabase.AssetPathToGUID(path);
            var extension = Path.GetExtension(path);
            if (string.IsNullOrEmpty(extension))
                return false;
            // Path.GetExtension returns the extension prefixed with ".", so we remove it. We force lower case such that
            // the comparison will be case-insensitive.
            extension = extension.Substring(1).ToLowerInvariant();
            if (extension != ShaderGraphImporter.Extension && extension != ShaderSubGraphImporter.Extension)
                return false;

            foreach (var w in Resources.FindObjectsOfTypeAll<MaterialGraphEditWindow>())
            {
                if (w.selectedGuid == guid)
                {
                    w.Focus();
                    return true;
                }
            }

            var window = EditorWindow.CreateWindow<MaterialGraphEditWindow>(typeof(MaterialGraphEditWindow), typeof(SceneView));
            window.Initialize(guid);
            window.Focus();
            return true;
        }

        [OnOpenAsset(0)]
        public static bool OnOpenAsset(int instanceID, int line)
        {
            var path = AssetDatabase.GetAssetPath(instanceID);
            return ShowGraphEditWindow(path);
        }
    }
}
