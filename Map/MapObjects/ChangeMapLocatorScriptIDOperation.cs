using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000060 RID: 96
	internal class ChangeMapLocatorScriptIDOperation : IOperation
	{
		// Token: 0x060004EB RID: 1259 RVA: 0x00028207 File Offset: 0x00027207
		public ChangeMapLocatorScriptIDOperation(MapLocator _mapLocator, ref string _oldValue, ref string _newValue)
		{
			this.mapLocator = _mapLocator;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x060004EC RID: 1260 RVA: 0x00028226 File Offset: 0x00027226
		public bool Undo()
		{
			if (!this.IsEmpty)
			{
				this.mapLocator.ScriptID = this.oldValue;
				return true;
			}
			return false;
		}

		// Token: 0x060004ED RID: 1261 RVA: 0x00028244 File Offset: 0x00027244
		public bool Redo()
		{
			if (!this.IsEmpty)
			{
				this.mapLocator.ScriptID = this.newValue;
				return true;
			}
			return false;
		}

		// Token: 0x060004EE RID: 1262 RVA: 0x00028262 File Offset: 0x00027262
		public void Destroy()
		{
			this.mapLocator = null;
		}

		// Token: 0x170000B5 RID: 181
		// (get) Token: 0x060004EF RID: 1263 RVA: 0x0002826B File Offset: 0x0002726B
		public bool IsEmpty
		{
			get
			{
				return this.mapLocator == null;
			}
		}

		// Token: 0x170000B6 RID: 182
		// (get) Token: 0x060004F0 RID: 1264 RVA: 0x00028276 File Offset: 0x00027276
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x170000B7 RID: 183
		// (get) Token: 0x060004F1 RID: 1265 RVA: 0x00028279 File Offset: 0x00027279
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x060004F2 RID: 1266 RVA: 0x0002827C File Offset: 0x0002727C
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x170000B8 RID: 184
		// (get) Token: 0x060004F3 RID: 1267 RVA: 0x0002828A File Offset: 0x0002728A
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x170000B9 RID: 185
		// (get) Token: 0x060004F4 RID: 1268 RVA: 0x00028291 File Offset: 0x00027291
		public string Description
		{
			get
			{
				return "ChangeMapLocatorScriptIDOperation";
			}
		}

		// Token: 0x040003A0 RID: 928
		private MapLocator mapLocator;

		// Token: 0x040003A1 RID: 929
		private readonly string oldValue;

		// Token: 0x040003A2 RID: 930
		private readonly string newValue;
	}
}
