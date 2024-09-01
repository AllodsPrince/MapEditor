using System;
using MapEditor.Map.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x02000049 RID: 73
	public class AstralBorderContainer : TypedMapObjectContainer
	{
		// Token: 0x060003F1 RID: 1009 RVA: 0x00020D78 File Offset: 0x0001FD78
		private void OnAstralBorderStabilityRadiusChanged(AstralBorder astralBorder, ref double oldValue, ref double newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(astralBorder))
			{
				if (this.AstralBorderStabilityRadiusChanged != null)
				{
					this.AstralBorderStabilityRadiusChanged(this.mapEditorMapObjectContainer, astralBorder, ref oldValue, ref newValue);
				}
				if (!astralBorder.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x14000013 RID: 19
		// (add) Token: 0x060003F2 RID: 1010 RVA: 0x00020DC5 File Offset: 0x0001FDC5
		// (remove) Token: 0x060003F3 RID: 1011 RVA: 0x00020DDE File Offset: 0x0001FDDE
		public event AstralBorderContainer.AstralBorderFieldChangedEvent<double> AstralBorderStabilityRadiusChanged;

		// Token: 0x060003F4 RID: 1012 RVA: 0x00020DF7 File Offset: 0x0001FDF7
		public AstralBorderContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.AstralBorder, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				AstralBorder.StabilityRadiusChanged += this.OnAstralBorderStabilityRadiusChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x060003F5 RID: 1013 RVA: 0x00020E37 File Offset: 0x0001FE37
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				AstralBorder.StabilityRadiusChanged -= this.OnAstralBorderStabilityRadiusChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x040002B8 RID: 696
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x0200004A RID: 74
		// (Invoke) Token: 0x060003F7 RID: 1015
		public delegate void AstralBorderFieldChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, AstralBorder astralBorder, ref T oldValue, ref T newValue);
	}
}
