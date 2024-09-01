using System;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x0200005D RID: 93
	public class MapLocatorPack : SerializableMapObjectPack
	{
		// Token: 0x170000AC RID: 172
		// (get) Token: 0x060004C9 RID: 1225 RVA: 0x00027EDD File Offset: 0x00026EDD
		// (set) Token: 0x060004CA RID: 1226 RVA: 0x00027EE5 File Offset: 0x00026EE5
		public string ScriptID
		{
			get
			{
				return this.scriptID;
			}
			set
			{
				this.scriptID = value;
			}
		}

		// Token: 0x170000AD RID: 173
		// (get) Token: 0x060004CB RID: 1227 RVA: 0x00027EEE File Offset: 0x00026EEE
		// (set) Token: 0x060004CC RID: 1228 RVA: 0x00027EF6 File Offset: 0x00026EF6
		public double ScanRadius
		{
			get
			{
				return this.scanRadius;
			}
			set
			{
				this.scanRadius = value;
			}
		}

		// Token: 0x060004CD RID: 1229 RVA: 0x00027F00 File Offset: 0x00026F00
		public override void Pack(IMapObject mapObject)
		{
			base.Pack(mapObject);
			MapLocator mapLocator = mapObject as MapLocator;
			if (mapLocator != null)
			{
				this.scriptID = mapLocator.ScriptID;
				this.scanRadius = mapLocator.ScanRadius;
			}
		}

		// Token: 0x060004CE RID: 1230 RVA: 0x00027F38 File Offset: 0x00026F38
		public override void Unpack(IMapObject mapObject)
		{
			base.Unpack(mapObject);
			MapLocator mapLocator = mapObject as MapLocator;
			if (mapLocator != null)
			{
				mapLocator.ScriptID = this.scriptID;
				mapLocator.ScanRadius = this.scanRadius;
			}
		}

		// Token: 0x04000396 RID: 918
		private string scriptID = string.Empty;

		// Token: 0x04000397 RID: 919
		private double scanRadius;
	}
}
