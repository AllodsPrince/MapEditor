using System;
using Operations;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x0200028D RID: 653
	internal class ChangeSpawnTimeFieldOperation : IOperation
	{
		// Token: 0x06001EB8 RID: 7864 RVA: 0x000C6644 File Offset: 0x000C5644
		private bool SetValue(object value)
		{
			try
			{
				string key;
				switch (key = this.field)
				{
				case "Factor":
				{
					SpawnTimeCommon _spawnTime = this.spawnTime as SpawnTimeCommon;
					if (_spawnTime != null)
					{
						_spawnTime.Factor = (float)Convert.ToInt32(value);
						return true;
					}
					break;
				}
				case "RangeSkipFirstSpawn":
				{
					SpawnTimeRange _spawnTime2 = this.spawnTime as SpawnTimeRange;
					if (_spawnTime2 != null)
					{
						_spawnTime2.SkipFirstSpawn = Convert.ToBoolean(value);
						return true;
					}
					break;
				}
				case "Min":
				{
					SpawnTimeRange _spawnTime3 = this.spawnTime as SpawnTimeRange;
					if (_spawnTime3 != null)
					{
						_spawnTime3.Min = Convert.ToInt32(value);
						return true;
					}
					break;
				}
				case "Max":
				{
					SpawnTimeRange _spawnTime4 = this.spawnTime as SpawnTimeRange;
					if (_spawnTime4 != null)
					{
						_spawnTime4.Max = Convert.ToInt32(value);
						return true;
					}
					break;
				}
				case "SimpleSkipFirstSpawn":
				{
					SpawnTimeSimple _spawnTime5 = this.spawnTime as SpawnTimeSimple;
					if (_spawnTime5 != null)
					{
						_spawnTime5.SkipFirstSpawn = Convert.ToBoolean(value);
						return true;
					}
					break;
				}
				case "SimpleTime":
				{
					SpawnTimeSimple _spawnTime6 = this.spawnTime as SpawnTimeSimple;
					if (_spawnTime6 != null)
					{
						_spawnTime6.Time = Convert.ToInt32(value);
						return true;
					}
					break;
				}
				case "TimeRareMin":
				{
					SpawnTimeRare _spawnTime7 = this.spawnTime as SpawnTimeRare;
					if (_spawnTime7 != null)
					{
						_spawnTime7.Min = Convert.ToInt32(value);
						return true;
					}
					break;
				}
				case "TimeRareMax":
				{
					SpawnTimeRare _spawnTime8 = this.spawnTime as SpawnTimeRare;
					if (_spawnTime8 != null)
					{
						_spawnTime8.Max = Convert.ToInt32(value);
						return true;
					}
					break;
				}
				case "QuestSkipFirstSpawn":
				{
					SpawnTimeQuest _spawnTime9 = this.spawnTime as SpawnTimeQuest;
					if (_spawnTime9 != null)
					{
						_spawnTime9.SkipFirstSpawn = Convert.ToBoolean(value);
						return true;
					}
					break;
				}
				}
			}
			catch (FormatException e)
			{
				Console.WriteLine(e);
			}
			return false;
		}

		// Token: 0x06001EB9 RID: 7865 RVA: 0x000C68A4 File Offset: 0x000C58A4
		public ChangeSpawnTimeFieldOperation(SpawnTimeAbstract _spawnTime, string _field, object _oldValue, object _newValue)
		{
			this.spawnTime = _spawnTime;
			this.field = _field;
			this.oldValue = _oldValue;
			this.newValue = _newValue;
		}

		// Token: 0x06001EBA RID: 7866 RVA: 0x000C68C9 File Offset: 0x000C58C9
		public bool Undo()
		{
			return !this.IsEmpty && this.SetValue(this.oldValue);
		}

		// Token: 0x06001EBB RID: 7867 RVA: 0x000C68E1 File Offset: 0x000C58E1
		public bool Redo()
		{
			return !this.IsEmpty && this.SetValue(this.newValue);
		}

		// Token: 0x06001EBC RID: 7868 RVA: 0x000C68F9 File Offset: 0x000C58F9
		public void Destroy()
		{
			this.spawnTime = null;
		}

		// Token: 0x1700070F RID: 1807
		// (get) Token: 0x06001EBD RID: 7869 RVA: 0x000C6902 File Offset: 0x000C5902
		public bool IsEmpty
		{
			get
			{
				return this.spawnTime == null || string.IsNullOrEmpty(this.field);
			}
		}

		// Token: 0x17000710 RID: 1808
		// (get) Token: 0x06001EBE RID: 7870 RVA: 0x000C6919 File Offset: 0x000C5919
		public bool IsAbsolute
		{
			get
			{
				return false;
			}
		}

		// Token: 0x17000711 RID: 1809
		// (get) Token: 0x06001EBF RID: 7871 RVA: 0x000C691C File Offset: 0x000C591C
		public bool IsHighPriority
		{
			get
			{
				return false;
			}
		}

		// Token: 0x06001EC0 RID: 7872 RVA: 0x000C691F File Offset: 0x000C591F
		public bool ContainsLabel(string label)
		{
			return label == this.Label;
		}

		// Token: 0x17000712 RID: 1810
		// (get) Token: 0x06001EC1 RID: 7873 RVA: 0x000C692D File Offset: 0x000C592D
		public string Label
		{
			get
			{
				return "MapObjectContainer";
			}
		}

		// Token: 0x17000713 RID: 1811
		// (get) Token: 0x06001EC2 RID: 7874 RVA: 0x000C6934 File Offset: 0x000C5934
		public string Description
		{
			get
			{
				return "ChangeSpawnTimeFieldOperation";
			}
		}

		// Token: 0x04001328 RID: 4904
		private SpawnTimeAbstract spawnTime;

		// Token: 0x04001329 RID: 4905
		private readonly string field;

		// Token: 0x0400132A RID: 4906
		private readonly object oldValue;

		// Token: 0x0400132B RID: 4907
		private readonly object newValue;
	}
}
