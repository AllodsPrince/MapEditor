using System;

namespace MapEditor.Forms.MobScripts
{
	// Token: 0x02000019 RID: 25
	public struct RandomSayActionPair
	{
		// Token: 0x06000232 RID: 562 RVA: 0x00019520 File Offset: 0x00018520
		public RandomSayActionPair(float _probabilty, string _textFile, string _text)
		{
			this.probability = _probabilty;
			this.textFile = _textFile;
			this.text = _text;
		}

		// Token: 0x04000206 RID: 518
		public readonly float probability;

		// Token: 0x04000207 RID: 519
		public string textFile;

		// Token: 0x04000208 RID: 520
		public readonly string text;
	}
}
