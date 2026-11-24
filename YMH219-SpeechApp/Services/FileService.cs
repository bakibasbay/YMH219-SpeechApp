using System.Text;
using SpeechApp.Models;

namespace SpeechApp.Services;

// Metinleri ve tanıma sonuçlarını .txt dosyasına kaydetme / açma işlemleri
public sealed class FileService
{
    // RichTextBox'ı kilitlememek için açılabilecek en büyük dosya boyutu (2 MB)
    private const long MaxFileSize = 2 * 1024 * 1024;

    private static readonly Encoding Utf8WithBom = new UTF8Encoding(encoderShouldEmitUTF8Identifier: true);

    static FileService()
    {
        // Eski Türkçe (Windows-1254) dosyaları okuyabilmek için kod sayfaları etkinleştirilir
        Encoding.RegisterProvider(CodePagesEncodingProvider.Instance);
    }

    public string ReadText(string path)
    {
        return Run(path, "Dosya Açılamadı", () =>
        {
            var info = new FileInfo(path);
            if (info.Length > MaxFileSize)
                throw new ServiceException("Dosya Çok Büyük", $"\"{info.Name}\" dosyası 2 MB sınırını aşıyor.");

            byte[] bytes = File.ReadAllBytes(path);
            return DecodeText(bytes);
        });
    }

    public void SaveText(string path, string content)
    {
        Run(path, "Dosya Kaydedilemedi", () =>
        {
            File.WriteAllText(path, content, Utf8WithBom);
            return true;
        });
    }

    // Tanıma sonuçlarını zaman damgası ve güven skoruyla kaydeder
    public void SaveResults(string path, IEnumerable<RecognitionResultEntry> results, string languageName)
    {
        var sb = new StringBuilder();
        sb.AppendLine("Konuşma Tanıma Sonuçları");
        sb.AppendLine($"Tarih : {DateTime.Now:dd.MM.yyyy HH:mm}");
        sb.AppendLine($"Dil   : {languageName}");
        sb.AppendLine(new string('-', 50));
        foreach (var entry in results)
            sb.AppendLine(entry.ToString());

        SaveText(path, sb.ToString());
    }

    // Önce UTF-8 denenir, geçersiz bayt varsa Türkçe ANSI kod sayfasına dönülür
    private static string DecodeText(byte[] bytes)
    {
        try
        {
            var strictUtf8 = new UTF8Encoding(encoderShouldEmitUTF8Identifier: false, throwOnInvalidBytes: true);
            string text = strictUtf8.GetString(bytes);
            return text.TrimStart('﻿');
        }
        catch (DecoderFallbackException)
        {
            return Encoding.GetEncoding(1254).GetString(bytes);
        }
    }

    // Dosya hatalarını kullanıcıya anlaşılır mesajlara çevirir
    private static T Run<T>(string path, string title, Func<T> action)
    {
        string fileName = Path.GetFileName(path);
        try
        {
            return action();
        }
        catch (UnauthorizedAccessException ex)
        {
            throw new ServiceException(title, $"\"{fileName}\" dosyasına erişim izni yok. Farklı bir konum seçin.", ex);
        }
        catch (FileNotFoundException ex)
        {
            throw new ServiceException(title, $"\"{fileName}\" dosyası bulunamadı.", ex);
        }
        catch (DirectoryNotFoundException ex)
        {
            throw new ServiceException(title, "Seçilen klasör bulunamadı.", ex);
        }
        catch (IOException ex)
        {
            throw new ServiceException(title,
                $"\"{fileName}\" dosyası başka bir program tarafından kullanılıyor olabilir.\n\nAyrıntı: {ex.Message}", ex);
        }
        catch (Exception ex) when (ex is ArgumentException or NotSupportedException or System.Security.SecurityException)
        {
            throw new ServiceException(title, $"Dosya yolu geçersiz: {path}", ex);
        }
    }
}
