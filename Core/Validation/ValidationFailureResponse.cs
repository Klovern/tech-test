namespace Core.Validation
{
    public record ValidationFailureResponse
    {
        public IEnumerable<ValidationError> Errors { get; set; }
    }

    public record ValidationError
    {
        public string PropertyName { get; set; }
        public string Message { get; set; }
    }
}
