using System.IO;
using System.Collections.Generic;
using UnityEngine;

namespace UnityEditor.ShaderGraph
{
    public static class PBRExtensionUtility
    {
        public static void ExtractPartialCodesFromSlots(string assetPath, string outputDirPath, bool withErrorMsg = true)
        {         
            if (string.IsNullOrEmpty(outputDirPath))
            {
                if (withErrorMsg)
                    Debug.LogError("A proper output directory should be chosen.");
                return;
            }

            var pbrMasterNode = GetPBRMasterNode(assetPath, withErrorMsg);

            if (pbrMasterNode == null)
            {
                if (withErrorMsg)
                    Debug.LogError("Extracting is supported only with PBRMasterNode.");
                return;
            }

            var shaderName = Path.GetFileNameWithoutExtension(assetPath);
            var slotNames = (string[])null;
            var codes = (string[])null;
            pbrMasterNode.GetPartialCodesFromSlots(out slotNames, out codes);
                        
            if (!Directory.Exists(outputDirPath))
            {
                Directory.CreateDirectory(outputDirPath);
            }

            for (var i = 0; i < slotNames.Length; ++i)
            {
                var slotName = slotNames[i];
                var code = codes[i];
                var filePath = $"{outputDirPath}/{shaderName}_{i:00}_{slotName}.txt";
                File.WriteAllText(filePath, code);
            }
        }

        public static void ExtractShaderCode(string assetPath, string outputDirPath, bool withErrorMsg = true)
        {     
            if (string.IsNullOrEmpty(outputDirPath))
            {
                if (withErrorMsg)
                    Debug.LogError("A proper output directory should be chosen.");
                return;
            }
            
            var pbrMasterNode = GetPBRMasterNode(assetPath);

            if (pbrMasterNode == null)
            {
                if (withErrorMsg)
                    Debug.LogError("Extracting is supported only with PBRMasterNode.");
                return;
            }       

            if (!Directory.Exists(outputDirPath))
            {
                Directory.CreateDirectory(outputDirPath);
            }

            var shaderName = Path.GetFileNameWithoutExtension(assetPath);
            List<PropertyCollector.TextureInfo> textureInfo;
            var result = pbrMasterNode.GetShader(GenerationMode.ForReals, shaderName, out textureInfo);
            File.WriteAllText($"{outputDirPath}/{shaderName}_Generated.txt", result);
        }

        private static PBRMasterNode GetPBRMasterNode(string assetPath, bool withErrorMsg = true)
        {
            var guid = AssetDatabase.AssetPathToGUID(assetPath);
            var extension = Path.GetExtension(assetPath);

            if (string.IsNullOrEmpty(extension))
            {
                if (withErrorMsg)
                    Debug.LogError("This file has no file extension!");
                return null;
            }
                
            extension = extension.Substring(1).ToLowerInvariant();
            
            if (extension != ShaderGraphImporter.Extension)
            {
                if (withErrorMsg)
                    Debug.LogError("Extracting is supported only with \".shadergraph\" files.");
                return null;
            }
                
            var textGraph = File.ReadAllText(assetPath, System.Text.Encoding.UTF8);
            var graph = JsonUtility.FromJson<GraphData>(textGraph);
            graph.OnEnable();
            graph.ValidateGraph();

            return (graph.outputNode as PBRMasterNode);
        }
    }
}