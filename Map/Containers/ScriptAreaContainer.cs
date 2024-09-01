using System;
using MapEditor.Map.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x020002CF RID: 719
	public class ScriptAreaContainer : TypedMapObjectContainer
	{
		// Token: 0x06002140 RID: 8512 RVA: 0x000D2B0C File Offset: 0x000D1B0C
		private void OnScriptAreaTypeChanged(ScriptArea scriptArea, ref ScriptAreaData oldValue, ref ScriptAreaData newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(scriptArea))
			{
				if (this.ScriptAreaTypeChanged != null)
				{
					this.ScriptAreaTypeChanged(this.mapEditorMapObjectContainer, scriptArea, ref oldValue, ref newValue);
				}
				if (!scriptArea.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06002141 RID: 8513 RVA: 0x000D2B5C File Offset: 0x000D1B5C
		private void OnScriptAreaDataChanged(ScriptArea scriptArea, ref ScriptAreaData oldValue, ref ScriptAreaData newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(scriptArea))
			{
				if (this.ScriptAreaDataChanged != null)
				{
					this.ScriptAreaDataChanged(this.mapEditorMapObjectContainer, scriptArea, ref oldValue, ref newValue);
				}
				if (!scriptArea.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06002142 RID: 8514 RVA: 0x000D2BAC File Offset: 0x000D1BAC
		private void OnScriptAreaScriptZoneChanged(ScriptArea scriptArea, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(scriptArea))
			{
				if (this.ScriptAreaScriptZoneChanged != null)
				{
					this.ScriptAreaScriptZoneChanged(this.mapEditorMapObjectContainer, scriptArea, ref oldValue, ref newValue);
				}
				if (!scriptArea.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06002143 RID: 8515 RVA: 0x000D2BFC File Offset: 0x000D1BFC
		private void OnScriptAreaScriptIDChanged(ScriptArea scriptArea, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(scriptArea))
			{
				if (this.ScriptAreaScriptIDChanged != null)
				{
					this.ScriptAreaScriptIDChanged(this.mapEditorMapObjectContainer, scriptArea, ref oldValue, ref newValue);
				}
				if (!scriptArea.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06002144 RID: 8516 RVA: 0x000D2C4C File Offset: 0x000D1C4C
		private void OnScriptAreaScanRadiusChanged(ScriptArea scriptArea, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(scriptArea))
			{
				if (this.ScriptAreaScanRadiusChanged != null)
				{
					this.ScriptAreaScanRadiusChanged(this.mapEditorMapObjectContainer, scriptArea, ref oldValue, ref newValue);
				}
				if (!scriptArea.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06002145 RID: 8517 RVA: 0x000D2C99 File Offset: 0x000D1C99
		private void OnScriptAreaDataFieldChanged(ScriptArea scriptArea)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(scriptArea) && this.ScriptAreaDataFieldChanged != null)
			{
				this.ScriptAreaDataFieldChanged(this.mapEditorMapObjectContainer, scriptArea);
			}
		}

		// Token: 0x06002146 RID: 8518 RVA: 0x000D2CC8 File Offset: 0x000D1CC8
		private void OnCylinderScriptAreaDataRadiusChanged(ScriptArea scriptArea, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(scriptArea))
			{
				if (this.CylinderScriptAreaDataRadiusChanged != null)
				{
					this.CylinderScriptAreaDataRadiusChanged(this.mapEditorMapObjectContainer, scriptArea, ref oldValue, ref newValue);
				}
				if (!scriptArea.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06002147 RID: 8519 RVA: 0x000D2D18 File Offset: 0x000D1D18
		private void OnCylinderScriptAreaDataHalfheightChanged(ScriptArea scriptArea, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(scriptArea))
			{
				if (this.CylinderScriptAreaDataHalfheightChanged != null)
				{
					this.CylinderScriptAreaDataHalfheightChanged(this.mapEditorMapObjectContainer, scriptArea, ref oldValue, ref newValue);
				}
				if (!scriptArea.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x140000F9 RID: 249
		// (add) Token: 0x06002148 RID: 8520 RVA: 0x000D2D65 File Offset: 0x000D1D65
		// (remove) Token: 0x06002149 RID: 8521 RVA: 0x000D2D7E File Offset: 0x000D1D7E
		public event ScriptAreaContainer.ScriptAreaFieldChangedEvent<ScriptAreaData> ScriptAreaTypeChanged;

		// Token: 0x140000FA RID: 250
		// (add) Token: 0x0600214A RID: 8522 RVA: 0x000D2D97 File Offset: 0x000D1D97
		// (remove) Token: 0x0600214B RID: 8523 RVA: 0x000D2DB0 File Offset: 0x000D1DB0
		public event ScriptAreaContainer.ScriptAreaFieldChangedEvent<ScriptAreaData> ScriptAreaDataChanged;

		// Token: 0x140000FB RID: 251
		// (add) Token: 0x0600214C RID: 8524 RVA: 0x000D2DC9 File Offset: 0x000D1DC9
		// (remove) Token: 0x0600214D RID: 8525 RVA: 0x000D2DE2 File Offset: 0x000D1DE2
		public event ScriptAreaContainer.ScriptAreaFieldChangedEvent<string> ScriptAreaScriptZoneChanged;

		// Token: 0x140000FC RID: 252
		// (add) Token: 0x0600214E RID: 8526 RVA: 0x000D2DFB File Offset: 0x000D1DFB
		// (remove) Token: 0x0600214F RID: 8527 RVA: 0x000D2E14 File Offset: 0x000D1E14
		public event ScriptAreaContainer.ScriptAreaFieldChangedEvent<string> ScriptAreaScriptIDChanged;

		// Token: 0x140000FD RID: 253
		// (add) Token: 0x06002150 RID: 8528 RVA: 0x000D2E2D File Offset: 0x000D1E2D
		// (remove) Token: 0x06002151 RID: 8529 RVA: 0x000D2E46 File Offset: 0x000D1E46
		public event ScriptAreaContainer.ScriptAreaFieldChangedEvent<double> ScriptAreaScanRadiusChanged;

		// Token: 0x140000FE RID: 254
		// (add) Token: 0x06002152 RID: 8530 RVA: 0x000D2E5F File Offset: 0x000D1E5F
		// (remove) Token: 0x06002153 RID: 8531 RVA: 0x000D2E78 File Offset: 0x000D1E78
		public event ScriptAreaContainer.ScriptAreaDataEvent ScriptAreaDataFieldChanged;

		// Token: 0x140000FF RID: 255
		// (add) Token: 0x06002154 RID: 8532 RVA: 0x000D2E91 File Offset: 0x000D1E91
		// (remove) Token: 0x06002155 RID: 8533 RVA: 0x000D2EAA File Offset: 0x000D1EAA
		public event ScriptAreaContainer.ScriptAreaDataFieldChangedEvent<double> CylinderScriptAreaDataRadiusChanged;

		// Token: 0x14000100 RID: 256
		// (add) Token: 0x06002156 RID: 8534 RVA: 0x000D2EC3 File Offset: 0x000D1EC3
		// (remove) Token: 0x06002157 RID: 8535 RVA: 0x000D2EDC File Offset: 0x000D1EDC
		public event ScriptAreaContainer.ScriptAreaDataFieldChangedEvent<double> CylinderScriptAreaDataHalfheightChanged;

		// Token: 0x06002158 RID: 8536 RVA: 0x000D2EF8 File Offset: 0x000D1EF8
		public ScriptAreaContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.ScriptArea, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				ScriptArea.TypeChanged += this.OnScriptAreaTypeChanged;
				ScriptArea.DataChanged += this.OnScriptAreaDataChanged;
				ScriptArea.ScriptZoneChanged += this.OnScriptAreaScriptZoneChanged;
				ScriptArea.ScriptIDChanged += this.OnScriptAreaScriptIDChanged;
				ScriptArea.ScanRadiusChanged += this.OnScriptAreaScanRadiusChanged;
				ScriptAreaData.Changed += this.OnScriptAreaDataFieldChanged;
				CylinderScriptAreaData.RadiusChanged += this.OnCylinderScriptAreaDataRadiusChanged;
				CylinderScriptAreaData.HalfheightChanged += this.OnCylinderScriptAreaDataHalfheightChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x06002159 RID: 8537 RVA: 0x000D2FC0 File Offset: 0x000D1FC0
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				ScriptArea.TypeChanged -= this.OnScriptAreaTypeChanged;
				ScriptArea.DataChanged -= this.OnScriptAreaDataChanged;
				ScriptArea.ScriptZoneChanged -= this.OnScriptAreaScriptZoneChanged;
				ScriptArea.ScriptIDChanged -= this.OnScriptAreaScriptIDChanged;
				ScriptArea.ScanRadiusChanged -= this.OnScriptAreaScanRadiusChanged;
				ScriptAreaData.Changed -= this.OnScriptAreaDataFieldChanged;
				CylinderScriptAreaData.RadiusChanged -= this.OnCylinderScriptAreaDataRadiusChanged;
				CylinderScriptAreaData.HalfheightChanged -= this.OnCylinderScriptAreaDataHalfheightChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x0400142A RID: 5162
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x020002D0 RID: 720
		// (Invoke) Token: 0x0600215B RID: 8539
		public delegate void ScriptAreaFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ScriptArea scriptArea, ref T oldValue, ref T newValue);

		// Token: 0x020002D1 RID: 721
		// (Invoke) Token: 0x0600215F RID: 8543
		public delegate void ScriptAreaDataEvent(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ScriptArea scriptArea);

		// Token: 0x020002D2 RID: 722
		// (Invoke) Token: 0x06002163 RID: 8547
		public delegate void ScriptAreaDataFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ScriptArea scriptArea, ref T oldValue, ref T newValue);
	}
}
