namespace MapEditor.Forms.RouteObjectBrowser
{
	// Token: 0x02000186 RID: 390
	public partial class RouteObjectBrowserForm : global::MapEditor.Forms.Base.BaseForm
	{
		// Token: 0x0600128C RID: 4748 RVA: 0x0008682E File Offset: 0x0008582E
		protected override void Dispose(bool disposing)
		{
			if (disposing && this.components != null)
			{
				this.components.Dispose();
			}
			base.Dispose(disposing);
		}

		// Token: 0x0600128D RID: 4749 RVA: 0x00086850 File Offset: 0x00085850
		private void InitializeComponent()
		{
			this.components = new global::System.ComponentModel.Container();
			global::System.ComponentModel.ComponentResourceManager resources = new global::System.ComponentModel.ComponentResourceManager(typeof(global::MapEditor.Forms.RouteObjectBrowser.RouteObjectBrowserForm));
			this.RoutesListView = new global::System.Windows.Forms.ListView();
			this.columnHeader00 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader01 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader02 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader03 = new global::System.Windows.Forms.ColumnHeader();
			this.columnHeader04 = new global::System.Windows.Forms.ColumnHeader();
			this.RoutesLabel = new global::System.Windows.Forms.Label();
			this.PathTrackBarLabel = new global::System.Windows.Forms.Label();
			this.PathTrackBar = new global::System.Windows.Forms.TrackBar();
			this.PathEditBox = new global::System.Windows.Forms.TextBox();
			this.PathLabel = new global::System.Windows.Forms.Label();
			this.AnimateRouteObjectCheckBox = new global::System.Windows.Forms.CheckBox();
			this.SpeedEditBox = new global::System.Windows.Forms.TextBox();
			this.SpeedLabel = new global::System.Windows.Forms.Label();
			this.SpeedTrackBarLabel = new global::System.Windows.Forms.Label();
			this.SpeedTrackBar = new global::System.Windows.Forms.TrackBar();
			this.UpdateRoutesButton = new global::System.Windows.Forms.Button();
			this.ShowCheckBox = new global::System.Windows.Forms.CheckBox();
			this.EditBoxTimer = new global::System.Windows.Forms.Timer(this.components);
			this.PointEditBox = new global::System.Windows.Forms.TextBox();
			this.PointLabel = new global::System.Windows.Forms.Label();
			this.RouteComposerButton = new global::System.Windows.Forms.Button();
			this.CameraLabel = new global::System.Windows.Forms.Label();
			this.StartCameraButton = new global::System.Windows.Forms.Button();
			this.StopCameraButton = new global::System.Windows.Forms.Button();
			this.CameraTimer = new global::System.Windows.Forms.Timer(this.components);
			this.SetObjectLabel = new global::System.Windows.Forms.Label();
			this.SetObjectDistanceTextBox = new global::System.Windows.Forms.TextBox();
			this.SetObjectButton = new global::System.Windows.Forms.Button();
			this.SetObjectRotationCheckBox = new global::System.Windows.Forms.CheckBox();
			((global::System.ComponentModel.ISupportInitialize)this.PathTrackBar).BeginInit();
			((global::System.ComponentModel.ISupportInitialize)this.SpeedTrackBar).BeginInit();
			base.SuspendLayout();
			resources.ApplyResources(this.RoutesListView, "RoutesListView");
			this.RoutesListView.Columns.AddRange(new global::System.Windows.Forms.ColumnHeader[]
			{
				this.columnHeader00,
				this.columnHeader01,
				this.columnHeader02,
				this.columnHeader03,
				this.columnHeader04
			});
			this.RoutesListView.FullRowSelect = true;
			this.RoutesListView.HideSelection = false;
			this.RoutesListView.Name = "RoutesListView";
			this.RoutesListView.ShowItemToolTips = true;
			this.RoutesListView.Sorting = global::System.Windows.Forms.SortOrder.Ascending;
			this.RoutesListView.UseCompatibleStateImageBehavior = false;
			this.RoutesListView.View = global::System.Windows.Forms.View.Details;
			this.RoutesListView.MouseDoubleClick += new global::System.Windows.Forms.MouseEventHandler(this.RoutesListView_MouseDoubleClick);
			this.RoutesListView.ItemSelectionChanged += new global::System.Windows.Forms.ListViewItemSelectionChangedEventHandler(this.RoutesListView_ItemSelectionChanged);
			resources.ApplyResources(this.columnHeader00, "columnHeader00");
			resources.ApplyResources(this.columnHeader01, "columnHeader01");
			resources.ApplyResources(this.columnHeader02, "columnHeader02");
			resources.ApplyResources(this.columnHeader03, "columnHeader03");
			resources.ApplyResources(this.columnHeader04, "columnHeader04");
			resources.ApplyResources(this.RoutesLabel, "RoutesLabel");
			this.RoutesLabel.Name = "RoutesLabel";
			resources.ApplyResources(this.PathTrackBarLabel, "PathTrackBarLabel");
			this.PathTrackBarLabel.Name = "PathTrackBarLabel";
			resources.ApplyResources(this.PathTrackBar, "PathTrackBar");
			this.PathTrackBar.LargeChange = 25;
			this.PathTrackBar.Maximum = 100;
			this.PathTrackBar.Name = "PathTrackBar";
			this.PathTrackBar.TickFrequency = 25;
			this.PathTrackBar.Scroll += new global::System.EventHandler(this.PathTrackBar_Scroll);
			resources.ApplyResources(this.PathEditBox, "PathEditBox");
			this.PathEditBox.Name = "PathEditBox";
			this.PathEditBox.TextChanged += new global::System.EventHandler(this.PathEditBox_TextChanged);
			resources.ApplyResources(this.PathLabel, "PathLabel");
			this.PathLabel.Name = "PathLabel";
			resources.ApplyResources(this.AnimateRouteObjectCheckBox, "AnimateRouteObjectCheckBox");
			this.AnimateRouteObjectCheckBox.Name = "AnimateRouteObjectCheckBox";
			this.AnimateRouteObjectCheckBox.UseVisualStyleBackColor = true;
			this.AnimateRouteObjectCheckBox.CheckedChanged += new global::System.EventHandler(this.AnimateRouteObjectCheckBox_CheckedChanged);
			resources.ApplyResources(this.SpeedEditBox, "SpeedEditBox");
			this.SpeedEditBox.Name = "SpeedEditBox";
			this.SpeedEditBox.TextChanged += new global::System.EventHandler(this.SpeedEditBox_TextChanged);
			resources.ApplyResources(this.SpeedLabel, "SpeedLabel");
			this.SpeedLabel.Name = "SpeedLabel";
			resources.ApplyResources(this.SpeedTrackBarLabel, "SpeedTrackBarLabel");
			this.SpeedTrackBarLabel.Name = "SpeedTrackBarLabel";
			resources.ApplyResources(this.SpeedTrackBar, "SpeedTrackBar");
			this.SpeedTrackBar.LargeChange = 25;
			this.SpeedTrackBar.Maximum = 100;
			this.SpeedTrackBar.Name = "SpeedTrackBar";
			this.SpeedTrackBar.TickFrequency = 25;
			this.SpeedTrackBar.Scroll += new global::System.EventHandler(this.SpeedTrackBar_Scroll);
			resources.ApplyResources(this.UpdateRoutesButton, "UpdateRoutesButton");
			this.UpdateRoutesButton.Name = "UpdateRoutesButton";
			this.UpdateRoutesButton.UseVisualStyleBackColor = true;
			this.UpdateRoutesButton.Click += new global::System.EventHandler(this.UpdateRoutesButton_Click);
			resources.ApplyResources(this.ShowCheckBox, "ShowCheckBox");
			this.ShowCheckBox.Name = "ShowCheckBox";
			this.ShowCheckBox.UseVisualStyleBackColor = true;
			this.ShowCheckBox.CheckedChanged += new global::System.EventHandler(this.ShowCheckBox_CheckedChanged);
			this.EditBoxTimer.Interval = 1000;
			this.EditBoxTimer.Tick += new global::System.EventHandler(this.EditBoxTimer_Tick);
			resources.ApplyResources(this.PointEditBox, "PointEditBox");
			this.PointEditBox.Name = "PointEditBox";
			this.PointEditBox.TextChanged += new global::System.EventHandler(this.PointEditBox_TextChanged);
			resources.ApplyResources(this.PointLabel, "PointLabel");
			this.PointLabel.Name = "PointLabel";
			resources.ApplyResources(this.RouteComposerButton, "RouteComposerButton");
			this.RouteComposerButton.Name = "RouteComposerButton";
			this.RouteComposerButton.UseVisualStyleBackColor = true;
			this.RouteComposerButton.Click += new global::System.EventHandler(this.RouteComposerButton_Click);
			resources.ApplyResources(this.CameraLabel, "CameraLabel");
			this.CameraLabel.Name = "CameraLabel";
			resources.ApplyResources(this.StartCameraButton, "StartCameraButton");
			this.StartCameraButton.Name = "StartCameraButton";
			this.StartCameraButton.UseVisualStyleBackColor = true;
			this.StartCameraButton.Click += new global::System.EventHandler(this.StartCameraButton_Click);
			resources.ApplyResources(this.StopCameraButton, "StopCameraButton");
			this.StopCameraButton.Name = "StopCameraButton";
			this.StopCameraButton.UseVisualStyleBackColor = true;
			this.StopCameraButton.Click += new global::System.EventHandler(this.StopCameraButton_Click);
			this.CameraTimer.Interval = 2000;
			this.CameraTimer.Tick += new global::System.EventHandler(this.CameraTimer_Tick);
			resources.ApplyResources(this.SetObjectLabel, "SetObjectLabel");
			this.SetObjectLabel.Name = "SetObjectLabel";
			resources.ApplyResources(this.SetObjectDistanceTextBox, "SetObjectDistanceTextBox");
			this.SetObjectDistanceTextBox.Name = "SetObjectDistanceTextBox";
			resources.ApplyResources(this.SetObjectButton, "SetObjectButton");
			this.SetObjectButton.Name = "SetObjectButton";
			this.SetObjectButton.UseVisualStyleBackColor = true;
			this.SetObjectButton.Click += new global::System.EventHandler(this.SetObjectButton_Click);
			resources.ApplyResources(this.SetObjectRotationCheckBox, "SetObjectRotationCheckBox");
			this.SetObjectRotationCheckBox.Name = "SetObjectRotationCheckBox";
			this.SetObjectRotationCheckBox.UseVisualStyleBackColor = true;
			resources.ApplyResources(this, "$this");
			base.AutoScaleMode = global::System.Windows.Forms.AutoScaleMode.Font;
			base.Controls.Add(this.SetObjectRotationCheckBox);
			base.Controls.Add(this.SetObjectButton);
			base.Controls.Add(this.SetObjectDistanceTextBox);
			base.Controls.Add(this.SetObjectLabel);
			base.Controls.Add(this.StartCameraButton);
			base.Controls.Add(this.StopCameraButton);
			base.Controls.Add(this.CameraLabel);
			base.Controls.Add(this.RouteComposerButton);
			base.Controls.Add(this.PointLabel);
			base.Controls.Add(this.PointEditBox);
			base.Controls.Add(this.ShowCheckBox);
			base.Controls.Add(this.UpdateRoutesButton);
			base.Controls.Add(this.SpeedTrackBarLabel);
			base.Controls.Add(this.SpeedTrackBar);
			base.Controls.Add(this.SpeedEditBox);
			base.Controls.Add(this.SpeedLabel);
			base.Controls.Add(this.AnimateRouteObjectCheckBox);
			base.Controls.Add(this.PathTrackBarLabel);
			base.Controls.Add(this.PathTrackBar);
			base.Controls.Add(this.PathEditBox);
			base.Controls.Add(this.PathLabel);
			base.Controls.Add(this.RoutesListView);
			base.Controls.Add(this.RoutesLabel);
			base.MaximizeBox = false;
			base.MinimizeBox = false;
			base.Name = "RouteObjectBrowserForm";
			base.ShowInTaskbar = false;
			((global::System.ComponentModel.ISupportInitialize)this.PathTrackBar).EndInit();
			((global::System.ComponentModel.ISupportInitialize)this.SpeedTrackBar).EndInit();
			base.ResumeLayout(false);
			base.PerformLayout();
		}

