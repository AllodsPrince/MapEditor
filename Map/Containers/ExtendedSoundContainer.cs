using System;
using MapEditor.Map.MapObjects;

namespace MapEditor.Map.Containers
{
	// Token: 0x020001AC RID: 428
	public class ExtendedSoundContainer : TypedMapObjectContainer
	{
		// Token: 0x06001498 RID: 5272 RVA: 0x00094C50 File Offset: 0x00093C50
		private void OnExtendedSoundCentralSoundChanged(ExtendedSound extendedSound, ref Sound oldValue, ref Sound newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(extendedSound))
			{
				if (this.ExtendedSoundCentralSoundChanged != null)
				{
					this.ExtendedSoundCentralSoundChanged(this.mapEditorMapObjectContainer, extendedSound, ref oldValue, ref newValue);
				}
				if (!extendedSound.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x06001499 RID: 5273 RVA: 0x00094CA0 File Offset: 0x00093CA0
		private void OnExtendedSoundSideSoundChanged(ExtendedSound extendedSound, ref Sound oldValue, ref Sound newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(extendedSound))
			{
				if (this.ExtendedSoundSideSoundChanged != null)
				{
					this.ExtendedSoundSideSoundChanged(this.mapEditorMapObjectContainer, extendedSound, ref oldValue, ref newValue);
				}
				if (!extendedSound.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x0600149A RID: 5274 RVA: 0x00094CF0 File Offset: 0x00093CF0
		private void OnExtendedSoundNameChanged(ExtendedSound extendedSound, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && base.Contains(extendedSound))
			{
				if (this.ExtendedSoundNameChanged != null)
				{
					this.ExtendedSoundNameChanged(this.mapEditorMapObjectContainer, extendedSound, ref oldValue, ref newValue);
				}
				if (!extendedSound.Temporary)
				{
					this.mapEditorMapObjectContainer.InvokeModified();
				}
			}
		}

		// Token: 0x14000091 RID: 145
		// (add) Token: 0x0600149B RID: 5275 RVA: 0x00094D3D File Offset: 0x00093D3D
		// (remove) Token: 0x0600149C RID: 5276 RVA: 0x00094D56 File Offset: 0x00093D56
		public event ExtendedSoundContainer.ExtendedSoundCentralSoundChangedEvent<Sound> ExtendedSoundCentralSoundChanged;

		// Token: 0x14000092 RID: 146
		// (add) Token: 0x0600149D RID: 5277 RVA: 0x00094D6F File Offset: 0x00093D6F
		// (remove) Token: 0x0600149E RID: 5278 RVA: 0x00094D88 File Offset: 0x00093D88
		public event ExtendedSoundContainer.ExtendedSoundSideSoundChangedEvent<Sound> ExtendedSoundSideSoundChanged;

		// Token: 0x14000093 RID: 147
		// (add) Token: 0x0600149F RID: 5279 RVA: 0x00094DA1 File Offset: 0x00093DA1
		// (remove) Token: 0x060014A0 RID: 5280 RVA: 0x00094DBA File Offset: 0x00093DBA
		public event ExtendedSoundContainer.ExtendedSoundNameChangedEvent<string> ExtendedSoundNameChanged;

		// Token: 0x060014A1 RID: 5281 RVA: 0x00094DD4 File Offset: 0x00093DD4
		public ExtendedSoundContainer(MapEditorMapObjectContainer _mapEditorMapObjectContainer) : base(MapObjectFactory.Type.ExtendedSound, false)
		{
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				ExtendedSound.CentralSoundChanged += this.OnExtendedSoundCentralSoundChanged;
				ExtendedSound.SideSoundChanged += this.OnExtendedSoundSideSoundChanged;
				ExtendedSound.NameChanged += this.OnExtendedSoundNameChanged;
				base.Destroyed += this.OnDestroyed;
			}
		}

		// Token: 0x060014A2 RID: 5282 RVA: 0x00094E44 File Offset: 0x00093E44
		public void OnDestroyed(TypedMapObjectContainer _typedMapObjectContainer)
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				ExtendedSound.CentralSoundChanged -= this.OnExtendedSoundCentralSoundChanged;
				ExtendedSound.SideSoundChanged -= this.OnExtendedSoundSideSoundChanged;
				ExtendedSound.NameChanged -= this.OnExtendedSoundNameChanged;
				base.Destroyed -= this.OnDestroyed;
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04000E7A RID: 3706
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x020001AD RID: 429
		// (Invoke) Token: 0x060014A4 RID: 5284
		public delegate void ExtendedSoundCentralSoundChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ExtendedSound extendedSound, ref T oldValue, ref T newValue);

		// Token: 0x020001AE RID: 430
		// (Invoke) Token: 0x060014A8 RID: 5288
		public delegate void ExtendedSoundSideSoundChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ExtendedSound extendedSound, ref T oldValue, ref T newValue);

		// Token: 0x020001AF RID: 431
		// (Invoke) Token: 0x060014AC RID: 5292
		public delegate void ExtendedSoundNameChangedEvent<T>(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ExtendedSound extendedSound, ref T oldValue, ref T newValue);
	}
}
