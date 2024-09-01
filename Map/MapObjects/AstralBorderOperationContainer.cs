using System;
using MapEditor.Map.Containers;
using Operations;
using Tools.MapObjects;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000158 RID: 344
	public class AstralBorderOperationContainer
	{
		// Token: 0x06001092 RID: 4242 RVA: 0x0007B9D4 File Offset: 0x0007A9D4
		private void OnScaleChanged(MapObjectContainer _mapObjectContainer, IMapObject mapObject, ref Scale oldValue, ref Scale newValue)
		{
			if (this.mapEditorMapObjectContainer != null && mapObject != null && mapObject.Type.Type == MapObjectFactory.Type.AstralBorder)
			{
				AstralBorder astralBorder = mapObject as AstralBorder;
				if (astralBorder != null)
				{
					astralBorder.StabilityRadius = astralBorder.StabilityRadius * (double)newValue.Radius / (double)oldValue.Radius;
				}
			}
		}

		// Token: 0x06001093 RID: 4243 RVA: 0x0007BA28 File Offset: 0x0007AA28
		private void OnAstralBorderStabilityRadiusChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, AstralBorder astralBorder, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && astralBorder != null && this.operationContainer != null && !astralBorder.Temporary)
			{
				ChangeAstralBorderStabilityRadiusOperation operation = new ChangeAstralBorderStabilityRadiusOperation(astralBorder, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001094 RID: 4244 RVA: 0x0007BA66 File Offset: 0x0007AA66
		public AstralBorderOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x06001095 RID: 4245 RVA: 0x0007BA75 File Offset: 0x0007AA75
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x06001096 RID: 4246 RVA: 0x0007BA88 File Offset: 0x0007AA88
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				this.mapEditorMapObjectContainer.ScaleChanged += this.OnScaleChanged;
				AstralBorderContainer astralBorderContainer = this.mapEditorMapObjectContainer.AstralBorderContainer;
				if (astralBorderContainer != null)
				{
					astralBorderContainer.AstralBorderStabilityRadiusChanged += this.OnAstralBorderStabilityRadiusChanged;
				}
			}
		}

		// Token: 0x06001097 RID: 4247 RVA: 0x0007BAE4 File Offset: 0x0007AAE4
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				this.mapEditorMapObjectContainer.ScaleChanged -= this.OnScaleChanged;
				AstralBorderContainer astralBorderContainer = this.mapEditorMapObjectContainer.AstralBorderContainer;
				if (astralBorderContainer != null)
				{
					astralBorderContainer.AstralBorderStabilityRadiusChanged -= this.OnAstralBorderStabilityRadiusChanged;
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04000C2B RID: 3115
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x04000C2C RID: 3116
		private OperationContainer operationContainer;
	}
}
