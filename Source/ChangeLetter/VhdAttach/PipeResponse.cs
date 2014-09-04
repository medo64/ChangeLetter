namespace ChangeLetter {
    internal class VhdAttachPipeResponse {

        public VhdAttachPipeResponse(bool isError, string message) {
            this.IsError = isError;
            this.Message = message;
        }

        public bool IsError { get; private set; }
        public string Message { get; private set; }

    }
}
