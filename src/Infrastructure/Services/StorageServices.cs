namespace Infrastructure.Data.Services;

using System.Net.Http;
using System.Net.Http.Headers;
using System.Net.Http.Json;
using Infrastructure.Interfaces;
using Microsoft.Extensions.Configuration;

public class StorageService : IStorageService
{
    private readonly HttpClient? _httpClient;
    private readonly IConfiguration? _configuration;


    public StorageService(HttpClient httpClient, IConfiguration configuration)
    // le tengo que agregar credenciales reales mi king
    {
        var keyLogin = "ACA VA LA CLAVE MISTICA";
        if (keyLogin is null)
        {
            throw new Exception("ASDA");
        }
        _httpClient = httpClient;
        _httpClient.DefaultRequestHeaders.Add(
            "apikey",
            keyLogin
        );

        _httpClient.DefaultRequestHeaders.Authorization =
            new AuthenticationHeaderValue(
                "Bearer",
                keyLogin
            );
    }

    public async Task<string> UploadSong(
    Stream stream,
    string fileName,
    string contentType)
    {
        var url =
            "https://etwseuwniupcpuigerkw.supabase.co/storage/v1/object/Ava-Sound/";

        var nombreArchivo =
            $"{Guid.NewGuid()}{Path.GetExtension(fileName)}";
        // el getextension es para obtener la extension del archivo

        using var content =
            new StreamContent(stream);

        content.Headers.ContentType =
            new MediaTypeHeaderValue(contentType);

        var response =
            await _httpClient.PostAsync(
                $"{url}{nombreArchivo}",
                content
            );


        if (!response.IsSuccessStatusCode)
        {
            var errorbody = await response.Content.ReadAsStringAsync();
            throw new Exception(errorbody);
        }

        response.EnsureSuccessStatusCode();
        var filePath = $"{url}{nombreArchivo}";

        return nombreArchivo;
    }

    public async Task<string> GetSongUrl(
    string filePath)
    {
        var bucket = "Ava-Sound";

        var body = new
        {
            expiresIn = 3600
        };

        var response =
            await _httpClient.PostAsJsonAsync(
                $"https://etwseuwniupcpuigerkw.supabase.co/storage/v1/object/sign/{bucket}/{filePath}",
                body
            );

        response.EnsureSuccessStatusCode();

        var result =
            await response.Content
                .ReadFromJsonAsync<SignedResponse>();

        return
            $"https://etwseuwniupcpuigerkw.supabase.co/storage/v1/{result!.SignedUrl}";
    }

    public class SignedResponse
    {
        public string SignedUrl { get; set; } = "";
    }
}
