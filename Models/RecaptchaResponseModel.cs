using Newtonsoft.Json;

namespace BlazoredRecaptcha.Models
{
    public class RecaptchaResponse
    {
        /// <summary>
        /// True if the token is valid, false if it is invalid
        /// </summary>
        [JsonProperty("success")]
        public bool IsSuccess { get; set; }
        /// <summary>
        /// The bot likelihood score
        /// </summary>
        [JsonProperty("score")]
        public float Score { get; set; }
    }
}
