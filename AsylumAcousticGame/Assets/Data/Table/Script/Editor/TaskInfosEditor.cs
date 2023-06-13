using UnityEditor;
using System;

namespace Proto.Data
{
    [CustomEditor(typeof(TaskInfos))]
    public class TaskInfosEditor : DataScriptEditor
    {
        public override string FileID => "1s4ly6YDlV2pxduBJOKjQUDsXy45bYZPLUY_5nadHPco";
        public override string SheetName => "TaskInfo";
        public override DataScript.DataType DataType => DataScript.DataType.Table;
        public override Type SubClassType => typeof(TaskInfo);

        public override void SetAssetData(string json)
        {
            var obj = target as TaskInfos;
            obj.Values = DataScript.MakeJsonListObjectString<TaskInfo>(json).Values;
        }
    }
}

