namespace ConsumeWebApi.models.requests {

    public class TodoCreateRequest {
        public int UserId {get; set;}
        public string Title {get; set;}
        public bool Completed {get; set;}

    }

}