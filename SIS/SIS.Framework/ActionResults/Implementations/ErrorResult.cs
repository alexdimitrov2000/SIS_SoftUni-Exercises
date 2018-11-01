namespace SIS.Framework.ActionResults.Implementations
{
    using SIS.Framework.ActionResults.Interfaces;

    public class ErrorResult : IError
    {
        public ErrorResult(string content)
        {
            this.Content = content;
        }

        public string Content { get; }

        public string Invoke()
        {
            return this.Content;
        }
    }
}
