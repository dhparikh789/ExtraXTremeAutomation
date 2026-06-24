namespace ExtraXTremeAutomation
{
	public class ExtraAutomationException : Exception
	{
		public ExtraAutomationException(string message) : base(message)
		{
		}

		public ExtraAutomationException(string message, Exception innerException) : base(message, innerException)
		{
		}
	}
}