		// Token: 0x04000D36 RID: 3382
		private global::System.Windows.Forms.ListView RoutesListView;

		// Token: 0x04000D37 RID: 3383
		private global::System.Windows.Forms.Label RoutesLabel;

		// Token: 0x04000D38 RID: 3384
		private global::System.Windows.Forms.Label PathTrackBarLabel;

		// Token: 0x04000D39 RID: 3385
		private global::System.Windows.Forms.TrackBar PathTrackBar;

		// Token: 0x04000D3A RID: 3386
		private global::System.Windows.Forms.TextBox PathEditBox;

		// Token: 0x04000D3B RID: 3387
		private global::System.Windows.Forms.Label PathLabel;

		// Token: 0x04000D3C RID: 3388
		private global::System.Windows.Forms.CheckBox AnimateRouteObjectCheckBox;

		// Token: 0x04000D3D RID: 3389
		private global::System.Windows.Forms.TextBox SpeedEditBox;

		// Token: 0x04000D3E RID: 3390
		private global::System.Windows.Forms.Label SpeedLabel;

		// Token: 0x04000D3F RID: 3391
		private global::System.Windows.Forms.Label SpeedTrackBarLabel;

		// Token: 0x04000D40 RID: 3392
		private global::System.Windows.Forms.TrackBar SpeedTrackBar;

