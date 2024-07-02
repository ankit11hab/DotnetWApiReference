namespace Blog.Api;

public record class PersonDetailResponse
(
    int Id,
    string Name,
    PersonPhotoResponse? Photo
);
