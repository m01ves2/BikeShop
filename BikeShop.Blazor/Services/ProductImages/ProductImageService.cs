namespace BikeShop.Blazor.Services.ProductImages
{
    public class ProductImageService
    {
        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IReadOnlyList<string> GetImageUrls(int productId)
        {
            var directory = _webHostEnvironment.WebRootFileProvider.GetDirectoryContents($"images/products/{productId}");

            if (!directory.Exists)
                return [];

            return directory
                .Where(file =>
                    !file.IsDirectory &&
                    Path.GetExtension(file.Name)
                        .Equals(".webp", StringComparison.OrdinalIgnoreCase))
                .OrderBy(file => GetImageNumber(file.Name))
                .Select(file => $"/images/products/{productId}/{file.Name}")
                .ToList();
        }

        private static int GetImageNumber(string fileName)
        {
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            return int.TryParse(nameWithoutExtension, out var imageNumber)
                ? imageNumber
                : int.MaxValue;
        }
    }
}
