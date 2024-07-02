namespace Blog.Api;

public record class CreatePersonPhotoRequest
(
    string Caption,
    string Url,
    int PersonId
);
