using System;
using System.Drawing;
using System.Windows.Forms;

namespace MapEditor.Forms.LightEditor
{
	// Token: 0x020002B4 RID: 692
	public class ColorValidator : Validator
	{
		// Token: 0x06001FD7 RID: 8151 RVA: 0x000CBDD4 File Offset: 0x000CADD4
		private void OnTextBoxChanging(object sender, EventArgs e)
		{
			base.InvokeValueChangingEvent();
		}

		// Token: 0x06001FD8 RID: 8152 RVA: 0x000CBDDC File Offset: 0x000CADDC
		private void OnRadioButtonCheckedChanged(object sender, EventArgs e)
		{
			if (this.radioButton.Checked)
			{
				this.SetValue(this.alphaValue, this.hueValue, this.satValue, this.lumValue);
			}
		}

		// Token: 0x06001FD9 RID: 8153 RVA: 0x000CBE09 File Offset: 0x000CAE09
		private void OnTextBoxValidated(object sender, EventArgs e)
		{
			if (this.radioButton.Checked)
			{
				this.Validate();
			}
		}

		// Token: 0x06001FDA RID: 8154 RVA: 0x000CBE20 File Offset: 0x000CAE20
		private float GetValueFromTrackBar(TrackBar trackBar)
		{
			float tracBarVal;
			if (trackBar.Maximum == 0)
			{
				tracBarVal = 1f;
			}
			else
			{
				tracBarVal = (float)trackBar.Value / (float)trackBar.Maximum;
			}
			return tracBarVal;
		}

		// Token: 0x06001FDB RID: 8155 RVA: 0x000CBE50 File Offset: 0x000CAE50
		private void SetValueToTrackBar(TrackBar trackBar, float value)
		{
			if (trackBar == null || value < 0f || value > 1f)
			{
				return;
			}
			int tracBarVal = (int)Math.Round((double)((float)trackBar.Maximum * value));
			if (tracBarVal < trackBar.Minimum)
			{
				tracBarVal = trackBar.Minimum;
			}
			trackBar.Value = tracBarVal;
		}

		// Token: 0x06001FDC RID: 8156 RVA: 0x000CBE9C File Offset: 0x000CAE9C
		private void OnAlphaTrackBarScroll(object sender, EventArgs e)
		{
			if (this.radioButton.Checked)
			{
				this.alphaTextBox.Text = ((int)(255f * this.GetValueFromTrackBar(this.alphaTrackBar))).ToString();
				this.Validate();
			}
		}

		// Token: 0x06001FDD RID: 8157 RVA: 0x000CBEE4 File Offset: 0x000CAEE4
		private void OnHueTrackBarScroll(object sender, EventArgs e)
		{
			if (this.radioButton.Checked)
			{
				this.hueTextBox.Text = ((int)(359f * this.GetValueFromTrackBar(this.hueTrackBar))).ToString();
				this.Validate();
			}
		}

		// Token: 0x06001FDE RID: 8158 RVA: 0x000CBF2C File Offset: 0x000CAF2C
		private void OnSatTrackBarScroll(object sender, EventArgs e)
		{
			if (this.radioButton.Checked)
			{
				this.satTextBox.Text = ((int)(100f * this.GetValueFromTrackBar(this.satTrackBar))).ToString();
				this.Validate();
			}
		}

		// Token: 0x06001FDF RID: 8159 RVA: 0x000CBF74 File Offset: 0x000CAF74
		private void OnLumTrackBarScroll(object sender, EventArgs e)
		{
			if (this.radioButton.Checked)
			{
				this.lumTextBox.Text = ((int)(100f * this.GetValueFromTrackBar(this.lumTrackBar))).ToString();
				this.Validate();
			}
		}

		// Token: 0x06001FE0 RID: 8160 RVA: 0x000CBFBC File Offset: 0x000CAFBC
		private static Color FromAHSL(float _alpha, float hue, float sat, float lum)
		{
			Color result = Color.Empty;
			if (_alpha < 0f || _alpha > 1f)
			{
				return result;
			}
			if (hue < 0f || hue > 1f)
			{
				return result;
			}
			if (sat < 0f || sat > 1f)
			{
				return result;
			}
			if (lum < 0f || lum > 1f)
			{
				return result;
			}
			float q;
			if ((double)lum < 0.5)
			{
				q = lum * (sat + 1f);
			}
			else
			{
				q = lum + sat - lum * sat;
			}
			float p = 2f * lum - q;
			float[] color = new float[]
			{
				hue + 0.33333334f,
				hue,
				hue - 0.33333334f
			};
			for (int i = 0; i < 3; i++)
			{
				if (color[i] < 0f)
				{
					color[i] += 1f;
				}
				if (color[i] > 1f)
				{
					color[i] -= 1f;
				}
				if (6f * color[i] < 1f)
				{
					color[i] = p + 6f * (q - p) * color[i];
				}
				else if (6f * color[i] >= 1f && 2f * color[i] < 1f)
				{
					color[i] = q;
				}
				else if (2f * color[i] >= 1f && 3f * color[i] < 2f)
				{
					color[i] = p + 6f * (q - p) * (0.6666667f - color[i]);
				}
				else
				{
					color[i] = p;
				}
			}
			foreach (float coord in color)
			{
				if (coord < 0f || coord > 1f)
				{
					return result;
				}
			}
			int alpha = (int)(255f * _alpha);
			int red = (int)(255f * color[0]);
			int green = (int)(255f * color[1]);
			int blue = (int)(255f * color[2]);
			return Color.FromArgb(alpha, red, green, blue);
		}

