using System;
using System.Reflection;
using System.Text.RegularExpressions;

using UnityEngine;

namespace GameFramework
{
    public class OnOpenLogInfo
    {
        // 处理asset打开的callback函数
        [UnityEditor.Callbacks.OnOpenAssetAttribute(0)]
        private static bool OnOpenAsset(int _instance, int _line)
        {
            // 自定义函数，用来获取log中的stacktrace，定义在后面。
            string _stack_trace = GetStackTrace();
            // 通过stacktrace来定位是否是我们自定义的log，特殊文字
            if (!string.IsNullOrEmpty(_stack_trace) && _stack_trace.Contains(ProjectDatas.LOG_SYMBOL))
            {
                // 正则匹配at xxx，在第几行
                Match _matches = Regex.Match(_stack_trace, @"\(at(.+)\)", RegexOptions.IgnoreCase);
                string _pathline = "";
                while (_matches.Success)
                {
                    _pathline = _matches.Groups[1].Value;
                    // 找到不是我们自定义log文件的那行，重新整理文件路径，手动打开
                    if (!_pathline.Contains("Utility.cs"))
                    {
                        int _splitIndex = _pathline.LastIndexOf(":");
                        string _path = _pathline.Substring(0, _splitIndex);
                        _line = Convert.ToInt32(_pathline.Substring(_splitIndex + 1));
                        string _fullpath = Application.dataPath.Substring(0, Application.dataPath.LastIndexOf("Assets"));
                        _fullpath = _fullpath + _path;
                        _fullpath = _fullpath.Replace(" ", string.Empty);
                        _fullpath = _fullpath.Replace("/", "\\");
                        UnityEditorInternal.InternalEditorUtility.OpenFileAtLineExternal(_fullpath, _line);
                        break;
                    }
                    _matches = _matches.NextMatch();
                }

                return true;
            }
            return false;
        }

        private static string GetStackTrace()
        {
            //找到UnityEditor.EditorWindow的assembly
            Assembly _assembly_unity_editor = Assembly.GetAssembly(typeof(UnityEditor.EditorWindow));
            if (_assembly_unity_editor == null) return null;

            // 找到类UnityEditor.ConsoleWindow
            Type _type_console_window = _assembly_unity_editor.GetType("UnityEditor.ConsoleWindow");
            if (_type_console_window == null) return null;
            // 找到UnityEditor.ConsoleWindow中的成员ms_ConsoleWindow
            FieldInfo _field_console_window = _type_console_window.GetField("ms_ConsoleWindow", System.Reflection.BindingFlags.Static | System.Reflection.BindingFlags.NonPublic);
            if (_field_console_window == null) return null;
            // 获取ms_ConsoleWindow的值
            object _instance_console_window = _field_console_window.GetValue(null);
            if (_instance_console_window == null) return null;

            // 如果console窗口时焦点窗口的话，获取stacktrace
            if ((object)UnityEditor.EditorWindow.focusedWindow == _instance_console_window)
            {
                //通过assembly获取类ListViewState
                Type _type_list_view_state = _assembly_unity_editor.GetType("UnityEditor.ListViewState");
                if (_type_list_view_state == null) return null;

                //找到类UnityEditor.ConsoleWindow中的成员m_ListView
                FieldInfo _field_list_view = _type_console_window.GetField("m_ListView", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (_field_list_view == null) return null;

                //获取m_ListView的值
                object _value_list_view = _field_list_view.GetValue(_instance_console_window);
                if (_value_list_view == null) return null;

                // 下面是stacktrace中一些可能有用的数据、函数和使用方法，这里就不一一说明了，我们这里暂时还用不到
                /*
                var field_row = type_list_view_state.GetField("row", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                if (field_row == null) return null;

                var field_total_rows = type_list_view_state.GetField("totalRows", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.Public);
                if (field_total_rows == null) return null;

                var type_log_entries = assembly_unity_editor.GetType("UnityEditorInternal.LogEntries");
                if (type_log_entries == null) return null;

                var method_get_entry = type_log_entries.GetMethod("GetEntryInternal", BindingFlags.Static | BindingFlags.Public);
                if (method_get_entry == null) return null;

                var type_log_entry = assembly_unity_editor.GetType("UnityEditorInternal.LogEntry");
                if (type_log_entry == null) return null;

                var field_instance_id = type_log_entry.GetField("instanceID", BindingFlags.Instance | BindingFlags.Public);
                if (field_instance_id == null) return null;

                var field_line = type_log_entry.GetField("line", BindingFlags.Instance | BindingFlags.Public);
                if (field_line == null) return null;

                var field_condition = type_log_entry.GetField("condition", BindingFlags.Instance | BindingFlags.Public);
                if (field_condition == null) return null;

                object instance_log_entry = Activator.CreateInstance(type_log_entry);
                int value_row = (int)field_row.GetValue(value_list_view);
                int value_total_rows = (int)field_total_rows.GetValue(value_list_view);
                int log_by_this_count = 0;
                for (int i = value_total_rows – 1; i > value_row; i–) {
                method_get_entry.Invoke(null, new object[] { i, instance_log_entry });
                string value_condition = field_condition.GetValue(instance_log_entry) as string;
                if (value_condition.Contains("[SDebug]")) {
                log_by_this_count++;
                }
                }
                */

                // 找到类UnityEditor.ConsoleWindow中的成员m_ActiveText
                FieldInfo _field_active_text = _type_console_window.GetField("m_ActiveText", System.Reflection.BindingFlags.Instance | System.Reflection.BindingFlags.NonPublic);
                if (_field_active_text == null) return null;

                // 获得m_ActiveText的值，就是我们需要的stacktrace
                string _value_active_text = _field_active_text.GetValue(_instance_console_window).ToString();
                return _value_active_text;
            }

            return null;
        }
    }
}