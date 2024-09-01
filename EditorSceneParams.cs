using System;
using System.Collections.Generic;
using System.Drawing;
using System.Windows.Forms;
using System.Xml.Serialization;
using MapEditor.Map;
using Tools.MapObjects;

namespace MapEditor
{
	// Token: 0x020000FB RID: 251
	public class EditorSceneParams
	{
		// Token: 0x17000245 RID: 581
		// (get) Token: 0x06000C5E RID: 3166 RVA: 0x0006ADE9 File Offset: 0x00069DE9
		// (set) Token: 0x06000C5F RID: 3167 RVA: 0x0006ADF1 File Offset: 0x00069DF1
		public bool ShowTerrainGrid
		{
			get
			{
				return this.showTerrainGrid;
			}
			set
			{
				if (this.showTerrainGrid != value)
				{
					this.showTerrainGrid = value;
					if (this.ShowTerrainGridChanged != null)
					{
						this.ShowTerrainGridChanged(this);
					}
				}
			}
		}

		// Token: 0x17000246 RID: 582
		// (get) Token: 0x06000C60 RID: 3168 RVA: 0x0006AE17 File Offset: 0x00069E17
		// (set) Token: 0x06000C61 RID: 3169 RVA: 0x0006AE1F File Offset: 0x00069E1F
		public bool ShowBottomGrid
		{
			get
			{
				return this.showBottomGrid;
			}
			set
			{
				if (this.showBottomGrid != value)
				{
					this.showBottomGrid = value;
					if (this.ShowBottomGridChanged != null)
					{
						this.ShowBottomGridChanged(this);
					}
				}
			}
		}

		// Token: 0x17000247 RID: 583
		// (get) Token: 0x06000C62 RID: 3170 RVA: 0x0006AE45 File Offset: 0x00069E45
		// (set) Token: 0x06000C63 RID: 3171 RVA: 0x0006AE4D File Offset: 0x00069E4D
		public bool LargeTerrainGrid
		{
			get
			{
				return this.largeTerrainGrid;
			}
			set
			{
				if (this.largeTerrainGrid != value)
				{
					this.largeTerrainGrid = value;
					if (this.LargeTerrainGridChanged != null)
					{
						this.LargeTerrainGridChanged(this);
					}
				}
			}
		}

		// Token: 0x17000248 RID: 584
		// (get) Token: 0x06000C64 RID: 3172 RVA: 0x0006AE73 File Offset: 0x00069E73
		// (set) Token: 0x06000C65 RID: 3173 RVA: 0x0006AE7B File Offset: 0x00069E7B
		public bool LargeBottomGrid
		{
			get
			{
				return this.largeBottomGrid;
			}
			set
			{
				if (this.largeBottomGrid != value)
				{
					this.largeBottomGrid = value;
					if (this.LargeBottomGridChanged != null)
					{
						this.LargeBottomGridChanged(this);
					}
				}
			}
		}

		// Token: 0x17000249 RID: 585
		// (get) Token: 0x06000C66 RID: 3174 RVA: 0x0006AEA1 File Offset: 0x00069EA1
		// (set) Token: 0x06000C67 RID: 3175 RVA: 0x0006AEA9 File Offset: 0x00069EA9
		public bool ShowCollisionGeometry
		{
			get
			{
				return this.showCollisionGeometry;
			}
			set
			{
				if (this.showCollisionGeometry != value)
				{
					this.showCollisionGeometry = value;
					if (this.ShowCollisionGeometryChanged != null)
					{
						this.ShowCollisionGeometryChanged(this);
					}
				}
			}
		}

		// Token: 0x1700024A RID: 586
		// (get) Token: 0x06000C68 RID: 3176 RVA: 0x0006AECF File Offset: 0x00069ECF
		// (set) Token: 0x06000C69 RID: 3177 RVA: 0x0006AED7 File Offset: 0x00069ED7
		public bool ShowWireframe
		{
			get
			{
				return this.showWireframe;
			}
			set
			{
				if (this.showWireframe != value)
				{
					this.showWireframe = value;
					if (this.ShowWireframeChanged != null)
					{
						this.ShowWireframeChanged(this);
					}
				}
			}
		}

