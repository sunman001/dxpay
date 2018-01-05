namespace WEB.Models
{
    public class JsonResponseModel
    {
        public JsonResponseModel()
        {
            success = 1;
        }
        public int success { get; set; }
        public string msg { get; set; }
    }
}