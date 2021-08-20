# BlazoredRecaptcha
A really simple Blazor server-side reCAPTCHA v3 library, allows you to do the reCAPTCHA token generation and verification in the server, instead of the client.

## License
Licensed under the MIT license, see LICENSE.md

## Requirements
- Visual Studio 2019

## Warnings
This project is designed and tested on Blazor server-side, and it potentially could work on Blazor WASM (WebAssembly). It WILL NOT work on Razor pages as it depends on IJSRuntime which Razor lacks.

## Compiling / Installation
To compile from source, if you so wish to, just simply clone the source code

```bash
git clone https://github.com/HarryTq/BlazoredRecaptcha.git
```

Then open the .sln file with Visual Studio 2019 and just rebuild. It should fetch the NuGet packages for you.

## Usage / documentation
Let's get started. To begin using the library, simply add it to your ServiceCollection:

```C#
services.AddRecaptcha(options => 
{
  options.SiteKey = "My site key";
  options.SecretKey = "My secret key";
});
```

(you can also combine this with Microsoft's standard appsettings system, I would recommend using appsettings instead of hard-coding the keys into your application)

This will inject the RecaptchaService into your ServiceCollection. You can now access it from any Blazor page like this:

```C#
@inject RecaptchaService RecaptchaService
```

Then copy the JS library (js/blazoredRecaptcha.min.js) to the wwwroot folder and add it in your `_Host.cshtml` file

```html
<script type="text/javascript" async defer src="~/blazoredRecaptcha.min.js"></script>
```

To get started generating tokens, just simply call this method:

```C#
var token = await RecaptchaService.GenerateCaptchaTokenAsync("action");
```

Then, with that token, you can validate it with Google to check if the user is likely a bot or not:

```C#
if (!await RecaptchaService.VerifyCaptchaAsync(token))
{
  Console.WriteLine("Begone bot!");
}
```

## Open-source libraries
Thank you for all these people below for your amazing open-source libraries that made this project possible

- [Newtonsoft.Json](https://github.com/JamesNK/Newtonsoft.Json)

## Contributors
Thank you for all these people for contributing to the BlazoredRecaptcha source code, and helping make it better for everyone

none yet :(
