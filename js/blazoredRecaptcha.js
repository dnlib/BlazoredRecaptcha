function loadRecaptcha(key) 
{
    let script = document.createElement('script');
    script.src = 'https://www.google.com/recaptcha/api.js?render=' + key;
    script.type = 'text/javascript';
    script.async = true;
    script.defer = true;
    script.charset = 'utf-8';

    document.getElementsByTagName('head')[0].appendChild(script);
}

function isRecaptchaLoaded(key) 
{
    try 
    {
        grecaptcha.execute(key, { action: 'homepage' }).then(function () {
            return true;
        });
    }
    catch(err)
    {
    }
    
    return false;
}

function generateCaptchaToken(key, action) 
{
    return grecaptcha.execute(key, { action: action });
}
