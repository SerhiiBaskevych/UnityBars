
namespace ApiPart.Models
{
    public class ResponseBody : BaseResponse
    {
        public int Id {  get; set; }
        public string? Name { get; set; }
        public string? Email { get; set; }
        public int Balance { get; set; }
    }
}
