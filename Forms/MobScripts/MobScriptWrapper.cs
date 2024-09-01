using System;
using System.Collections.Generic;
using System.IO;
using Db;
using Db.Main;
using Tools.DbCommon;
using Tools.DBGameObjects;

namespace MapEditor.Forms.MobScripts
{
	// Token: 0x02000018 RID: 24
	public class MobScriptWrapper
	{
		// Token: 0x06000229 RID: 553 RVA: 0x0001908C File Offset: 0x0001808C
		private static bool CheckObjType(IObjMan _scriptMan, string field, string type)
		{
			return _scriptMan.IsStructPtrInstanceCompatible(field, type);
		}

		// Token: 0x0600022A RID: 554 RVA: 0x00019098 File Offset: 0x00018098
		private static string GetTextFile(IObjMan scriptMan, string field)
		{
			string filePath;
			scriptMan.GetValue(field, out filePath);
			return filePath;
		}

		// Token: 0x0600022B RID: 555 RVA: 0x000190AF File Offset: 0x000180AF
		private static string GetText(IObjMan _scriptMan, string field)
		{
			return DBMethods.GetTextValue(_scriptMan, field, false);
		}

		// Token: 0x0600022C RID: 556 RVA: 0x000190BC File Offset: 0x000180BC
		private void SetText(IObjMan _scriptMan, string field, RandomSayActionPair pair)
		{
			string defaultFileName = string.Empty;
			if (!string.IsNullOrEmpty(pair.textFile))
			{
				_scriptMan.SetValue(field, pair.textFile);
			}
			else
			{
				defaultFileName = this.GetNewTextFileName();
			}
			DBMethods.SetTextValue(_scriptMan, field, pair.text, defaultFileName, false);
		}

		// Token: 0x0600022D RID: 557 RVA: 0x00019108 File Offset: 0x00018108
		private string GetNewTextFileName()
		{
			if (this.scriptMan != null)
			{
				IFieldDesc desc = this.scriptMan.GetFieldDesc(string.Empty);
				if (desc != null)
				{
					string pathPrefix = this.scriptMan.DBID.GetFileShortName();
					pathPrefix = string.Format("{0}_{1}", pathPrefix, desc.FieldName);
					int index = 0;
					string path = null;
					string folder = this.scriptMan.DBID.GetFileFolder(EditorEnvironment.DataFolder);
					while (string.IsNullOrEmpty(path) || File.Exists(string.Format("{0}\\{1}", folder, path)))
					{
						path = string.Format("{0}_{1}.txt", pathPrefix, index);
						index++;
					}
					return path;
				}
			}
			return null;
		}

		// Token: 0x0600022E RID: 558 RVA: 0x000191A9 File Offset: 0x000181A9
		public MobScriptWrapper(IObjMan _scriptMan)
		{
			this.scriptMan = _scriptMan;
		}

		// Token: 0x0600022F RID: 559 RVA: 0x000191B8 File Offset: 0x000181B8
		public string GetScriptType(out List<RandomSayActionPair> collection)
		{
			collection = null;
			string baseType;
			string instType;
			if (this.scriptMan == null || (this.scriptMan.IsStructPtr(string.Empty, out baseType, out instType) && string.IsNullOrEmpty(instType)))
			{
				return "EmptyScriptType";
			}
			if (MobScriptWrapper.CheckObjType(this.scriptMan, string.Empty, "CreatureSayAction"))
			{
				collection = new List<RandomSayActionPair>(1);
				collection.Add(new RandomSayActionPair(1f, MobScriptWrapper.GetTextFile(this.scriptMan, "text"), MobScriptWrapper.GetText(this.scriptMan, "text")));
				return "CreatureSayAction";
			}
			if (MobScriptWrapper.CheckObjType(this.scriptMan, string.Empty, "VisActionRandom"))
			{
				List<RandomSayActionPair> _collection = new List<RandomSayActionPair>();
				IObjMan arrayMan = this.scriptMan.CreateManipulator("elements");
				int size = arrayMan.GetArraySize();
				for (int index = 0; index < size; index++)
				{
					arrayMan.SetArrayIndex(index);
					if (!MobScriptWrapper.CheckObjType(arrayMan, "action", "CreatureSayAction"))
					{
						return null;
					}
					float probability;
					arrayMan.GetValue("probability", out probability);
					_collection.Add(new RandomSayActionPair(probability, MobScriptWrapper.GetTextFile(arrayMan, "action.text"), MobScriptWrapper.GetText(arrayMan, "action.text")));
				}
				collection = _collection;
				return "VisActionRandom";
			}
			return null;
		}

		// Token: 0x06000230 RID: 560 RVA: 0x000192EC File Offset: 0x000182EC
		public void SetValues(string type, List<RandomSayActionPair> values)
		{
			if (string.IsNullOrEmpty(type))
			{
				return;
			}
			List<string> textFiles = new List<string>();
			List<RandomSayActionPair> collection;
			this.GetScriptType(out collection);
			if (collection != null)
			{
				foreach (RandomSayActionPair action in collection)
				{
					string fileName = action.textFile;
					if (DbCommonMethods.CheckFileRef(ref fileName, true, true))
					{
						textFiles.Add(fileName);
					}
				}
			}
			ObjMan.StartMassEditing();
			if (type != null)
			{
				if (type == "CreatureSayAction")
				{
					collection = new List<RandomSayActionPair>(1);
					this.scriptMan.SetStructPtrInstance(string.Empty, type);
					goto IL_E3;
				}
				if (type == "VisActionRandom")
				{
					collection = new List<RandomSayActionPair>(1);
					this.scriptMan.SetStructPtrZero(string.Empty);
					this.scriptMan.SetStructPtrInstance(string.Empty, "VisActionRandom");
					goto IL_E3;
				}
			}
			this.scriptMan.SetStructPtrZero(string.Empty);
			collection = null;
			IL_E3:
			if (collection != null)
			{
				int cnt = values.Count;
				for (int pairIndex = 0; pairIndex < cnt; pairIndex++)
				{
					RandomSayActionPair pair = values[pairIndex];
					if (textFiles.Count > 0)
					{
						pair.textFile = textFiles[0];
						textFiles.RemoveAt(0);
					}
					if (type == "CreatureSayAction")
					{
						this.SetText(this.scriptMan, "text", pair);
						break;
					}
					if (type == "VisActionRandom")
					{
						IObjMan arrayMan = this.scriptMan.CreateManipulator("elements");
						int index = arrayMan.GetArraySize();
						arrayMan.Insert(string.Empty, index);
						arrayMan.SetArrayIndex(index);
						arrayMan.SetValue("probability", pair.probability);
						arrayMan.SetStructPtrInstance("action", "CreatureSayAction");
						this.SetText(arrayMan, "action.text", pair);
					}
				}
			}
			ObjMan.StopMassEditing();
			foreach (string textFile in textFiles)
			{
				DBMethods.DeleteFile(textFile);
			}
		}

		// Token: 0x17000020 RID: 32
		// (get) Token: 0x06000231 RID: 561 RVA: 0x00019518 File Offset: 0x00018518
		public IObjMan ScriptMan
		{
			get
			{
				return this.scriptMan;
			}
		}

		// Token: 0x04000202 RID: 514
		public const string emptyScriptType = "EmptyScriptType";

		// Token: 0x04000203 RID: 515
		public const string sayActionType = "CreatureSayAction";

		// Token: 0x04000204 RID: 516
		public const string randomActionType = "VisActionRandom";

		// Token: 0x04000205 RID: 517
		private readonly IObjMan scriptMan;
	}
}