		// Token: 0x1700024B RID: 587
		// (get) Token: 0x06000C6A RID: 3178 RVA: 0x0006AEFD File Offset: 0x00069EFD
		// (set) Token: 0x06000C6B RID: 3179 RVA: 0x0006AF05 File Offset: 0x00069F05
		[XmlIgnore]
		public EditorSceneParams.LightmapState ActiveLightmapState
		{
			get
			{
				return this.lightmapState;
			}
			set
			{
				if (this.lightmapState != value)
				{
					this.lightmapState = value;
					if (this.LightmapStateChanged != null)
					{
						this.LightmapStateChanged(this);
					}
				}
			}
		}

		// Token: 0x1700024C RID: 588
		// (get) Token: 0x06000C6C RID: 3180 RVA: 0x0006AF2B File Offset: 0x00069F2B
		// (set) Token: 0x06000C6D RID: 3181 RVA: 0x0006AF33 File Offset: 0x00069F33
		public bool ShowFog
		{
			get
			{
				return this.showFog;
			}
			set
			{
				if (this.showFog != value)
				{
					this.showFog = value;
					if (this.ShowFogChanged != null)
					{
						this.ShowFogChanged(this);
					}
				}
			}
		}

		// Token: 0x1700024D RID: 589
		// (get) Token: 0x06000C6E RID: 3182 RVA: 0x0006AF59 File Offset: 0x00069F59
		// (set) Token: 0x06000C6F RID: 3183 RVA: 0x0006AF61 File Offset: 0x00069F61
		public bool ShowSky
		{
			get
			{
				return this.showSky;
			}
			set
			{
				if (this.showSky != value)
				{
					this.showSky = value;
					if (this.ShowSkyChanged != null)
					{
						this.ShowSkyChanged(this);
					}
				}
			}
		}

		// Token: 0x1700024E RID: 590
		// (get) Token: 0x06000C70 RID: 3184 RVA: 0x0006AF87 File Offset: 0x00069F87
		// (set) Token: 0x06000C71 RID: 3185 RVA: 0x0006AF8F File Offset: 0x00069F8F
		[XmlIgnore]
		public bool ShowGrass
		{
			get
			{
				return this.showGrass;
			}
			set
			{
				if (this.showGrass != value)
				{
					this.showGrass = value;
					if (this.ShowGrassChanged != null)
					{
						this.ShowGrassChanged(this);
					}
				}
			}
		}

		// Token: 0x1700024F RID: 591
		// (get) Token: 0x06000C72 RID: 3186 RVA: 0x0006AFB5 File Offset: 0x00069FB5
		// (set) Token: 0x06000C73 RID: 3187 RVA: 0x0006AFBD File Offset: 0x00069FBD
		[XmlIgnore]
		public bool ShowWater
		{
			get
			{
				return this.showWater;
			}
			set
			{
				if (this.showWater != value)
				{
					this.showWater = value;
					if (this.ShowWaterChanged != null)
					{
						this.ShowWaterChanged(this);
					}
				}
			}
		}

		// Token: 0x17000250 RID: 592
		// (get) Token: 0x06000C74 RID: 3188 RVA: 0x0006AFE3 File Offset: 0x00069FE3
		// (set) Token: 0x06000C75 RID: 3189 RVA: 0x0006AFEB File Offset: 0x00069FEB
		[XmlIgnore]
		public bool ShowWorldCutSphere
		{
			get
			{
				return this.showWorldCutSphere;
			}
			set
			{
				if (this.showWorldCutSphere != value)
				{
					this.showWorldCutSphere = value;
					if (this.ShowWorldCutSphereChanged != null)
					{
						this.ShowWorldCutSphereChanged(this);
					}
				}
			}
		}

