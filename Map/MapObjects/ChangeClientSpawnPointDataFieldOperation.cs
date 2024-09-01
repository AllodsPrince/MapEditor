using System;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000231 RID: 561
	internal class ChangeClientSpawnPointDataFieldOperation : IOperation
	{
		// Token: 0x06001AE0 RID: 6880 RVA: 0x000AF18C File Offset: 0x000AE18C
		private bool SetValue(object value)
		{
			try
			{
				string a;
				if ((a = this.field) != null && a == "VisualState")
				{
					DeviceClientSpawnPointData _clientSpawnPointData = this.clientSpawnPointData as DeviceClientSpawnPointData;
					if (_clientSpawnPointData != null)
					{
						_clientSpawnPointData.VisualState = Convert.ToInt32(value);
						return true;
					}
				}
			}
			catch (FormatException e)
			{
				Console.WriteLine(e);
			}
			return false;
		}

		// Token: 0x06001AE1 RID: 6881 RVA: 0x000AF1F0 File Offset: 0x000AE1F0
		public ChangeClientSpawnPointDataFieldOperation(ClientSpawnPointData _clientSpawnPointData, string _field, object _oldValue, object _newValue)
		{
			this.clientSpawnPointData = _clientSpawnPointData;
			this.field = _field;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001AE2 RID: 6882 RVA: 0x000AF215 File Offset: 0x000AE215
		public bool Undo()
		{
			return !this.IsEmpty && this.SetValue(this.oldValue);
		}

		// Token: 0x06001AE3 RID: 6883 RVA: 0x000AF22D File Offset: 0x000AE22D
		public bool Redo()
		{
			return !this.IsEmpty && this.SetValue(this.newValue);
		}

		// Token: 0x06001AE4 RID: 6884 RVA: 0x000AF245 File Offset: 0x000AE245
		public void Destroy()
		{
			this.clientSpawnPointData = null;
		}

		// Token: 0x17000663 RID: 1635
		// (get) Token: 0x06001AE5 RID: 6885 RVA: 0x000AF24E File Offset: 0x000AE24E
		public bool IsEmpty
		{
			get
			{
				return this.clientSpawnPointData == null || string.IsNullOrEmpty(this.field);
			}
		}

		// Token: 0x17000664 RID: 1636
		// (get) Token: 0x06001AE6 RID: 6886 RVA: 0x000AF265 File Offset: 0x000AE265
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000665 RID: 1637
		// (get) Token: 0x06001AE7 RID: 6887 RVA: 0x000AF268 File Offset: 0x000AE268
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001AE8 RID: 6888 RVA: 0x000AF26B File Offset: 0x000AE26B
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000666 RID: 1638
		// (get) Token: 0x06001AE9 RID: 6889 RVA: 0x000AF279 File Offset: 0x000AE279
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000667 RID: 1639
		// (get) Token: 0x06001AEA RID: 6890 RVA: 0x000AF280 File Offset: 0x000AE280
		public string Description
		{
			get
			{
				return "ChangeClienSpawnDataFieldOperation";
			}
		}

		// Token: 0x04001144 RID: 4420
		private ClientSpawnPointData clientSpawnPointData;

		// Token: 0x04001145 RID: 4421
		private readonly string field;

		// Token: 0x04001146 RID: 4422
		private readonly object oldValue;

		// Token: 0x04001147 RID: 4423
		private readonly object newValue;
	}
}
