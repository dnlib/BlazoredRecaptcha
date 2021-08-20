using System.Text.Json.Serialization;

namespace BlazoredRecaptcha.Models
{
    public class RecaptchaResponse
    {
        /// <summary>
        /// True if the token is valid, false if it is invalid
        /// </summary>
        [JsonPropertyName("success")]
        public bool IsSuccess { get; set; }
        /// <summary>
        /// The bot likelihood score
        /// </summary>
        [JsonPropertyName("score")]
        public float Score { get; set; }
    }
}
