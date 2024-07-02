namespace Blog.Api;

public static class PersonMapper
{
    // CreateReq -> Person
    public static Person toPersonFromCreateRequest(this CreatePersonRequest req) {
        return new Person() {
            Name = req.Name
        };
    }

    // CreatePhotoReq -> PersonPhoto
    public static PersonPhoto toPersonPhotoFromCreateRequest(this CreatePersonPhotoRequest req, Person person) {
        return new PersonPhoto() {
            Caption = req.Caption,
            Url = req.Url,
            Person = person
        };
    }

    // Person -> SummaryRes
    public static PersonSummaryResponse toPersonSummaryResponse(this Person person) {
        return new PersonSummaryResponse(
            person.Id,
            person.Name
        );
    }

    // Person -> DetailRes
    public static PersonDetailResponse toPersonDetailResponse(this Person person) {
        return new PersonDetailResponse(
            person.Id,
            person.Name,
            person.Photo?.toPersonPhotoResponse()
        );
    }

    // Person -> DetailRes
    public static PersonPhotoResponse toPersonPhotoResponse(this PersonPhoto photo) {
        return new PersonPhotoResponse(
            photo.Id,
            photo.Caption,
            photo.Url
        );
    }
}