		// Token: 0x06001FE1 RID: 8161 RVA: 0x000CC1C8 File Offset: 0x000CB1C8
		private static void HSVFromCoolor(Color color, out float hum, out float sat, out float lum)
		{
			hum = color.GetHue() / 359f;
			float min = (float)Math.Min(Math.Min(color.R, color.G), color.B) / 255f;
			float max = (float)Math.Max(Math.Max(color.R, color.G), color.B) / 255f;
			lum = (max + min) / 2f;
			if (lum < 0.0001f || lum > 0.9999f)
			{
				sat = 0f;
				return;
			}
			if (2f * lum < 0.9999f)
			{
				sat = (max - min) / (2f * lum);
				return;
			}
			sat = (max - min) / (2f - 2f * lum);
		}

		// Token: 0x06001FE2 RID: 8162 RVA: 0x000CC28C File Offset: 0x000CB28C
		private bool ControlsAreNotNull()
		{
			return this.radioButton != null && this.alphaTextBox != null && this.hueTextBox != null && this.satTextBox != null && this.lumTextBox != null && this.alphaTrackBar != null && this.hueTrackBar != null && this.satTrackBar != null && this.lumTrackBar != null && this.label != null;
		}

		// Token: 0x06001FE3 RID: 8163 RVA: 0x000CC300 File Offset: 0x000CB300
		public ColorValidator(Panel _panel, RadioButton _radioButton, TextBox _alphaTextBox, TextBox _hueTextBox, TextBox _satTextBox, TextBox _lumTextBox, TrackBar _alphaTrackBar, TrackBar _hueTrackBar, TrackBar _satTrackBar, TrackBar _lumTrackBar, Label _label)
		{
			this.panel = _panel;
			this.radioButton = _radioButton;
			this.alphaTextBox = _alphaTextBox;
			this.hueTextBox = _hueTextBox;
			this.satTextBox = _satTextBox;
			this.lumTextBox = _lumTextBox;
			this.alphaTrackBar = _alphaTrackBar;
			this.hueTrackBar = _hueTrackBar;
			this.satTrackBar = _satTrackBar;
			this.lumTrackBar = _lumTrackBar;
			this.label = _label;
			if (this.ControlsAreNotNull())
			{
				this.radioButton.CheckedChanged += this.OnRadioButtonCheckedChanged;
				this.alphaTextBox.Validated += this.OnTextBoxValidated;
				this.alphaTextBox.TextChanged += this.OnTextBoxChanging;
				this.hueTextBox.Validated += this.OnTextBoxValidated;
				this.hueTextBox.TextChanged += this.OnTextBoxChanging;
				this.satTextBox.Validated += this.OnTextBoxValidated;
				this.satTextBox.TextChanged += this.OnTextBoxChanging;
				this.lumTextBox.Validated += this.OnTextBoxValidated;
				this.lumTextBox.TextChanged += this.OnTextBoxChanging;
				this.alphaTrackBar.Scroll += this.OnAlphaTrackBarScroll;
				this.hueTrackBar.Scroll += this.OnHueTrackBarScroll;
				this.satTrackBar.Scroll += this.OnSatTrackBarScroll;
				this.lumTrackBar.Scroll += this.OnLumTrackBarScroll;
			}
			this.SetValue(this.alphaValue, this.hueValue, this.satValue, this.lumValue);
		}

