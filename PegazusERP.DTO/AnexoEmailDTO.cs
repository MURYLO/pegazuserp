using System.IO;

namespace PegazusERP.DTO
{
    public class AnexoEmailDTO
    {
        public Stream ContentStream { get; set; }

        public string FileName { get; set; }
    }
}
