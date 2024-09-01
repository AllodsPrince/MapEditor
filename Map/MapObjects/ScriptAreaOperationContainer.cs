using System;
using MapEditor.Map.Containers;
using Operations;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x020001DD RID: 477
	public class ScriptAreaOperationContainer
	{
		// Token: 0x0600183D RID: 6205 RVA: 0x000A1F70 File Offset: 0x000A0F70
		private void OnScaleChanged(MapObjectContainer _mapObjectContainer, IMapObject mapObject, ref Scale oldValue, ref Scale newValue)
		{
			if (this.mapEditorMapObjectContainer != null && mapObject != null && mapObject.Type.Type == MapObjectFactory.Type.ScriptArea)
			{
				ScriptArea scriptArea = mapObject as ScriptArea;
				if (scriptArea != null && scriptArea.ScriptAreaType == ScriptAreaType.Cylinder)
				{
					CylinderScriptAreaData cylinderScriptAreaData = scriptArea.ScriptAreaData as CylinderScriptAreaData;
					if (cylinderScriptAreaData != null)
					{
						bool activeBackup = scriptArea.Active;
						scriptArea.Active = true;
						cylinderScriptAreaData.Radius = cylinderScriptAreaData.Radius * (double)newValue.Radius / (double)oldValue.Radius;
						cylinderScriptAreaData.Halfheight = cylinderScriptAreaData.Halfheight * (double)newValue.Z / (double)oldValue.Z;
						scriptArea.Active = activeBackup;
						cylinderScriptAreaData.InvokeChanged();
					}
				}
			}
		}

		// Token: 0x0600183E RID: 6206 RVA: 0x000A201C File Offset: 0x000A101C
		private void OnScriptAreaTypeChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ScriptArea scriptArea, ref ScriptAreaData oldValue, ref ScriptAreaData newValue)
		{
			if (this.mapEditorMapObjectContainer != null && scriptArea != null && this.operationContainer != null && !scriptArea.Temporary)
			{
				ChangeScriptAreaDataOperation operation = new ChangeScriptAreaDataOperation(scriptArea, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x0600183F RID: 6207 RVA: 0x000A205C File Offset: 0x000A105C
		private void OnScriptAreaDataChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ScriptArea scriptArea, ref ScriptAreaData oldValue, ref ScriptAreaData newValue)
		{
			if (this.mapEditorMapObjectContainer != null && scriptArea != null && this.operationContainer != null && !scriptArea.Temporary)
			{
				ChangeScriptAreaDataOperation operation = new ChangeScriptAreaDataOperation(scriptArea, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001840 RID: 6208 RVA: 0x000A209C File Offset: 0x000A109C
		private void OnScriptAreaScriptZoneChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ScriptArea scriptArea, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && scriptArea != null && this.operationContainer != null && !scriptArea.Temporary)
			{
				ChangeScriptAreaScriptZoneOperation operation = new ChangeScriptAreaScriptZoneOperation(scriptArea, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001841 RID: 6209 RVA: 0x000A20DC File Offset: 0x000A10DC
		private void OnScriptAreaScriptIDChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ScriptArea scriptArea, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && scriptArea != null && this.operationContainer != null && !scriptArea.Temporary)
			{
				ChangeScriptAreaScriptIDOperation operation = new ChangeScriptAreaScriptIDOperation(scriptArea, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001842 RID: 6210 RVA: 0x000A211C File Offset: 0x000A111C
		private void OnScriptAreaScanRadiusChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ScriptArea scriptArea, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && scriptArea != null && this.operationContainer != null && !scriptArea.Temporary)
			{
				ChangeScriptAreaScanRadiusOperation operation = new ChangeScriptAreaScanRadiusOperation(scriptArea, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001843 RID: 6211 RVA: 0x000A215C File Offset: 0x000A115C
		private void OnCylinderScriptAreaDataRadiusChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ScriptArea scriptArea, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && scriptArea != null && this.operationContainer != null && !scriptArea.Temporary)
			{
				ChangeCylinderScriptAreaDataRadiusOperation operation = new ChangeCylinderScriptAreaDataRadiusOperation(scriptArea, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001844 RID: 6212 RVA: 0x000A219C File Offset: 0x000A119C
		private void OnCylinderScriptAreaDataHalfheightChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ScriptArea scriptArea, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && scriptArea != null && this.operationContainer != null && !scriptArea.Temporary)
			{
				ChangeCylinderScriptAreaDataHalfheightOperation operation = new ChangeCylinderScriptAreaDataHalfheightOperation(scriptArea, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001845 RID: 6213 RVA: 0x000A21DA File Offset: 0x000A11DA
		public ScriptAreaOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x06001846 RID: 6214 RVA: 0x000A21E9 File Offset: 0x000A11E9
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x06001847 RID: 6215 RVA: 0x000A21FC File Offset: 0x000A11FC
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				this.mapEditorMapObjectContainer.ScaleChanged += this.OnScaleChanged;
				ScriptAreaContainer scriptAreaContainer = this.mapEditorMapObjectContainer.ScriptAreaContainer;
				if (scriptAreaContainer != null)
				{
					scriptAreaContainer.ScriptAreaTypeChanged += this.OnScriptAreaTypeChanged;
					scriptAreaContainer.ScriptAreaDataChanged += this.OnScriptAreaDataChanged;
					scriptAreaContainer.ScriptAreaScriptZoneChanged += this.OnScriptAreaScriptZoneChanged;
					scriptAreaContainer.ScriptAreaScriptIDChanged += this.OnScriptAreaScriptIDChanged;
					scriptAreaContainer.ScriptAreaScanRadiusChanged += this.OnScriptAreaScanRadiusChanged;
					scriptAreaContainer.CylinderScriptAreaDataRadiusChanged += this.OnCylinderScriptAreaDataRadiusChanged;
					scriptAreaContainer.CylinderScriptAreaDataHalfheightChanged += this.OnCylinderScriptAreaDataHalfheightChanged;
				}
			}
		}

		// Token: 0x06001848 RID: 6216 RVA: 0x000A22C8 File Offset: 0x000A12C8
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				this.mapEditorMapObjectContainer.ScaleChanged -= this.OnScaleChanged;
				ScriptAreaContainer scriptAreaContainer = this.mapEditorMapObjectContainer.ScriptAreaContainer;
				if (scriptAreaContainer != null)
				{
					scriptAreaContainer.ScriptAreaTypeChanged -= this.OnScriptAreaTypeChanged;
					scriptAreaContainer.ScriptAreaDataChanged -= this.OnScriptAreaDataChanged;
					scriptAreaContainer.ScriptAreaScriptZoneChanged -= this.OnScriptAreaScriptZoneChanged;
					scriptAreaContainer.ScriptAreaScriptIDChanged -= this.OnScriptAreaScriptIDChanged;
					scriptAreaContainer.ScriptAreaScanRadiusChanged -= this.OnScriptAreaScanRadiusChanged;
					scriptAreaContainer.CylinderScriptAreaDataRadiusChanged -= this.OnCylinderScriptAreaDataRadiusChanged;
					scriptAreaContainer.CylinderScriptAreaDataHalfheightChanged -= this.OnCylinderScriptAreaDataHalfheightChanged;
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04000FA6 RID: 4006
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x04000FA7 RID: 4007
		private OperationContainer operationContainer;
	}
}
