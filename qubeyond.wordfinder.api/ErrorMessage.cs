namespace qubeyond.wordfinder.api
{
    public class ErrorMessage
    {
        public string Type { get; set; }
        public string Message { get; set; }
        public string? StackTrace { get; set; }

        public ErrorMessage(Exception ex)
        {
            Type = ex.GetType().Name;
            Message = ex.Message + ex.InnerException?.Message;
            StackTrace = ex.StackTrace;
        }
    }
}
