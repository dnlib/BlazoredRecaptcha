using System;
using System.Threading;
using System.Threading.Tasks;

namespace BlazoredRecaptcha.Interfaces
{
    public interface IRecaptchaService : IDisposable
    {
        Task<bool> VerifyCaptchaAsync(string recaptchaResponse, CancellationToken cancellationToken = default);
        Task<string> GenerateCaptchaTokenAsync(string action, CancellationToken cancellationToken = default);
    }
}