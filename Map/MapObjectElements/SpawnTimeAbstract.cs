using System;
using System.Xml.Serialization;
using Db;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x02000281 RID: 641
	[XmlInclude(typeof(SpawnTimeRange))]
	[XmlInclude(typeof(SpawnTimeNever))]
	[XmlInclude(typeof(SpawnTimeTrash))]
	[XmlInclude(typeof(SpawnTimeOnce))]
	[XmlInclude(typeof(SpawnTimeSimple))]
	[XmlInclude(typeof(SpawnTimePatrol))]
	[XmlInclude(typeof(SpawnTimeRare))]
	[XmlInclude(typeof(SpawnTimeQuest))]
	[XmlInclude(typeof(SpawnTimeCommon))]
	public abstract class SpawnTimeAbstract
	{
		// Token: 0x06001E69 RID: 7785 RVA: 0x000C5C31 File Offset: 0x000C4C31
		protected static IObjMan GetSpawnTimeManipulator(IObjMan objectMan)
		{
			if (objectMan == null)
			{
				return null;
			}
			return objectMan.CreateManipulator("spawnTime");
		}

		// Token: 0x06001E6A RID: 7786 RVA: 0x000C5C43 File Offset: 0x000C4C43
		protected void InvokeChangedEvent(string property, object oldValue, object newValue)
		{
			if (SpawnTimeAbstract.Changed != null)
			{
				SpawnTimeAbstract.Changed(this, property, oldValue, newValue);
			}
		}

		// Token: 0x140000D5 RID: 213
		// (add) Token: 0x06001E6B RID: 7787 RVA: 0x000C5C5A File Offset: 0x000C4C5A
		// (remove) Token: 0x06001E6C RID: 7788 RVA: 0x000C5C71 File Offset: 0x000C4C71
		public static event SpawnTimeAbstract.SpawnTimeChangedEvent Changed;

		// Token: 0x06001E6D RID: 7789 RVA: 0x000C5C88 File Offset: 0x000C4C88
		public static SpawnTimeAbstract Create(string type)
		{
			if (type == "gameMechanics.elements.spawn.TimeCommon" || type == typeof(SpawnTimeCommon).Name)
			{
				return new SpawnTimeCommon();
			}
			if (type == "gameMechanics.elements.spawn.TimeOnce" || type == typeof(SpawnTimeOnce).Name)
			{
				return new SpawnTimeOnce();
			}
			if (type == "gameMechanics.elements.spawn.TimeRange" || type == typeof(SpawnTimeRange).Name)
			{
				return new SpawnTimeRange();
			}
			if (type == "gameMechanics.elements.spawn.TimeSimple" || type == typeof(SpawnTimeSimple).Name)
			{
				return new SpawnTimeSimple();
			}
			if (type == "gameMechanics.elements.spawn.TimePatrol" || type == typeof(SpawnTimePatrol).Name)
			{
				return new SpawnTimePatrol();
			}
			if (type == "gameMechanics.elements.spawn.TimeRare" || type == typeof(SpawnTimeRare).Name)
			{
				return new SpawnTimeRare();
			}
			if (type == "gameMechanics.elements.spawn.TimeTrash" || type == typeof(SpawnTimeTrash).Name)
			{
				return new SpawnTimeTrash();
			}
			if (type == "gameMechanics.elements.spawn.TimeNever" || type == typeof(SpawnTimeNever).Name)
			{
				return new SpawnTimeNever();
			}
			if (type == "gameMechanics.elements.spawn.TimeQuest" || type == typeof(SpawnTimeQuest).Name)
			{
				return new SpawnTimeQuest();
			}
			return null;
		}

		// Token: 0x06001E6E RID: 7790 RVA: 0x000C5E10 File Offset: 0x000C4E10
		public static SpawnTimeAbstract Create(IObjMan objectMan)
		{
			SpawnTimeAbstract spawnTime = null;
			if (objectMan != null)
			{
				string baseName;
				string instName;
				objectMan.IsStructPtr("spawnTime", out baseName, out instName);
				spawnTime = SpawnTimeAbstract.Create(instName);
				if (spawnTime != null)
				{
					spawnTime.Load(objectMan);
				}
			}
			return spawnTime;
		}

		// Token: 0x06001E6F RID: 7791
		public abstract void Load(IObjMan objectMan);

		// Token: 0x06001E70 RID: 7792
		public abstract void Save(IObjMan objectMan);

		// Token: 0x06001E71 RID: 7793
		public abstract SpawnTimeAbstract Clone();

		// Token: 0x06001E72 RID: 7794 RVA: 0x000C5E44 File Offset: 0x000C4E44
		public static string[] GetTypes()
		{
			return new string[]
			{
				typeof(SpawnTimeCommon).Name,
				typeof(SpawnTimeOnce).Name,
				typeof(SpawnTimeRange).Name,
				typeof(SpawnTimeSimple).Name,
				typeof(SpawnTimePatrol).Name,
				typeof(SpawnTimeRare).Name,
				typeof(SpawnTimeTrash).Name,
				typeof(SpawnTimeNever).Name,
				typeof(SpawnTimeQuest).Name
			};
		}

		// Token: 0x04001309 RID: 4873
		protected const string fieldName = "spawnTime";

		// Token: 0x02000282 RID: 642
		// (Invoke) Token: 0x06001E75 RID: 7797
		public delegate void SpawnTimeChangedEvent(SpawnTimeAbstract sender, string property, object oldValue, object newValue);
	}
}
