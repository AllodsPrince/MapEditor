using System;
using Db;
using Tools.SafeObjMan;

namespace MapEditor.Map.MapObjectElements
{
	// Token: 0x020002C7 RID: 711
	public class SingleSpawnController
	{
		// Token: 0x0600210D RID: 8461 RVA: 0x000D1D47 File Offset: 0x000D0D47
		private static IObjMan GetControllersManipulator(IObjMan objectMan)
		{
			if (objectMan == null)
			{
				return null;
			}
			return objectMan.CreateManipulator("controllers");
		}

		// Token: 0x140000F8 RID: 248
		// (add) Token: 0x0600210E RID: 8462 RVA: 0x000D1D59 File Offset: 0x000D0D59
		// (remove) Token: 0x0600210F RID: 8463 RVA: 0x000D1D70 File Offset: 0x000D0D70
		public static event SingleSpawnController.SpawnTimeChangedEvent Changed;

		// Token: 0x06002110 RID: 8464 RVA: 0x000D1D88 File Offset: 0x000D0D88
		public static SingleSpawnController Create(IObjMan objectMan)
		{
			objectMan = SingleSpawnController.GetControllersManipulator(objectMan);
			if (objectMan != null)
			{
				int size = objectMan.GetArraySize();
				for (int index = 0; index < size; index++)
				{
					objectMan.SetArrayIndex(index);
					if (objectMan.IsStructPtrInstanceCompatible(string.Empty, "gameMechanics.elements.spawn.PersistentSingleRespawnController"))
					{
						SingleSpawnController controller = new SingleSpawnController();
						DBID dbid;
						objectMan.GetValue("object", out dbid);
						controller.dbObject = ((!DBID.IsNullOrEmpty(dbid)) ? dbid.ToString() : null);
						return controller;
					}
				}
			}
			return null;
		}

		// Token: 0x06002111 RID: 8465 RVA: 0x000D1DFC File Offset: 0x000D0DFC
		public static void Clear(IObjMan objectMan)
		{
			objectMan = SingleSpawnController.GetControllersManipulator(objectMan);
			if (objectMan != null)
			{
				int size = objectMan.GetArraySize();
				for (int index = size - 1; index >= 0; index--)
				{
					objectMan.SetArrayIndex(index);
					if (objectMan.IsStructPtrInstanceCompatible(string.Empty, "gameMechanics.elements.spawn.PersistentSingleRespawnController"))
					{
						objectMan.Remove(string.Empty, index);
						return;
					}
				}
			}
		}

		// Token: 0x06002112 RID: 8466 RVA: 0x000D1E50 File Offset: 0x000D0E50
		public static void Save(SingleSpawnController singleSpawnController, IObjMan objectMan)
		{
			SingleSpawnController.Clear(objectMan);
			objectMan = SingleSpawnController.GetControllersManipulator(objectMan);
			if (objectMan != null && singleSpawnController != null)
			{
				objectMan.Insert(string.Empty, 0);
				objectMan.SetArrayIndex(0);
				objectMan.SetStructPtrInstance(string.Empty, "gameMechanics.elements.spawn.PersistentSingleRespawnController");
				SafeObjMan.SetDBID(objectMan, "object", singleSpawnController.dbObject);
			}
		}

		// Token: 0x1700076A RID: 1898
		// (get) Token: 0x06002113 RID: 8467 RVA: 0x000D1EA6 File Offset: 0x000D0EA6
		// (set) Token: 0x06002114 RID: 8468 RVA: 0x000D1EB0 File Offset: 0x000D0EB0
		public string DbObject
		{
			get
			{
				return this.dbObject;
			}
			set
			{
				if (!string.IsNullOrEmpty(value))
				{
					value = value.Trim();
					DBID dbid = SingleSpawnController.mainDb.GetDBIDByName(value);
					if (DBID.IsNullOrEmpty(dbid))
					{
						return;
					}
					value = dbid.ToString();
				}
				if (this.dbObject != value)
				{
					string oldValue = this.dbObject;
					this.dbObject = value;
					if (SingleSpawnController.Changed != null)
					{
						SingleSpawnController.Changed(this, oldValue, value);
					}
				}
			}
		}

		// Token: 0x06002115 RID: 8469 RVA: 0x000D1F1C File Offset: 0x000D0F1C
		public SingleSpawnController Clone()
		{
			return new SingleSpawnController
			{
				dbObject = this.dbObject
			};
		}

		// Token: 0x04001412 RID: 5138
		public const string dbType = "gameMechanics.elements.spawn.PersistentSingleRespawnController";

		// Token: 0x04001413 RID: 5139
		private const string fieldName = "controllers";

		// Token: 0x04001414 RID: 5140
		private static readonly IDatabase mainDb = IDatabase.GetMainDatabase();

		// Token: 0x04001415 RID: 5141
		private string dbObject;

		// Token: 0x020002C8 RID: 712
		// (Invoke) Token: 0x06002119 RID: 8473
		public delegate void SpawnTimeChangedEvent(SingleSpawnController sender, string oldValue, string newValue);
	}
}