		// Token: 0x17000251 RID: 593
		// (get) Token: 0x06000C76 RID: 3190 RVA: 0x0006B011 File Offset: 0x0006A011
		// (set) Token: 0x06000C77 RID: 3191 RVA: 0x0006B019 File Offset: 0x0006A019
		public string ArtDirectory
		{
			get
			{
				return this.artDirectory;
			}
			set
			{
				if (this.artDirectory != value)
				{
					this.artDirectory = value;
					if (this.ArtDirectoryChanged != null)
					{
						this.ArtDirectoryChanged(this);
					}
				}
			}
		}

		// Token: 0x17000252 RID: 594
		// (get) Token: 0x06000C78 RID: 3192 RVA: 0x0006B044 File Offset: 0x0006A044
		// (set) Token: 0x06000C79 RID: 3193 RVA: 0x0006B04C File Offset: 0x0006A04C
		public int TerrainGridColor
		{
			get
			{
				return this.terrainGridColor;
			}
			set
			{
				this.terrainGridColor = value;
				if (this.TerrainGridColorChanged != null)
				{
					this.TerrainGridColorChanged(this);
				}
			}
		}

		// Token: 0x17000253 RID: 595
		// (get) Token: 0x06000C7A RID: 3194 RVA: 0x0006B069 File Offset: 0x0006A069
		// (set) Token: 0x06000C7B RID: 3195 RVA: 0x0006B071 File Offset: 0x0006A071
		public int BottomGridColor
		{
			get
			{
				return this.bottomGridColor;
			}
			set
			{
				this.bottomGridColor = value;
				if (this.BottomGridColorChanged != null)
				{
					this.BottomGridColorChanged(this);
				}
			}
		}

		// Token: 0x17000254 RID: 596
		// (get) Token: 0x06000C7C RID: 3196 RVA: 0x0006B08E File Offset: 0x0006A08E
		// (set) Token: 0x06000C7D RID: 3197 RVA: 0x0006B096 File Offset: 0x0006A096
		public int BackgroundColor
		{
			get
			{
				return this.backgroundColor;
			}
			set
			{
				this.backgroundColor = value;
				if (this.BackgroundColorChanged != null)
				{
					this.BackgroundColorChanged(this);
				}
			}
		}

		// Token: 0x17000255 RID: 597
		// (get) Token: 0x06000C7E RID: 3198 RVA: 0x0006B0B3 File Offset: 0x0006A0B3
		public List<int> CustomColors
		{
			get
			{
				return this.customColors;
			}
		}

		// Token: 0x17000256 RID: 598
		// (get) Token: 0x06000C7F RID: 3199 RVA: 0x0006B0BB File Offset: 0x0006A0BB
		// (set) Token: 0x06000C80 RID: 3200 RVA: 0x0006B0C3 File Offset: 0x0006A0C3
		public bool FullOpen
		{
			get
			{
				return this.fullOpen;
			}
			set
			{
				this.fullOpen = value;
			}
		}

		// Token: 0x17000257 RID: 599
		// (get) Token: 0x06000C81 RID: 3201 RVA: 0x0006B0CC File Offset: 0x0006A0CC
		// (set) Token: 0x06000C82 RID: 3202 RVA: 0x0006B0D4 File Offset: 0x0006A0D4
		[XmlIgnore]
		public ContinentType ContinentType
		{
			get
			{
				return this.continentType;
			}
			set
			{
				if (this.continentType != value)
				{
					this.continentType = value;
					if (this.ContinentTypeChanged != null)
					{
						this.ContinentTypeChanged(this);
					}
				}
			}
		}

