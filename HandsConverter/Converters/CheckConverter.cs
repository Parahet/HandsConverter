using System.Text.RegularExpressions;

namespace HandsConverter.Converters
{
	public class CheckConverter : ConverterWithPlayerName
	{
		private const string patternWithoutName = @": checks";

		public CheckConverter(string str) : base(str)
		{
		}

		protected override string pattern => @".*" + patternWithoutName;

		public override string ConvertToParty()
		{
			return PlayerName + " checks";
		}

		public override string ConvertTo888()
		{
			return PlayerName + " checks";
		}

		public override void Initialize()
		{
			playerName = Regex.Replace(str, patternWithoutName, "");
		}
	}
}
