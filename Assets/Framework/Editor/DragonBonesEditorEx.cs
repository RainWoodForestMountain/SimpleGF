using System;
using System.IO;
using DragonBones;
using UnityEditor;
using UnityEngine;

namespace GameFramework {
    public class DragonBonesEditorEx {
        [MenuItem("Assets/创建DragonBones", false, -101)]
        static void CreateDragonBones() {
            if (Selection.assetGUIDs != null) {
                var path = AssetDatabase.GUIDToAssetPath(Selection.assetGUIDs[0]);
                var paths = path.Split(new[] {"/", "\\"}, StringSplitOptions.RemoveEmptyEntries);
                var root_name = "";
                var prefab_name_base = paths[paths.Length - 2];

                for (int i = 1; i < paths.Length; i++) {
                    if (paths[i] == "Assets") {
                        root_name = paths[i - 1].ToLower().Remove(3);
                        root_name += "_pre_";
                        break;
                    }
                }
                // 创建目录
                var prefab_path = Path.GetFullPath(path + @"\..\prefab") + "\\";
                if (!Directory.Exists(prefab_path)) {
                    Directory.CreateDirectory(prefab_path);
                }

                var animation_path = Path.GetFullPath(path + @"\..\animation") + "\\";
                if (!Directory.Exists(animation_path)) {
                    Directory.CreateDirectory(animation_path);
                }

                var texture_path = Path.GetFullPath(path + @"\..\texture") + "\\";
                if (!Directory.Exists(texture_path)) {
                    Directory.CreateDirectory(texture_path);
                }

                var materials_path = Path.GetFullPath(path + @"\..\materials") + "\\";
                if (!Directory.Exists(materials_path)) {
                    Directory.CreateDirectory(materials_path);
                }
                
                // 规范文件名
                var file_prefix = paths[paths.Length - 1].Replace("_ske.json", "").Replace("_tex.png", "")
                    .Replace("_tex.json", "");
                var ske_file_path = root_name + prefab_name_base + "_ske.json";
                var tex_file_path = root_name + prefab_name_base + "_tex.png";
                var texj_file_path = root_name + prefab_name_base + "_tex.json";
                var base_path = Path.GetDirectoryName(Path.GetFullPath(path)) + "\\";
                File.Move(base_path + file_prefix + "_ske.json", base_path + ske_file_path);
                File.Move(base_path + file_prefix + "_tex.png", base_path + tex_file_path);
                File.Move(base_path + file_prefix + "_tex.json", base_path + texj_file_path);
                AssetDatabase.Refresh();
                
                // 创建dragonbones
                var gowarpper = new GameObject(root_name + prefab_name_base);
                var go = new GameObject(root_name + prefab_name_base, typeof(UnityArmatureComponent));

                var armatureComponent = go.GetComponent<UnityArmatureComponent>();
                armatureComponent.isUGUI = true;
                if (armatureComponent.GetComponentInParent<Canvas>() == null) {
                    var canvas = GameObject.Find("Layer");
                    if (canvas) {
                        gowarpper.transform.SetParent(canvas.transform);
                        gowarpper.transform.localPosition = Vector3.zero;
                        gowarpper.transform.localScale = Vector3.one;
                        gowarpper.transform.localRotation = Quaternion.identity;
                        armatureComponent.transform.SetParent(gowarpper.transform);
                    }
                }

                armatureComponent.transform.localScale = Vector2.one * 100.0f;
                armatureComponent.transform.localPosition = Vector3.zero;
                var dragonBonesJSON =
                    AssetDatabase.LoadMainAssetAtPath(Path.GetDirectoryName(path) + "\\" + ske_file_path) as TextAsset;

                DragonBones.UnityEditor.ChangeDragonBonesData(armatureComponent, dragonBonesJSON);
                go.name = "root";
                AssetDatabase.Refresh();


                //移动文件
                var data_file_path = root_name + prefab_name_base + "_Data.asset";
                var materials_file_path = root_name + prefab_name_base + "_tex_UI_Mat.mat";

                base_path = base_path.Substring(base_path.IndexOf("Assets"));
                animation_path = animation_path.Substring(animation_path.IndexOf("Assets"));
                materials_path = materials_path.Substring(materials_path.IndexOf("Assets"));
                texture_path = texture_path.Substring(texture_path.IndexOf("Assets"));
                AssetDatabase.MoveAsset(base_path + data_file_path, animation_path + data_file_path);
                AssetDatabase.MoveAsset(base_path + ske_file_path, animation_path + ske_file_path);
                AssetDatabase.MoveAsset(base_path + texj_file_path, animation_path + texj_file_path);

                AssetDatabase.MoveAsset(base_path + materials_file_path, materials_path + materials_file_path);
                AssetDatabase.MoveAsset(base_path + tex_file_path, texture_path + tex_file_path);
                AssetDatabase.Refresh();

                //创建prefab
                prefab_path = prefab_path.Substring(prefab_path.IndexOf("Assets"));
                var prefabObj = PrefabUtility.CreatePrefab(
                    Utility.MakeUnifiedDirectory(prefab_path + root_name + prefab_name_base + ".prefab"), gowarpper);
                PrefabUtility.ConnectGameObjectToPrefab(gowarpper, prefabObj);
                AssetDatabase.Refresh();
            }
        }
    }
}