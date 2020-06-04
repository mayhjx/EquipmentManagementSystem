using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;
using System;
using System.IO;
using System.Linq;
using System.Net;
using System.Threading.Tasks;

namespace EquipmentManagementSystem.Utilities
{
    public class FileHelpers
    {

        public static async Task<byte[]> ProcessFormFile<T>(IFormFile formFile,
            ModelStateDictionary modelState, long sizeLimit)
        {

            //var fieldDisplayName = string.Empty;

            // 允许的文件后缀
            string[] permittedExtensions = { ".zip", ".rar", ".7z", ".csv", ".xlsx", ".xls", ".docx", ".doc", ".pptx", ".ppt", ".pdf", ".jpg", ".jpeg", ".png" };


            // Use reflection to obtain the display name for the model
            // property associated with this IFormFile. If a display
            // name isn't found, error messages simply won't show
            // a display name.
            //MemberInfo property =
            //        typeof(T).GetProperty(
            //            formFile.Name.Substring(formFile.Name.IndexOf(".",
            //            StringComparison.Ordinal) + 1));

            //if (property != null)
            //{
            //    if (property.GetCustomAttribute(typeof(DisplayAttribute)) is
            //        DisplayAttribute displayAttribute)
            //    {
            //        fieldDisplayName = $"{displayAttribute.Name}";
            //    }
            //}

            // Don't trust the file name sent by the client. To display
            // the file name, HTML-encode the value.
            var trustedFileNameForDisplay = WebUtility.HtmlEncode(
                formFile.FileName);

            // Check the file length. This check doesn't catch files that only have 
            // a BOM as their content.
            if (formFile.Length == 0)
            {
                modelState.AddModelError(formFile.Name,
                    $"{trustedFileNameForDisplay} 是空文件。");

                return new byte[0];
            }

            if (formFile.Length > sizeLimit)
            {
                var megabyteSizeLimit = sizeLimit / 1048576;
                modelState.AddModelError(formFile.Name,
                                    $"{trustedFileNameForDisplay} 文件大小超过 " +
                                    $"{megabyteSizeLimit:N1} MB。");

                return new byte[0];
            }

            try
            {
                using (var memoryStream = new MemoryStream())
                {
                    await formFile.CopyToAsync(memoryStream);

                    if (memoryStream.Length == 0)
                    {
                        modelState.AddModelError(formFile.Name,
                            $"{trustedFileNameForDisplay} 是空文件。");
                    }

                    if (!IsValidFileExtension(formFile.FileName, memoryStream, permittedExtensions))
                    {
                        modelState.AddModelError(formFile.Name,
                            $"{trustedFileNameForDisplay} 文件类型不符， " +
                            $"允许的文件类型为：{string.Join("，", permittedExtensions)}");
                    }
                    else
                    {
                        return memoryStream.ToArray();
                    }
                }
            }
            catch (Exception ex)
            {
                modelState.AddModelError(formFile.Name,
                    $"上传文件({trustedFileNameForDisplay})失败，" +
                    $"错误信息：{ex.HResult}");
            }

            return new byte[0];
        }

        private static bool IsValidFileExtension(string fileName, Stream data, string[] permittedExtensions)
        {
            if (string.IsNullOrEmpty(fileName) || data == null || data.Length == 0)
            {
                return false;
            }

            var ext = Path.GetExtension(fileName).ToLowerInvariant();

            if (string.IsNullOrEmpty(ext) || !permittedExtensions.Contains(ext))
            {
                return false;
            }
            return true;
        }
    }
}