		// Token: 0x17000258 RID: 600
		// (get) Token: 0x06000C83 RID: 3203 RVA: 0x0006B0FA File Offset: 0x0006A0FA
		// (set) Token: 0x06000C84 RID: 3204 RVA: 0x0006B102 File Offset: 0x0006A102
		[XmlIgnore]
		public double AstralRadius
		{
			get
			{
				return this.astralRadius;
			}
			set
			{
				if (this.astralRadius != value)
				{
					this.astralRadius = value;
					if (this.AstralRadiusChanged != null)
					{
						this.AstralRadiusChanged(this);
					}
				}
			}
		}

		// Token: 0x17000259 RID: 601
		// (get) Token: 0x06000C85 RID: 3205 RVA: 0x0006B128 File Offset: 0x0006A128
		// (set) Token: 0x06000C86 RID: 3206 RVA: 0x0006B130 File Offset: 0x0006A130
		[XmlIgnore]
		public double AstralHalfHeight
		{
			get
			{
				return this.astralHalfHeight;
			}
			set
			{
				if (this.astralHalfHeight != value)
				{
					this.astralHalfHeight = value;
					if (this.AstralHalfHeightChanged != null)
					{
						this.AstralHalfHeightChanged(this);
					}
				}
			}
		}

		// Token: 0x1700025A RID: 602
		// (get) Token: 0x06000C87 RID: 3207 RVA: 0x0006B156 File Offset: 0x0006A156
		// (set) Token: 0x06000C88 RID: 3208 RVA: 0x0006B160 File Offset: 0x0006A160
		public Position AstralShift
		{
			get
			{
				return this.astralShift;
			}
			set
			{
				if (this.astralShift.X != value.X || this.astralShift.Y != value.Y || this.astralShift.Z != value.Z)
				{
					this.astralShift = value;
					if (this.AstralShiftChanged != null)
					{
						this.AstralShiftChanged(this);
					}
				}
			}
		}

		// Token: 0x06000C89 RID: 3209 RVA: 0x0006B1C4 File Offset: 0x0006A1C4
		public void ShowColorDialog(string tag)
		{
			ColorDialog colorDialog = new ColorDialog();
			colorDialog.AllowFullOpen = true;
			colorDialog.AnyColor = true;
			colorDialog.FullOpen = this.FullOpen;
			if (tag == "set_terrain_grid_color")
			{
				colorDialog.Color = Color.FromArgb(this.terrainGridColor);
			}
			else if (tag == "set_bottom_grid_color")
			{
				colorDialog.Color = Color.FromArgb(this.bottomGridColor);
			}
			else if (tag == "set_background_color")
			{
				colorDialog.Color = Color.FromArgb(this.backgroundColor);
			}
			int[] _customColors = new int[16];
			for (int index = 0; index < 16; index++)
			{
				if (index < this.CustomColors.Count)
				{
					_customColors[index] = this.CustomColors[index];
				}
				else
				{
					_customColors[index] = Color.White.ToArgb();
				}
			}
			colorDialog.CustomColors = _customColors;
			if (colorDialog.ShowDialog() == DialogResult.OK)
			{
				if (tag == "set_terrain_grid_color")
				{
					this.TerrainGridColor = colorDialog.Color.ToArgb();
				}
				else if (tag == "set_bottom_grid_color")
				{
					this.BottomGridColor = colorDialog.Color.ToArgb();
				}
				else if (tag == "set_background_color")
				{
					this.BackgroundColor = colorDialog.Color.ToArgb();
				}
			}
			this.CustomColors.Capacity = 16;
			for (int index2 = 0; index2 < 16; index2++)
			{
				if (index2 < this.CustomColors.Count)
				{
					this.CustomColors[index2] = colorDialog.CustomColors[index2];
				}
				else
				{
					this.CustomColors.Add(colorDialog.CustomColors[index2]);
				}
			}
		}

