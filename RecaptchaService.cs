using System;
using System.Net;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;
using BlazoredRecaptcha.Exceptions;
using BlazoredRecaptcha.Interfaces;
using BlazoredRecaptcha.Models;
using Microsoft.JSInterop;

namespace BlazoredRecaptcha
{
    public class RecaptchaService : IRecaptchaService
    {
        private readonly RecaptchaConfiguration _config;
        private readonly IJSRuntime _js;

        private static readonly HttpClient Client = new();

        public RecaptchaService(RecaptchaConfiguration config,
            IJSRuntime js)
        {
            _config = config;
            _js = js;
        }

        /// <summary>
        ///     Loads the reCAPTCHA V3 JS library
        /// </summary>
        /// <param name="cancellationToken"></param>
        /// <returns></returns>
        private async Task LoadRecaptchaAsync(CancellationToken cancellationToken = default)
        {
            await _js.InvokeVoidAsync("loadRecaptcha", cancellationToken, _config.SiteKey);
        }

        /// <summary>
        ///     Generates a captcha token, and loads the reCAPTCHA V3 JS library if it is not loaded already
        /// </summary>
        /// <param name="action">
        ///     The action you want to pass in to the reCAPTCHA token generation, see
        ///     <see cref="https://developers.google.com/recaptcha/docs/v3" />
        /// </param>
        /// <param name="cancellationToken"></param>
        /// <returns>The newly generated captcha token</returns>
        public async Task<string> GenerateCaptchaTokenAsync(string action,
            CancellationToken cancellationToken = default)
        {
            // Do not use the cancellation token as it may cause massive recursion
            _ = action ?? throw new ArgumentNullException(nameof(action));

            if (await _js.InvokeAsync<bool>("isRecaptchaLoaded", _config.SiteKey))
                return await _js.InvokeAsync<string>("generateCaptchaToken",_config.SiteKey, action);

            await LoadRecaptchaAsync(cancellationToken);
            await Task.Delay(1000, cancellationToken);
            return await _js.InvokeAsync<string>("generateCaptchaToken",_config.SiteKey, action);
        }

        /// <summary>
        ///     Asynchronously verifies a captcha token
        /// </summary>
        /// <param name="token">The captcha token to verify</param>
        /// <param name="cancellationToken"></param>
        /// <returns>True if it's valid and meets the score requirements, false otherwise</returns>
        public async Task<bool> VerifyCaptchaAsync(string token, CancellationToken cancellationToken = default)
        {
            _ = token ?? throw new ArgumentNullException(nameof(token));

            var request =
                await Client.GetAsync(
                    $"https://google.com/recaptcha/api/siteverify?secret={_config.SecretKey}&response={token}",
                    cancellationToken);

            if (request.StatusCode != HttpStatusCode.OK)
                throw new InvalidGoogleResponse($"Google responded with {request.StatusCode}.");

            var responseContent = await request.Content.ReadAsStringAsync(cancellationToken);
            var json = JsonSerializer.Deserialize<RecaptchaResponse>(responseContent);

            return json is {IsSuccess: true} && !(json.Score < 0.5);
        }

        public void Dispose()
        {
            Client.Dispose();
            
            GC.SuppressFinalize(this);
        }
    }
}