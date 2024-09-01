using System;
using MapEditor.Map.Containers;
using Operations;

namespace MapEditor.Map.MapObjects
{
	// Token: 0x02000179 RID: 377
	public class ExtendedSoundOperationContainer
	{
		// Token: 0x06001238 RID: 4664 RVA: 0x00084B48 File Offset: 0x00083B48
		private void OnExtendedSoundCentralSoundChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ExtendedSound extendedSound, ref Sound oldValue, ref Sound newValue)
		{
			if (this.mapEditorMapObjectContainer != null && extendedSound != null && this.operationContainer != null && !extendedSound.Temporary)
			{
				ChangeExtendedSoundCentralSoundOperation operation = new ChangeExtendedSoundCentralSoundOperation(extendedSound, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x06001239 RID: 4665 RVA: 0x00084B88 File Offset: 0x00083B88
		private void OnExtendedSoundSideSoundChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ExtendedSound extendedSound, ref Sound oldValue, ref Sound newValue)
		{
			if (this.mapEditorMapObjectContainer != null && extendedSound != null && this.operationContainer != null && !extendedSound.Temporary)
			{
				ChangeExtendedSoundSideSoundOperation operation = new ChangeExtendedSoundSideSoundOperation(extendedSound, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x0600123A RID: 4666 RVA: 0x00084BC8 File Offset: 0x00083BC8
		private void OnExtendedSoundNameChanged(MapEditorMapObjectContainer _mapEditorMapObjectContainer, ExtendedSound extendedSound, ref string oldValue, ref string newValue)
		{
			if (this.mapEditorMapObjectContainer != null && extendedSound != null && this.operationContainer != null && !extendedSound.Temporary)
			{
				ChangeExtendedSoundNameOperation operation = new ChangeExtendedSoundNameOperation(extendedSound, ref oldValue, ref newValue);
				this.operationContainer.Add(operation);
			}
		}

		// Token: 0x0600123B RID: 4667 RVA: 0x00084C06 File Offset: 0x00083C06
		public ExtendedSoundOperationContainer(OperationContainer _operationContainer)
		{
			this.operationContainer = _operationContainer;
		}

		// Token: 0x0600123C RID: 4668 RVA: 0x00084C15 File Offset: 0x00083C15
		public void Destroy()
		{
			if (this.operationContainer != null)
			{
				this.operationContainer = null;
			}
		}

		// Token: 0x0600123D RID: 4669 RVA: 0x00084C28 File Offset: 0x00083C28
		public void Bind(MapEditorMapObjectContainer _mapEditorMapObjectContainer)
		{
			this.Unbind();
			this.mapEditorMapObjectContainer = _mapEditorMapObjectContainer;
			if (this.mapEditorMapObjectContainer != null)
			{
				ExtendedSoundContainer extendedSoundContainer = this.mapEditorMapObjectContainer.ExtendedSoundContainer;
				if (extendedSoundContainer != null)
				{
					extendedSoundContainer.ExtendedSoundCentralSoundChanged += this.OnExtendedSoundCentralSoundChanged;
					extendedSoundContainer.ExtendedSoundSideSoundChanged += this.OnExtendedSoundSideSoundChanged;
					extendedSoundContainer.ExtendedSoundNameChanged += this.OnExtendedSoundNameChanged;
				}
			}
		}

		// Token: 0x0600123E RID: 4670 RVA: 0x00084C90 File Offset: 0x00083C90
		public void Unbind()
		{
			if (this.mapEditorMapObjectContainer != null)
			{
				ExtendedSoundContainer extendedSoundContainer = this.mapEditorMapObjectContainer.ExtendedSoundContainer;
				if (extendedSoundContainer != null)
				{
					extendedSoundContainer.ExtendedSoundCentralSoundChanged -= this.OnExtendedSoundCentralSoundChanged;
					extendedSoundContainer.ExtendedSoundSideSoundChanged -= this.OnExtendedSoundSideSoundChanged;
					extendedSoundContainer.ExtendedSoundNameChanged -= this.OnExtendedSoundNameChanged;
				}
				this.mapEditorMapObjectContainer = null;
			}
		}

		// Token: 0x04000CEE RID: 3310
		private MapEditorMapObjectContainer mapEditorMapObjectContainer;

		// Token: 0x04000CEF RID: 3311
		private OperationContainer operationContainer;
	}
}
