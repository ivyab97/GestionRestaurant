namespace Application.DTO.Response
{
    public class ResponseMessage
    {
        public int code { get; set; }
        public object result { get; set; }

        public ResponseMessage(int code, object result)
        {
            this.code = code;
            this.result = result;
        }
    }
}
