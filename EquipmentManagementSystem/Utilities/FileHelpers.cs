using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Text.RegularExpressions;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc.ModelBinding;

namespace EquipmentManagementSystem.Utilities
{
    public class FileHelpers
    {
        public static async Task<byte[]> ProcessFormFile<T>(IFormFile formFile,
            ModelStateDictionary modelState, long sizeLimit)
        {
            //var fieldDisplayName = string.Empty;

            // 允许的文件后缀
            string[] permittedExtensions = { ".txt", ".zip", ".rar", ".csv", ".xlsx", ".xls", ".docx", ".doc", ".pptx", ".ppt", ".pdf", ".jpg", ".jpeg", ".png" };

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
                            $"{trustedFileNameForDisplay} 的文件类型不符， " +
                            $"可接受的文件类型为：{string.Join("，", permittedExtensions)}。");
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
                    $"错误信息：{ex.HResult}。");
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

        public static string GetContentType(string path)
        {
            var types = GetMimeTypes();
            var ext = Path.GetExtension(path).ToLowerInvariant();
            return types[ext];
        }

        public static Dictionary<string, string> GetMimeTypes()
        {
            //{ ".txt", ".zip", ".rar",  ".csv", ".xlsx", ".xls", ".docx", ".doc", ".pptx", ".ppt", ".pdf", ".jpg", ".jpeg", ".png" };
            return new Dictionary<string, string>
            {
                {".txt", "text/plain"},
                {".zip", "application/zip" },
                {".rar", "application/vnd.rar" },
                {".csv", "text/csv"},
                {".xlsx", "application/vnd.openxmlformats-officedocument.spreadsheetml.sheet"},
                {".xls", "application/vnd.ms-excel"},
                {".docx", "application/vnd.openxmlformats-officedocument.wordprocessingml.document"},
                {".doc", "application/msword"},
                {".pptx", "application/vnd.openxmlformats-officedocument.presentationml.presentation"},
                {".ppt", "application/vnd.ms-powerpoint"},
                {".pdf", "application/pdf"},
                {".jpg", "image/jpeg"},
                {".jpeg", "image/jpeg"},
                {".png", "image/png"},
            };
        }

        public static string RemoveGuidStringInFileName(string filePath)
        {
            // 文件名中有可能不止一个"_"
            return Path.GetFileName(filePath).Split("_").LastOrDefault();
        }

        private static string FormatFileName(string fileName)
        {
            // 转义文件名中的特殊字符（#，&，+，%，=等），避免上传后无法查看和下载
            return Regex.Replace(fileName.Trim(), "[<>#&+%=\",/$:;?@{}|\\^[]`]+", "-");
        }

        public static string CreateFilePath(string folderPath, string fileName)
        {
            // 生成文件路径，文件名加上Guid
            return Path.Combine(folderPath, Guid.NewGuid().ToString() + "_" + FormatFileName(fileName));
        }

        public static async void SaveFile(byte[] formFileContent, string filePath)
        {
            // 将文件写入硬盘
            using (var fileStream = System.IO.File.Create(filePath))
            {
                await fileStream.WriteAsync(formFileContent);
            }
        }

        public static void DeleteOlderFile(string filePath)
        {
            if (System.IO.File.Exists(filePath))
            {
                System.IO.File.Delete(filePath);
            }
        }
    }
}