		// Token: 0x04000D41 RID: 3393
		private global::System.Windows.Forms.Button UpdateRoutesButton;

		// Token: 0x04000D42 RID: 3394
		private global::System.Windows.Forms.CheckBox ShowCheckBox;

		// Token: 0x04000D43 RID: 3395
		private global::System.Windows.Forms.ColumnHeader columnHeader00;

		// Token: 0x04000D44 RID: 3396
		private global::System.Windows.Forms.ColumnHeader columnHeader01;

		// Token: 0x04000D45 RID: 3397
		private global::System.Windows.Forms.ColumnHeader columnHeader03;

		// Token: 0x04000D46 RID: 3398
		private global::System.Windows.Forms.ColumnHeader columnHeader04;

		// Token: 0x04000D47 RID: 3399
		private global::System.Windows.Forms.ColumnHeader columnHeader02;

		// Token: 0x04000D48 RID: 3400
		private global::System.Windows.Forms.Timer EditBoxTimer;

		// Token: 0x04000D49 RID: 3401
		private global::System.ComponentModel.IContainer components;

		// Token: 0x04000D4A RID: 3402
		private global::System.Windows.Forms.TextBox PointEditBox;

		// Token: 0x04000D4B RID: 3403
		private global::System.Windows.Forms.Label PointLabel;

		// Token: 0x04000D4C RID: 3404
		private global::System.Windows.Forms.Button RouteComposerButton;

		// Token: 0x04000D4D RID: 3405
		private global::System.Windows.Forms.Label CameraLabel;

		// Token: 0x04000D4E RID: 3406
		private global::System.Windows.Forms.Button StartCameraButton;

		// Token: 0x04000D4F RID: 3407
		private global::System.Windows.Forms.Button StopCameraButton;

		// Token: 0x04000D50 RID: 3408
		private global::System.Windows.Forms.Timer CameraTimer;

		// Token: 0x04000D51 RID: 3409
		private global::System.Windows.Forms.Label SetObjectLabel;

		// Token: 0x04000D52 RID: 3410
		private global::System.Windows.Forms.TextBox SetObjectDistanceTextBox;

		// Token: 0x04000D53 RID: 3411
		private global::System.Windows.Forms.Button SetObjectButton;

		// Token: 0x04000D54 RID: 3412
		private global::System.Windows.Forms.CheckBox SetObjectRotationCheckBox;
	}
}