		// Token: 0x06000C8A RID: 3210 RVA: 0x0006B360 File Offset: 0x0006A360
		public void ShowArtDirectoryDialog()
		{
			FolderBrowserDialog folderDialog = new FolderBrowserDialog();
			string _artDirectory = this.artDirectory;
			folderDialog.Description = "Set Art Folder";
			if (!string.IsNullOrEmpty(_artDirectory))
			{
				folderDialog.SelectedPath = _artDirectory.Replace('/', '\\');
			}
			if (folderDialog.ShowDialog() == DialogResult.OK)
			{
				this.ArtDirectory = folderDialog.SelectedPath.Replace('\\', '/');
			}
		}

		// Token: 0x1700025B RID: 603
		// (get) Token: 0x06000C8B RID: 3211 RVA: 0x0006B3BB File Offset: 0x0006A3BB
		// (set) Token: 0x06000C8C RID: 3212 RVA: 0x0006B3C3 File Offset: 0x0006A3C3
		public bool AutoFocus
		{
			get
			{
				return this.autoFocus;
			}
			set
			{
				if (this.autoFocus != value)
				{
					this.autoFocus = value;
					if (this.AutoFocusChanged != null)
					{
						this.AutoFocusChanged(this);
					}
				}
			}
		}

		// Token: 0x1700025C RID: 604
		// (get) Token: 0x06000C8D RID: 3213 RVA: 0x0006B3E9 File Offset: 0x0006A3E9
		// (set) Token: 0x06000C8E RID: 3214 RVA: 0x0006B3F1 File Offset: 0x0006A3F1
		public bool BlockEditing
		{
			get
			{
				return this.blockEditing;
			}
			set
			{
				if (this.blockEditing != value)
				{
					this.blockEditing = value;
					if (this.BlockEditingChanged != null)
					{
						this.BlockEditingChanged(this);
					}
				}
			}
		}

		// Token: 0x04000A21 RID: 2593
		private static readonly double defaultAstralRadius = (double)(4 * Constants.PatchSize) / 2.0;

		// Token: 0x04000A22 RID: 2594
		private static readonly double defaultAstralHalfHeight = (double)(4 * Constants.PatchSize) / 16.0;

		// Token: 0x04000A23 RID: 2595
		private static readonly Position defaultAstralShift = new Position(EditorSceneParams.defaultAstralRadius, EditorSceneParams.defaultAstralRadius, 0.0);

		// Token: 0x04000A24 RID: 2596
		private static readonly int defaultTerrainGridColor = Color.White.ToArgb();

		// Token: 0x04000A25 RID: 2597
		private static readonly int defaultBottomGridColor = Color.LightCoral.ToArgb();

		// Token: 0x04000A26 RID: 2598
		private static readonly int defaultBackgroundColor = Color.Black.ToArgb();

		// Token: 0x04000A27 RID: 2599
		private bool showTerrainGrid;

		// Token: 0x04000A28 RID: 2600
		private bool showBottomGrid;

		// Token: 0x04000A29 RID: 2601
		private bool largeTerrainGrid;

		// Token: 0x04000A2A RID: 2602
		private bool largeBottomGrid;

		// Token: 0x04000A2B RID: 2603
		private bool showCollisionGeometry;

		// Token: 0x04000A2C RID: 2604
		private bool showWireframe;

		// Token: 0x04000A2D RID: 2605
		private bool showFog = true;

		// Token: 0x04000A2E RID: 2606
		private bool showSky = true;

		// Token: 0x04000A2F RID: 2607
		private bool showGrass = true;

		// Token: 0x04000A30 RID: 2608
		private bool showWater = true;

		// Token: 0x04000A31 RID: 2609
		private bool showWorldCutSphere = true;

		// Token: 0x04000A32 RID: 2610
		private string artDirectory = string.Empty;

		// Token: 0x04000A33 RID: 2611
		private EditorSceneParams.LightmapState lightmapState;

		// Token: 0x04000A34 RID: 2612
		private int terrainGridColor = EditorSceneParams.defaultTerrainGridColor;

