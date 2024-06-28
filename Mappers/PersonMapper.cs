namespace Blog.Api;

public static class PersonMapper
{
    // CreateReq -> Person
    public static Person toPersonFromCreateRequest(this CreatePersonRequest req) {
        return new Person() {
            Name = req.Name
        };
    }

    // Person -> SummaryRes
    public static PersonSummaryResponse toPersonSummaryResponse(this Person person) {
        return new PersonSummaryResponse(
            person.Id,
            person.Name
        );
    }
}
