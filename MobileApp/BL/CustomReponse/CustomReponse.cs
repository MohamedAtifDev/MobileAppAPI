namespace MobileApp.BL.CustomReponse
{
    public class CustomReponse<T>
    {
        public int StatusCode {  get; set; }
        public List<string> Message { get; set; }
        
        public T Data { get; set; } 
    }
}
