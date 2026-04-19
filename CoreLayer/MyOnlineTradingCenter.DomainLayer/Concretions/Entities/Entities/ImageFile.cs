namespace MyOnlineTradingCenter.DomainLayer.Concretions.Entities.Entities;

public class ImageFile : UploadedFile
{
    public bool ShowcasePicture { get; set; } = default!;
    public ICollection<Product> Products { get; set; } = default!;

}