		// Token: 0x04000A35 RID: 2613
		private int bottomGridColor = EditorSceneParams.defaultBottomGridColor;

		// Token: 0x04000A36 RID: 2614
		private int backgroundColor = EditorSceneParams.defaultBackgroundColor;

		// Token: 0x04000A37 RID: 2615
		private readonly List<int> customColors = new List<int>();

		// Token: 0x04000A38 RID: 2616
		private bool fullOpen = true;

		// Token: 0x04000A39 RID: 2617
		private ContinentType continentType;

		// Token: 0x04000A3A RID: 2618
		private double astralRadius = EditorSceneParams.defaultAstralRadius;

		// Token: 0x04000A3B RID: 2619
		private double astralHalfHeight = EditorSceneParams.defaultAstralHalfHeight;

		// Token: 0x04000A3C RID: 2620
		private Position astralShift = EditorSceneParams.defaultAstralShift;

		// Token: 0x04000A3D RID: 2621
		private bool autoFocus = true;

		// Token: 0x04000A3E RID: 2622
		private bool blockEditing;

		// Token: 0x04000A3F RID: 2623
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent ShowTerrainGridChanged;

		// Token: 0x04000A40 RID: 2624
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent ShowBottomGridChanged;

		// Token: 0x04000A41 RID: 2625
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent LargeTerrainGridChanged;

		// Token: 0x04000A42 RID: 2626
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent LargeBottomGridChanged;

		// Token: 0x04000A43 RID: 2627
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent ShowCollisionGeometryChanged;

		// Token: 0x04000A44 RID: 2628
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent ShowWireframeChanged;

		// Token: 0x04000A45 RID: 2629
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent LightmapStateChanged;

		// Token: 0x04000A46 RID: 2630
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent TerrainGridColorChanged;

		// Token: 0x04000A47 RID: 2631
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent BottomGridColorChanged;

		// Token: 0x04000A48 RID: 2632
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent BackgroundColorChanged;

		// Token: 0x04000A49 RID: 2633
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent ShowFogChanged;

		// Token: 0x04000A4A RID: 2634
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent ShowSkyChanged;

		// Token: 0x04000A4B RID: 2635
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent ShowGrassChanged;

		// Token: 0x04000A4C RID: 2636
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent ShowWaterChanged;

		// Token: 0x04000A4D RID: 2637
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent ShowWorldCutSphereChanged;

		// Token: 0x04000A4E RID: 2638
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent ShowAllObjectsChanged;

		// Token: 0x04000A4F RID: 2639
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent ArtDirectoryChanged;

		// Token: 0x04000A50 RID: 2640
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent ContinentTypeChanged;

		// Token: 0x04000A51 RID: 2641
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent AstralRadiusChanged;

		// Token: 0x04000A52 RID: 2642
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent AstralHalfHeightChanged;

		// Token: 0x04000A53 RID: 2643
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent AstralShiftChanged;

		// Token: 0x04000A54 RID: 2644
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent AutoFocusChanged;

		// Token: 0x04000A55 RID: 2645
		[XmlIgnore]
		public EditorSceneParams.ParamsEvent BlockEditingChanged;

		// Token: 0x020000FC RID: 252
		public enum LightmapState
		{
			// Token: 0x04000A57 RID: 2647
			Lightmaps,
			// Token: 0x04000A58 RID: 2648
			Passability,
			// Token: 0x04000A59 RID: 2649
			Zones,
			// Token: 0x04000A5A RID: 2650
			ZoneLights,
			// Token: 0x04000A5B RID: 2651
			Music,
			// Token: 0x04000A5C RID: 2652
			Ambience,
			// Token: 0x04000A5D RID: 2653
			TerrainMask
		}

		// Token: 0x020000FD RID: 253
		// (Invoke) Token: 0x06000C92 RID: 3218
		public delegate void ParamsEvent(EditorSceneParams editorSceneParams);
	}
}
