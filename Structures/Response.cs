namespace App1.Structures
{
    public partial class AuthenticationService
    {
        public struct Response
        {
            public Response()
            {
                Status = false;
                Message = "";
            }

            public bool Status { get; set; }
            public string Message { get; set; }
        }
    }
}