		// Token: 0x06001FE4 RID: 8164 RVA: 0x000CC4BC File Offset: 0x000CB4BC
		public override bool Validate()
		{
			bool result = true;
			int _alpha = 0;
			int _hue = 0;
			int _sat = 0;
			int _lum = 0;
			if (result && !int.TryParse(this.alphaTextBox.Text, out _alpha))
			{
				result = false;
			}
			if (result && !int.TryParse(this.hueTextBox.Text, out _hue))
			{
				result = false;
			}
			if (result && !int.TryParse(this.satTextBox.Text, out _sat))
			{
				result = false;
			}
			if (result && !int.TryParse(this.lumTextBox.Text, out _lum))
			{
				result = false;
			}
			float alpha = (float)_alpha / 255f;
			float hue = (float)_hue / 359f;
			float sat = (float)_sat / 100f;
			float lum = (float)_lum / 100f;
			if (result && (alpha < 0f || alpha > 1f))
			{
				result = false;
			}
			if (result && (hue < 0f || hue > 1f))
			{
				result = false;
			}
			if (result && (sat < 0f || sat > 1f))
			{
				result = false;
			}
			if (result && (lum < 0f || lum > 1f))
			{
				result = false;
			}
			if (result)
			{
				this.SetValue(alpha, hue, sat, lum);
				base.InvokeValueChangedEvent();
			}
			else
			{
				this.SetValue(this.alphaValue, this.hueValue, this.satValue, this.lumValue);
			}
			return result;
		}

		// Token: 0x06001FE5 RID: 8165 RVA: 0x000CC5F5 File Offset: 0x000CB5F5
		public override void Clear()
		{
			this.SetValue(Color.Empty);
		}

		// Token: 0x06001FE6 RID: 8166 RVA: 0x000CC602 File Offset: 0x000CB602
		public void SetValue(Color _value)
		{
			ColorValidator.HSVFromCoolor(_value, out this.hueValue, out this.satValue, out this.lumValue);
			this.SetValue((float)_value.A / 255f, this.hueValue, this.satValue, this.lumValue);
		}

		// Token: 0x06001FE7 RID: 8167 RVA: 0x000CC644 File Offset: 0x000CB644
		private void SetValue(float alpha, float hue, float sat, float lum)
		{
			this.alphaValue = alpha;
			this.hueValue = hue;
			this.satValue = sat;
			this.lumValue = lum;
			if (this.ControlsAreNotNull())
			{
				this.panel.BackColor = this.GetValue();
				string AlphaResultString = ((int)(255f * alpha)).ToString();
				string HueResultString = ((int)(359f * hue)).ToString();
				string SatResultString = ((int)(100f * sat)).ToString();
				string LumResultString = ((int)(100f * lum)).ToString();
				if (this.radioButton.Checked)
				{
					this.alphaTextBox.Text = AlphaResultString;
					this.hueTextBox.Text = HueResultString;
					this.satTextBox.Text = SatResultString;
					this.lumTextBox.Text = LumResultString;
					this.SetValueToTrackBar(this.alphaTrackBar, alpha);
					this.SetValueToTrackBar(this.hueTrackBar, hue);
					this.SetValueToTrackBar(this.satTrackBar, sat);
					this.SetValueToTrackBar(this.lumTrackBar, lum);
				}
				this.label.Text = string.Concat(new object[]
				{
					'(',
					AlphaResultString,
					", ",
					HueResultString,
					", ",
					SatResultString,
					", ",
					LumResultString,
					')'
				});
			}
		}

		// Token: 0x06001FE8 RID: 8168 RVA: 0x000CC7A5 File Offset: 0x000CB7A5
		public Color GetValue()
		{
			return ColorValidator.FromAHSL(this.alphaValue, this.hueValue, this.satValue, this.lumValue);
		}

		// Token: 0x04001397 RID: 5015
		private const int alphaMax = 255;

		// Token: 0x04001398 RID: 5016
		private const int hueMax = 359;

		// Token: 0x04001399 RID: 5017
		private const int satMax = 100;

		// Token: 0x0400139A RID: 5018
		private const int lumMax = 100;

		// Token: 0x0400139B RID: 5019
		private const float eps = 0.0001f;

		// Token: 0x0400139C RID: 5020
		private float alphaValue;

		// Token: 0x0400139D RID: 5021
		private float hueValue;

		// Token: 0x0400139E RID: 5022
		private float satValue;

		// Token: 0x0400139F RID: 5023
		private float lumValue;

		// Token: 0x040013A0 RID: 5024
		private Panel panel;

		// Token: 0x040013A1 RID: 5025
		private Label label;

		// Token: 0x040013A2 RID: 5026
		private RadioButton radioButton;

		// Token: 0x040013A3 RID: 5027
		private TextBox alphaTextBox;

		// Token: 0x040013A4 RID: 5028
		private TextBox hueTextBox;

		// Token: 0x040013A5 RID: 5029
		private TextBox satTextBox;

		// Token: 0x040013A6 RID: 5030
		private TextBox lumTextBox;

		// Token: 0x040013A7 RID: 5031
		private TrackBar alphaTrackBar;

		// Token: 0x040013A8 RID: 5032
		private TrackBar hueTrackBar;

		// Token: 0x040013A9 RID: 5033
		private TrackBar satTrackBar;

		// Token: 0x040013AA RID: 5034
		private TrackBar lumTrackBar;
	}
}
