namespace Blog.Api;

public static class PersonMapper
{
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
            person.Name,
            person.Email
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
