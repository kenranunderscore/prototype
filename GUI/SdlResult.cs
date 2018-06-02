namespace prototype.GUI
{
    internal struct SdlResult
    {
        private SdlResult(bool success, string message)
        {
            Success = success;
            Message = message;
        }

        public bool Success { get; }

        public string Message { get; }

        public static SdlResult Valid => new SdlResult(true, string.Empty);

        public static SdlResult Invalid(string message) => new SdlResult(false, message);
    }
}
