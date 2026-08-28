using Microsoft.AspNetCore.Components.Forms;

namespace BikeShop.Blazor.Services.ProductImages
{
    public class ProductImageService
    {
        private const long MaxImageSize = 5 * 1024 * 1024;

        private readonly IWebHostEnvironment _webHostEnvironment;

        public ProductImageService(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        public IReadOnlyList<string> GetImageUrls(int productId)
        {
            var directory = _webHostEnvironment.WebRootFileProvider
                .GetDirectoryContents($"images/products/{productId}");

            if (!directory.Exists)
                return [];

            return directory.Where(file =>
                    !file.IsDirectory &&
                    Path.GetExtension(file.Name)
                        .Equals(".webp", StringComparison.OrdinalIgnoreCase))
                .OrderBy(file => GetImageNumber(file.Name))
                .Select(file => $"/images/products/{productId}/{file.Name}?v={file.LastModified.UtcTicks}")
                .ToList();
        }

        private static int GetImageNumber(string fileName)
        {
            var nameWithoutExtension = Path.GetFileNameWithoutExtension(fileName);

            return int.TryParse(nameWithoutExtension, out var imageNumber) ? imageNumber : int.MaxValue;
        }


        public async Task SaveImagesAsync(
    int productId,
    IEnumerable<IBrowserFile> images)
        {
            var productDirectory = GetProductDirectory(productId);

            Directory.CreateDirectory(productDirectory);

            var usedNumbers = Directory
                .EnumerateFiles(productDirectory, "*.webp")
                .Select(Path.GetFileNameWithoutExtension)
                .Select(name => int.TryParse(name, out var number)
                    ? number
                    : (int?)null)
                .Where(number => number.HasValue)
                .Select(number => number!.Value)
                .ToHashSet();

            foreach (var image in images) {
                ValidateImage(image);

                var imageNumber = 1;

                while (usedNumbers.Contains(imageNumber))
                    imageNumber++;

                var imagePath = Path.Combine(
                    productDirectory,
                    $"{imageNumber}.webp");

                await using var inputStream =
                    image.OpenReadStream(MaxImageSize);

                await using var outputStream =
                    new FileStream(imagePath, FileMode.CreateNew);

                await inputStream.CopyToAsync(outputStream);

                usedNumbers.Add(imageNumber);
            }
        }

        public void DeleteImages(
            int productId,
            IEnumerable<string> imageUrls)
        {
            var productDirectory = GetProductDirectory(productId);

            foreach (var imageUrl in imageUrls) {
                var imagePathWithoutQuery = imageUrl.Split('?', 2)[0];
                var fileName = Path.GetFileName(imagePathWithoutQuery);

                //var fileName = Path.GetFileName(
                //    new Uri(imageUrl, UriKind.RelativeOrAbsolute)
                //        .LocalPath);

                if (!Path.GetExtension(fileName).Equals(
                        ".webp",
                        StringComparison.OrdinalIgnoreCase))
                    continue;

                var imagePath = Path.Combine(
                    productDirectory,
                    fileName);

                if (File.Exists(imagePath))
                    File.Delete(imagePath);
            }
        }

        private string GetProductDirectory(int productId)
        {
            if (productId <= 0)
                throw new ArgumentOutOfRangeException(nameof(productId));

            return Path.Combine(
                _webHostEnvironment.WebRootPath,
                "images",
                "products",
                productId.ToString());
        }

        private static void ValidateImage(IBrowserFile image)
        {
            if (!image.ContentType.Equals(
                    "image/webp",
                    StringComparison.OrdinalIgnoreCase))
                throw new InvalidOperationException(
                    $"File \"{image.Name}\" is not a WebP image.");

            if (image.Size > MaxImageSize)
                throw new InvalidOperationException(
                    $"File \"{image.Name}\" exceeds the 5 MB limit.");
        }
    }
}
